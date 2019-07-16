using FluentAssertions;
using Voxelscape.Utility.Common.Core.Disposables;
using Voxelscape.Utility.Common.Pact.Disposables;
using Voxelscape.Utility.Concurrency.Core.Collections;
using Xunit;

namespace Voxelscape.Utility.Concurrency.Core.Test.Collections
{
	/// <summary>
	/// Unit tests for the <see cref="CompactingBlockingQueue{T}"/> methods.
	/// </summary>
	public static class CompactingBlockingQueueTests
	{
		[Fact]
		public static void ZeroCapacityQueue()
		{
			var subject = CreateSubject(0);
			bool success;
			IDisposableValue<int> takeValue;

			// queue starts out empty, taking fails
			success = subject.TryTake(out takeValue);
			AssertFailedTryTake(success, takeValue);

			// adding to zero capacity queue fails
			success = subject.TryAdd(new DisposableWrapper<int>(1));
			success.Should().Be(false);

			// queue is still empty, taking fails
			success = subject.TryTake(out takeValue);
			AssertFailedTryTake(success, takeValue);

			// adding stale value to zero capacity queue still fails
			var value = new DisposableWrapper<int>(1);
			value.Dispose();
			success = subject.TryAdd(value);
			success.Should().Be(false);
		}

		[Fact]
		public static void SingleCapacityQueue()
		{
			var subject = CreateSubject(1);
			bool success;
			IDisposableValue<int> takeValue;

			// queue starts out empty, taking fails
			success = subject.TryTake(out takeValue);
			AssertFailedTryTake(success, takeValue);

			// adding to empty queue succeeds
			success = subject.TryAdd(new DisposableWrapper<int>(1));
			success.Should().Be(true);

			// adding to full queue fails
			success = subject.TryAdd(new DisposableWrapper<int>(2));
			success.Should().Be(false);

			// taking from full queue succeeds
			success = subject.TryTake(out takeValue);
			AssertSuccessfulTryTake(success, takeValue, 1);

			// taking from empty queue fails
			success = subject.TryTake(out takeValue);
			AssertFailedTryTake(success, takeValue);
		}

		[Fact]
		public static void CompactingSingleCapacityQueue()
		{
			var subject = CreateSubject(1);
			bool success;
			IDisposableValue<int> takeValue;

			// adding to empty queue succeeds
			var valueToDispose = new DisposableWrapper<int>(1);
			success = subject.TryAdd(valueToDispose);
			success.Should().Be(true);

			// adding to full queue fails
			success = subject.TryAdd(new DisposableWrapper<int>(2));
			success.Should().Be(false);

			// dispose value so queue can compact, adding succeeds
			valueToDispose.Dispose();
			success = subject.TryAdd(new DisposableWrapper<int>(3));
			success.Should().Be(true);

			// taking from full queue succeeds
			success = subject.TryTake(out takeValue);
			AssertSuccessfulTryTake(success, takeValue, 3);
		}

		[Fact]
		public static void SingleCapacityQueueStaleValues()
		{
			var subject = CreateSubject(1);
			bool success;
			IDisposableValue<int> takeValue;

			var staleValue = new DisposableWrapper<int>(1);
			staleValue.Dispose();

			// adding stale value to empty queue succeeds
			success = subject.TryAdd(staleValue);
			success.Should().Be(true);

			// queue does not return stale value, take fails
			success = subject.TryTake(out takeValue);
			AssertFailedTryTake(success, takeValue);

			// adding value succeeds (queue is empty)
			success = subject.TryAdd(new DisposableWrapper<int>(2));
			success.Should().Be(true);

			// adding stale value to full queue succeeds
			// (because queue accepts but doesn't actually store stale values)
			success = subject.TryAdd(staleValue);
			success.Should().Be(true);

			// adding regular value fails because queue is full
			success = subject.TryAdd(new DisposableWrapper<int>(3));
			success.Should().Be(false);

			// taking returns the non-stale value
			success = subject.TryTake(out takeValue);
			AssertSuccessfulTryTake(success, takeValue, 2);
		}

		[Fact]
		public static void QueueWraps()
		{
			var subject = CreateSubject(2);
			bool success;
			IDisposableValue<int> takeValue;

			// adding to empty queue succeeds
			success = subject.TryAdd(new DisposableWrapper<int>(1));
			success.Should().Be(true);

			// adding again succeeds
			success = subject.TryAdd(new DisposableWrapper<int>(2));
			success.Should().Be(true);

			// queue is full, adding fails
			success = subject.TryAdd(new DisposableWrapper<int>(3));
			success.Should().Be(false);

			// taking returns first value added
			success = subject.TryTake(out takeValue);
			AssertSuccessfulTryTake(success, takeValue, 1);

			// adding again succeeds
			success = subject.TryAdd(new DisposableWrapper<int>(4));
			success.Should().Be(true);

			// queue is full, adding fails
			success = subject.TryAdd(new DisposableWrapper<int>(5));
			success.Should().Be(false);

			// taking returns second value added
			success = subject.TryTake(out takeValue);
			AssertSuccessfulTryTake(success, takeValue, 2);

			// taking returns third value added
			success = subject.TryTake(out takeValue);
			AssertSuccessfulTryTake(success, takeValue, 4);

			// taking fails, queue empty
			success = subject.TryTake(out takeValue);
			AssertFailedTryTake(success, takeValue);
		}

		[Fact]
		public static void StaleValueInMiddleOfQueueCompactedOver()
		{
			var subject = CreateSubject(3);
			bool success;
			IDisposableValue<int> takeValue;

			// fill the queue
			success = subject.TryAdd(new DisposableWrapper<int>(1));
			success.Should().Be(true);

			var valueToDispose = new DisposableWrapper<int>(2);
			success = subject.TryAdd(valueToDispose);
			success.Should().Be(true);

			success = subject.TryAdd(new DisposableWrapper<int>(3));
			success.Should().Be(true);

			// confirm queue is full
			success = subject.TryAdd(new DisposableWrapper<int>(4));
			success.Should().Be(false);

			// dispose middle value, then adding succeeds because the queue can compact
			valueToDispose.Dispose();
			success = subject.TryAdd(new DisposableWrapper<int>(5));
			success.Should().Be(true);

			// confirm which values ended up in the queue
			success = subject.TryTake(out takeValue);
			AssertSuccessfulTryTake(success, takeValue, 1);

			success = subject.TryTake(out takeValue);
			AssertSuccessfulTryTake(success, takeValue, 3);

			success = subject.TryTake(out takeValue);
			AssertSuccessfulTryTake(success, takeValue, 5);

			// confirm queue empty
			success = subject.TryTake(out takeValue);
			AssertFailedTryTake(success, takeValue);
		}

		private static CompactingBlockingQueue<IDisposableValue<int>> CreateSubject(int capacity) =>
			new CompactingBlockingQueue<IDisposableValue<int>>(value => value.IsDisposed, capacity);

		private static void AssertSuccessfulTryTake<T>(bool success, IDisposableValue<T> takeValue, T value)
		{
			success.Should().Be(true);
			takeValue.IsDisposed.Should().Be(false);
			takeValue.Value.Should().Be(value);
		}

		private static void AssertFailedTryTake<T>(bool success, IDisposableValue<T> takeValue)
		{
			success.Should().Be(false);
			takeValue.Should().Be(null);
		}
	}
}
