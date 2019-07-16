using System;
using System.Reactive.Linq;
using FluentAssertions;
using Voxelscape.Utility.Concurrency.Core.Reactive;
using Xunit;

namespace Voxelscape.Utility.Concurrency.Core.Test.Reactive
{
	/// <summary>
	///
	/// </summary>
	public static class SubscriptionCounterTests
	{
		[Fact]
		public static void Count()
		{
			var source = Observable.Never<int>();
			var counter = new SubscriptionCounter<int>(source);
			counter.SubscriberCount.Should().Be(0);

			var subscription = counter.CountedSource.Subscribe();
			counter.SubscriberCount.Should().Be(1);

			var subscription2 = counter.CountedSource.Subscribe();
			counter.SubscriberCount.Should().Be(2);

			subscription.Dispose();
			counter.SubscriberCount.Should().Be(1);

			subscription2.Dispose();
			counter.SubscriberCount.Should().Be(0);
		}
	}
}
