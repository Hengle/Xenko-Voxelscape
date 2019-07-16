using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Voxelscape.Utility.Concurrency.Core.Test.Tasks
{
	/// <summary>
	/// Unit tests for the <see cref="TaskExtensions.TimeoutAfterAsync(Task, int)"/> methods.
	/// </summary>
	public static class TaskExtensionsTimeoutAfterTests
	{
		#region Test Data

		/// <summary>
		/// A short length delay in milliseconds.
		/// </summary>
		private static readonly int ShortDelay = 100;

		/// <summary>
		/// A longer length delay in milliseconds.
		/// </summary>
		private static readonly int LongDelay = 2000;

		/// <summary>
		/// The data set to use for the <see cref="AlreadyCompleted"/> tests.
		/// </summary>
		/// <returns>The data set.</returns>
		public static IEnumerable<object[]> AlreadyCompletedData()
		{
			return new object[][]
			{
				new object[] { Timeout.InfiniteTimeSpan },
				new object[] { TimeSpan.Zero },
				new object[] { TimeSpan.FromMilliseconds(1) },
				new object[] { TimeSpan.FromMilliseconds(ShortDelay) },
				new object[] { TimeSpan.FromMilliseconds(LongDelay) },
			};
		}

		#endregion

		#region Tests

		/// <summary>
		/// Tests that an already completed task is considered completed before the timeout.
		/// </summary>
		/// <param name="delay">The timeout delay.</param>
		[Theory]
		[MemberData(nameof(AlreadyCompletedData))]
		public static async void AlreadyCompleted(TimeSpan delay)
		{
			Task subject = Task.Run(() => { });
			await subject;

			bool isCompletedBeforeTimeout = await subject.TimeoutAfterAsync(delay);

			AssertTaskFinished(subject, isCompletedBeforeTimeout);
		}

		/// <summary>
		/// Tests that a task that finishes before the timeout is considered completed before the timeout.
		/// </summary>
		[Fact]
		public static async void FinishesInTime()
		{
			Task subject = Task.Run(async () => { await Task.Delay(ShortDelay); });

			bool isCompletedBeforeTimeout = await subject.TimeoutAfterAsync(LongDelay);

			AssertTaskFinished(subject, isCompletedBeforeTimeout);
		}

		/// <summary>
		/// Tests that a task that times out is considered timed out.
		/// </summary>
		[Fact]
		public static async void TimesOut()
		{
			Task subject = Task.Run(async () => { await Task.Delay(LongDelay); });

			bool isCompletedBeforeTimeout = await subject.TimeoutAfterAsync(ShortDelay);

			AssertTimedOut(isCompletedBeforeTimeout);
		}

		/// <summary>
		/// Tests that a task that has already timed out is considered timed out.
		/// </summary>
		[Fact]
		public static async void GuaranteedTimeout()
		{
			Task subject = Task.Run(async () => { await Task.Delay(LongDelay); });

			bool isCompletedBeforeTimeout = await subject.TimeoutAfterAsync(TimeSpan.Zero);

			AssertTimedOut(isCompletedBeforeTimeout);
		}

		/// <summary>
		/// Tests that a task completed during an infinite timeout is considered completed before the timeout.
		/// </summary>
		[Fact]
		public static async void InfiniteTimeout()
		{
			Task subject = Task.Run(async () => { await Task.Delay(ShortDelay); });

			bool isCompletedBeforeTimeout = await subject.TimeoutAfterAsync(Timeout.InfiniteTimeSpan);

			AssertTaskFinished(subject, isCompletedBeforeTimeout);
		}

		/// <summary>
		/// Tests that a task given an already canceled timeout is considered timed out.
		/// </summary>
		[Fact]
		public static async void CanceledBeforeTimeout()
		{
			Task subject = Task.Run(async () => { await Task.Delay(ShortDelay); });
			CancellationTokenSource cancellationSource = new CancellationTokenSource();
			cancellationSource.Cancel();

			bool isCompletedBeforeTimeout = await subject.TimeoutAfterAsync(LongDelay, cancellationSource.Token);

			AssertTimedOut(isCompletedBeforeTimeout);
		}

		/// <summary>
		/// Tests that a task that's not completed before canceling the timeout is considered timed out.
		/// </summary>
		[Fact]
		public static async void CanceledDuringTimeout()
		{
			Task subject = Task.Run(async () => { await Task.Delay(LongDelay / 2); });
			CancellationTokenSource cancellationSource = new CancellationTokenSource();
			cancellationSource.CancelAfter(ShortDelay);

			bool isCompletedBeforeTimeout = await subject.TimeoutAfterAsync(LongDelay, cancellationSource.Token);

			AssertTimedOut(isCompletedBeforeTimeout);
		}

		/// <summary>
		/// Tests that a task given an infinite timeout that's canceled before the task completes is considered timed out.
		/// </summary>
		[Fact]
		public static async void InfiniteTimeoutAndCanceledDuringTimeout()
		{
			Task subject = Task.Run(async () => { await Task.Delay(LongDelay); });
			CancellationTokenSource cancellationSource = new CancellationTokenSource();
			cancellationSource.CancelAfter(ShortDelay);

			bool isCompletedBeforeTimeout = await subject.TimeoutAfterAsync(Timeout.InfiniteTimeSpan, cancellationSource.Token);

			AssertTimedOut(isCompletedBeforeTimeout);
		}

		/// <summary>
		/// Tests that an already completed task given an already canceled timeout before starting the timeout
		/// is considered completed before the timeout.
		/// </summary>
		[Fact]
		public static async void AlreadyCompletedAndTimeoutCanceledBeforeStarting()
		{
			Task subject = Task.Run(() => { });
			await subject;
			CancellationTokenSource cancellationSource = new CancellationTokenSource();
			cancellationSource.Cancel();

			bool isCompletedBeforeTimeout = await subject.TimeoutAfterAsync(LongDelay, cancellationSource.Token);

			AssertTaskFinished(subject, isCompletedBeforeTimeout);
		}

		#endregion

		#region Test Assertions

		/// <summary>
		/// Asserts that the task finished before the timeout expired.
		/// </summary>
		/// <param name="subject">The subject task.</param>
		/// <param name="isCompletedBeforeTimeout">A value indicating whether the task completed before the timeout.</param>
		private static void AssertTaskFinished(Task subject, bool isCompletedBeforeTimeout)
		{
			isCompletedBeforeTimeout.Should().Be(true);
			subject.IsCompleted.Should().Be(true);
		}

		/// <summary>
		/// Asserts that the task timed out.
		/// </summary>
		/// <param name="isCompletedBeforeTimeout">A value indicating whether the task completed before the timeout.</param>
		private static void AssertTimedOut(bool isCompletedBeforeTimeout)
		{
			// subject.IsCompleted is likely false but could be true (race condition)
			isCompletedBeforeTimeout.Should().Be(false);
		}

		#endregion
	}
}
