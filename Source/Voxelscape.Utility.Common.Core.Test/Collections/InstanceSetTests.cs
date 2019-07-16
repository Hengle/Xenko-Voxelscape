using System.Collections.Generic;
using FluentAssertions;
using Voxelscape.Utility.Common.Core.Collections;
using Voxelscape.Utility.Common.Pact.Test.Collections;
using Xunit;

namespace Voxelscape.Utility.Common.Core.Test.Collections
{
	/// <summary>
	/// Tests for the <see cref="InstanceSet{T}"/> class.
	/// </summary>
	public static class InstanceSetTests
	{
		/// <summary>
		/// Tests that all interface implementations pass their respective test suites.
		/// </summary>
		[Fact]
		public static void InterfaceTests()
		{
			ICollectionTests.UsingStrings.RunTests(() => new InstanceSet<string>(EqualityComparer<string>.Default));
			ISetTests.UsingStrings.RunTests(() => new InstanceSet<string>(EqualityComparer<string>.Default));
		}

		/// <summary>
		/// Tests that null can't be added to the set.
		/// </summary>
		[Fact]
		public static void CantAddNull()
		{
			InstanceSet<string> subject = new InstanceSet<string>(EqualityComparer<string>.Default);

			bool result = subject.Add(null);

			result.Should().BeFalse();
			subject.Contains(null).Should().BeFalse();
		}

		/// <summary>
		/// Tests that GetInstanceOf retrieves the same instance that was passed into the set.
		/// </summary>
		[Fact]
		public static void GetInstanceOfRetrievesCorrectInstance()
		{
			InstanceSet<ISet<string>> subject = new InstanceSet<ISet<string>>(SetEqualityComparer<string>.Instance);

			ISet<string> setA = new HashSet<string>(new string[] { "A" });
			ISet<string> setABC = new HashSet<string>(new string[] { "A", "B", "C" });
			subject.Add(setA);
			subject.Add(setABC);

			subject.GetInstanceOf(new HashSet<string>(new string[] { "A" })).Should().BeSameAs(setA);
			subject.GetInstanceOf(new HashSet<string>(new string[] { "A", "B", "C" })).Should().BeSameAs(setABC);
		}

		/// <summary>
		/// Tests that TryGetInstanceOf retrieves the same instance that was passed into the set.
		/// </summary>
		[Fact]
		public static void TryGetInstanceOfRetrievesCorrectInstance()
		{
			InstanceSet<ISet<string>> subject = new InstanceSet<ISet<string>>(SetEqualityComparer<string>.Instance);

			ISet<string> setA = new HashSet<string>(new string[] { "A" });
			subject.Add(setA);

			ISet<string> result;
			bool wasRetrieved = subject.TryGetInstanceOf(new HashSet<string>(new string[] { "A" }), out result);

			wasRetrieved.Should().BeTrue();
			result.Should().BeSameAs(setA);
		}

		/// <summary>
		/// Tests that TryGetInstanceOf works correctly when there is no matching instance to retrieve.
		/// </summary>
		[Fact]
		public static void TryGetInstanceOfFailure()
		{
			InstanceSet<ISet<string>> subject = new InstanceSet<ISet<string>>(SetEqualityComparer<string>.Instance);

			ISet<string> result;
			bool wasRetrieved = subject.TryGetInstanceOf(new HashSet<string>(new string[] { "A" }), out result);

			wasRetrieved.Should().BeFalse();
			result.Should().BeNull();
		}

		/// <summary>
		/// Tests that TryGetInstanceOf works correctly when passed null for the instance to retrieve.
		/// </summary>
		[Fact]
		public static void TryGetInstanceOfNull()
		{
			InstanceSet<ISet<string>> subject = new InstanceSet<ISet<string>>(SetEqualityComparer<string>.Instance);

			ISet<string> result;
			bool wasRetrieved = subject.TryGetInstanceOf(null, out result);

			wasRetrieved.Should().BeFalse();
			result.Should().BeNull();
		}
	}
}
