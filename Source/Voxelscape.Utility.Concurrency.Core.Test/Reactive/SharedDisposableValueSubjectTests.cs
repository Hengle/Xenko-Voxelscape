using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Nito.AsyncEx;
using Voxelscape.Utility.Common.Core.Disposables;
using Voxelscape.Utility.Common.Pact.Disposables;
using Voxelscape.Utility.Concurrency.Core.Reactive;
using Xunit;

namespace Voxelscape.Utility.Concurrency.Core.Test.Reactive
{
	/// <summary>
	///
	/// </summary>
	public static class SharedDisposableValueSubjectTests
	{
		[Fact]
		public static void NoSubcribersValueAutoDisposes()
		{
			using (var subject = new SharedDisposableValueSubject<int>())
			{
				var sourceValue = new DisposableWrapper<int>(0);
				sourceValue.IsDisposed.Should().BeFalse();

				subject.OnNext(sourceValue);
				sourceValue.IsDisposed.Should().BeTrue();

				subject.OnCompleted();
			}
		}

		[Fact]
		public static void SingleSurcriber()
		{
			using (var subject = new SharedDisposableValueSubject<int>())
			{
				var testNumber = 1;
				var sourceValue = new DisposableWrapper<int>(testNumber);
				sourceValue.IsDisposed.Should().BeFalse();

				IDisposableValue<int> retrieved = null;
				subject.Subscribe(value => retrieved = value);

				// value retrieved from sequence but not disposed yet
				subject.OnNext(sourceValue);
				retrieved.Should().NotBeNull();
				retrieved.Value.Should().Be(testNumber);
				retrieved.IsDisposed.Should().BeFalse();
				sourceValue.IsDisposed.Should().BeFalse();

				// disposing retrieved disposes the source value
				retrieved.Dispose();
				retrieved.IsDisposed.Should().BeTrue();
				sourceValue.IsDisposed.Should().BeTrue();

				subject.OnCompleted();
			}
		}

		[Fact]
		public static void ManySubcribers()
		{
			using (var subject = new SharedDisposableValueSubject<int>())
			{
				var testNumber = 1;
				var sourceValue = new DisposableWrapper<int>(testNumber);
				sourceValue.IsDisposed.Should().BeFalse();

				IDisposableValue<int> retrieved1 = null;
				subject.Subscribe(value => retrieved1 = value);
				IDisposableValue<int> retrieved2 = null;
				subject.Subscribe(value => retrieved2 = value);

				// value retrieved from sequence but not disposed yet
				subject.OnNext(sourceValue);
				retrieved1.Should().NotBeNull();
				retrieved1.Value.Should().Be(testNumber);
				retrieved1.IsDisposed.Should().BeFalse();
				retrieved2.Should().NotBeNull();
				retrieved2.Value.Should().Be(testNumber);
				retrieved2.IsDisposed.Should().BeFalse();
				sourceValue.IsDisposed.Should().BeFalse();

				// disposing only 1 retrieved value does not yet dispose the source value
				retrieved1.Dispose();
				retrieved1.IsDisposed.Should().BeTrue();
				retrieved2.IsDisposed.Should().BeFalse();
				retrieved2.Value.Should().Be(testNumber);
				sourceValue.IsDisposed.Should().BeFalse();

				// disposing both retrieved values disposes the source value
				retrieved2.Dispose();
				retrieved2.IsDisposed.Should().BeTrue();
				sourceValue.IsDisposed.Should().BeTrue();

				subject.OnCompleted();
			}
		}

		[Fact]
		public static void DisposingManyTimesStillRequiresEachSubscriberToDispose()
		{
			using (var subject = new SharedDisposableValueSubject<int>())
			{
				var testNumber = 1;
				var sourceValue = new DisposableWrapper<int>(testNumber);
				sourceValue.IsDisposed.Should().BeFalse();

				IDisposableValue<int> retrieved1 = null;
				subject.Subscribe(value => retrieved1 = value);
				IDisposableValue<int> retrieved2 = null;
				subject.Subscribe(value => retrieved2 = value);

				subject.OnNext(sourceValue);

				// disposing only 1 retrieved value does not yet dispose the source value
				// even though the retrieved value is disposed many times
				retrieved1.Dispose();
				retrieved1.Dispose();
				retrieved1.Dispose();
				retrieved1.IsDisposed.Should().BeTrue();
				retrieved2.IsDisposed.Should().BeFalse();
				sourceValue.IsDisposed.Should().BeFalse();

				// disposing both retrieved values disposes the source value
				retrieved2.Dispose();
				retrieved2.IsDisposed.Should().BeTrue();
				sourceValue.IsDisposed.Should().BeTrue();

				subject.OnCompleted();
			}
		}

		[Fact]
		public static void SingleSubcriberUnsubcribes()
		{
			using (var subject = new SharedDisposableValueSubject<int>())
			{
				var testNumber = 1;
				var sourceValue = new DisposableWrapper<int>(testNumber);
				sourceValue.IsDisposed.Should().BeFalse();

				var subscription = subject.Subscribe(value => { });
				subscription.Dispose();

				// source value auto disposes because no subscribers
				subject.OnNext(sourceValue);
				sourceValue.IsDisposed.Should().BeTrue();

				subject.OnCompleted();
			}
		}

		[Fact]
		public static void SubcriberUnsubcribes()
		{
			using (var subject = new SharedDisposableValueSubject<int>())
			{
				var testNumber = 1;
				var sourceValue = new DisposableWrapper<int>(testNumber);
				sourceValue.IsDisposed.Should().BeFalse();

				IDisposableValue<int> retrieved = null;
				subject.Subscribe(value => retrieved = value);

				var subscription = subject.Subscribe(value => { });
				subscription.Dispose();

				// value retrieved from sequence but not disposed yet
				subject.OnNext(sourceValue);
				retrieved.Should().NotBeNull();
				retrieved.Value.Should().Be(testNumber);
				retrieved.IsDisposed.Should().BeFalse();
				sourceValue.IsDisposed.Should().BeFalse();

				// disposing retrieved causes source to be disposed
				retrieved.Dispose();
				retrieved.IsDisposed.Should().BeTrue();
				sourceValue.IsDisposed.Should().BeTrue();

				subject.OnCompleted();
			}
		}

		[Fact]
		public static async Task DelayedSubcriberAsync()
		{
			using (var subject = new SharedDisposableValueSubject<int>())
			{
				var testNumber = 1;
				var sourceValue = new DisposableWrapper<int>(testNumber);
				sourceValue.IsDisposed.Should().BeFalse();

				// delay countdown event used just to ensure that the value isn't disposed until assertions checked
				var delay = new AsyncCountdownEvent(1);
				var disposed = new AsyncCountdownEvent(2);

				subject.Delay(TimeSpan.FromSeconds(1)).Subscribe(async value =>
				{
					await delay.WaitAsync().DontMarshallContext();
					value.Dispose();
					disposed.Signal(1);
				});

				subject.Subscribe(value =>
				{
					value.Dispose();
					disposed.Signal(1);
				});

				// value is not yet disposed
				subject.OnNext(sourceValue);
				sourceValue.IsDisposed.Should().BeFalse();

				// wait for value to be disposed
				delay.Signal(1);
				await disposed.WaitAsync().DontMarshallContext();
				sourceValue.IsDisposed.Should().BeTrue();

				subject.OnCompleted();
			}
		}

		[Fact]
		public static void MultipleObservedValues()
		{
			using (var subject = new SharedDisposableValueSubject<int>())
			{
				var testNumber1 = 1;
				var sourceValue1 = new DisposableWrapper<int>(testNumber1);
				sourceValue1.IsDisposed.Should().BeFalse();

				var testNumber2 = 2;
				var sourceValue2 = new DisposableWrapper<int>(testNumber2);
				sourceValue2.IsDisposed.Should().BeFalse();

				IDisposableValue<int> retrieved = null;
				subject.Subscribe(value => retrieved = value);

				// first test value
				// value retrieved from sequence but not disposed yet
				subject.OnNext(sourceValue1);
				retrieved.Should().NotBeNull();
				retrieved.Value.Should().Be(testNumber1);
				retrieved.IsDisposed.Should().BeFalse();
				sourceValue1.IsDisposed.Should().BeFalse();

				// disposing retrieved disposes the source value
				retrieved.Dispose();
				retrieved.IsDisposed.Should().BeTrue();
				sourceValue1.IsDisposed.Should().BeTrue();

				// second test value
				// value retrieved from sequence but not disposed yet
				subject.OnNext(sourceValue2);
				retrieved.Should().NotBeNull();
				retrieved.Value.Should().Be(testNumber2);
				retrieved.IsDisposed.Should().BeFalse();
				sourceValue2.IsDisposed.Should().BeFalse();

				// disposing retrieved disposes the source value
				retrieved.Dispose();
				retrieved.IsDisposed.Should().BeTrue();
				sourceValue2.IsDisposed.Should().BeTrue();

				subject.OnCompleted();
			}
		}
	}
}
