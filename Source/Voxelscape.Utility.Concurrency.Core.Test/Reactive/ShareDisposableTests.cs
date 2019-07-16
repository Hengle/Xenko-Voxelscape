using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using FluentAssertions;
using Voxelscape.Utility.Common.Core.Disposables;
using Voxelscape.Utility.Common.Pact.Disposables;
using Xunit;

namespace Voxelscape.Utility.Concurrency.Core.Test.Reactive
{
	/// <summary>
	///
	/// </summary>
	public static class ShareDisposableTests
	{
		[Fact]
		public static void ManySubcribers()
		{
			using (var subject = new Subject<IDisposableValue<int>>())
			{
				var testSubject = subject.ShareDisposable();

				var testNumber = 1;
				var sourceValue = new DisposableWrapper<int>(testNumber);
				sourceValue.IsDisposed.Should().BeFalse();

				IDisposableValue<int> retrieved1 = null;
				testSubject.Subscribe(value => retrieved1 = value);
				IDisposableValue<int> retrieved2 = null;
				testSubject.Subscribe(value => retrieved2 = value);

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
		public static void ManySubcribersDisposeQuickly()
		{
			using (var subject = new Subject<IDisposableValue<int>>())
			{
				var testSubject = subject.ShareDisposable();

				var testNumber = 1;
				var sourceValue = new DisposableWrapper<int>(testNumber);
				sourceValue.IsDisposed.Should().BeFalse();

				testSubject.Subscribe(value =>
				{
					value.IsDisposed.Should().BeFalse();
					value.Value.Should().Be(testNumber);
					value.Dispose();
				});

				testSubject.Subscribe(value =>
				{
					value.IsDisposed.Should().BeFalse();
					value.Value.Should().Be(testNumber);
					value.Dispose();
				});

				subject.OnNext(sourceValue);

				subject.OnCompleted();
			}
		}
	}
}
