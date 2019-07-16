using System.Linq;
using FluentAssertions;
using Voxelscape.Utility.Common.Core.Collections;
using Voxelscape.Utility.Common.Pact.Test.Collections;
using Xunit;

namespace Voxelscape.Utility.Common.Core.Test.Collections
{
	/// <summary>
	/// Tests for the <see cref="CountingCollection{T}"/> class.
	/// </summary>
	public static class CountingCollectionTests
	{
		/// <summary>
		/// Tests that all interface implementations pass their respective test suites.
		/// </summary>
		[Fact]
		public static void CollectionTests()
		{
			ICollectionTests.UsingStrings.RunTests(() => new CountingCollection<string>());
		}

		#region CountingCollection Specific Tests

		/// <summary>
		/// Tests that CountOf works.
		/// </summary>
		/// <param name="numberOfTimesToAdd">The number of times to add the same value.</param>
		/// <param name="numberOfTimesToRemove">The number of times to remove the same value.</param>
		[Theory]
		[InlineData(1, 0)]
		[InlineData(2, 0)]
		[InlineData(3, 0)]
		[InlineData(1, 1)]
		[InlineData(2, 1)]
		[InlineData(2, 2)]
		[InlineData(0, 0)]
		[InlineData(0, 1)]
		[InlineData(1, 2)]
		public static void CountOf(int numberOfTimesToAdd, int numberOfTimesToRemove)
		{
			CountingCollection<string> subject = new CountingCollection<string>();
			string value = "A";

			for (int count = 1; count <= numberOfTimesToAdd; count++)
			{
				subject.Add(value);

				// assert
				subject.CountOf(value).Should().Be(count);
			}

			for (int count = 1; count <= numberOfTimesToRemove; count++)
			{
				subject.Remove(value);

				// assert
				subject.CountOf(value).Should().Be((numberOfTimesToAdd - count).ClampLower(0));
			}
		}

		/// <summary>
		/// Tests that ContainsSameCountAs works.
		/// </summary>
		/// <param name="valuesToAddToFirstSubject">The values to add to the first subject.</param>
		/// <param name="valuesToAddToSecondSubject">The values to add to the second subject.</param>
		/// <param name="expectedOutcome">The expected outcome.</param>
		[Theory]
		[InlineData(new string[] { "A" }, new string[] { "A" }, true)]
		[InlineData(new string[] { "A", "B" }, new string[] { "A" }, false)]
		[InlineData(new string[] { "A", "A" }, new string[] { "A" }, false)]
		[InlineData(new string[] { "A", "B" }, new string[] { "B", "A" }, true)]
		[InlineData(new string[] { "A", "B", "A" }, new string[] { "B", "A", "A" }, true)]
		[InlineData(new string[] { "A", "A", "B", "B" }, new string[] { "A", "B", "A", "B" }, true)]
		[InlineData(new string[] { }, new string[] { }, true)]
		[InlineData(new string[] { "A" }, new string[] { }, false)]
		public static void ContainsSameCountAs(
			string[] valuesToAddToFirstSubject, string[] valuesToAddToSecondSubject, bool expectedOutcome)
		{
			CountingCollection<string> firstSubject = new CountingCollection<string>(valuesToAddToFirstSubject);
			CountingCollection<string> secondSubject = new CountingCollection<string>(valuesToAddToSecondSubject);

			// asserts
			firstSubject.ContainsSameCountAs(secondSubject).Should().Be(expectedOutcome);
			secondSubject.ContainsSameCountAs(firstSubject).Should().Be(expectedOutcome);

			firstSubject.ContainsSameCountAs(firstSubject).Should().BeTrue();
			secondSubject.ContainsSameCountAs(secondSubject).Should().BeTrue();
		}

		/// <summary>
		/// Tests that SubtractCountFrom works.
		/// </summary>
		/// <param name="valuesToAddToSubject">The values to add to the subject.</param>
		/// <param name="valuesToSubtract">The values to subtract from the subject.</param>
		/// <param name="expectedValues">The expected values in the subject after subtracting.</param>
		[Theory]
		[InlineData(new string[] { "A" }, new string[] { "A" }, new string[] { })]
		[InlineData(new string[] { "A" }, new string[] { "B" }, new string[] { "A" })]
		[InlineData(new string[] { "A", "A" }, new string[] { "A" }, new string[] { "A" })]
		[InlineData(new string[] { "A", "A", "A" }, new string[] { "A" }, new string[] { "A", "A" })]
		[InlineData(new string[] { }, new string[] { "A" }, new string[] { })]
		[InlineData(new string[] { "A", "B" }, new string[] { "A" }, new string[] { "B" })]
		[InlineData(new string[] { "A", "B" }, new string[] { "A", "B" }, new string[] { })]
		[InlineData(new string[] { "A", "A", "A" }, new string[] { "A", "A" }, new string[] { "A" })]
		public static void SubtractCountFrom(
			string[] valuesToAddToSubject, string[] valuesToSubtract, string[] expectedValues)
		{
			CountingCollection<string> subject = new CountingCollection<string>(valuesToAddToSubject);
			CountingCollection<string> subtract = new CountingCollection<string>(valuesToSubtract);
			CountingCollection<string> expected = new CountingCollection<string>(expectedValues);

			subject.SubtractCountFrom(subtract);

			// asserts
			subject.ContainsSameCountAs(expected).Should().BeTrue();
		}

		/// <summary>
		/// Tests that Expand works.
		/// </summary>
		/// <param name="valuesToAddToSubject">The values to add to the subject.</param>
		[Theory]
		[InlineData(new object[] { new string[] { "A" } })]
		[InlineData(new object[] { new string[] { "A", "B" } })]
		[InlineData(new object[] { new string[] { "A", "A", "B" } })]
		[InlineData(new object[] { new string[] { "A", "B", "A" } })]
		[InlineData(new object[] { new string[] { "A", "A", "B", "B" } })]
		[InlineData(new object[] { new string[] { "A", "B", "B", "C", "A" } })]
		public static void Expand(string[] valuesToAddToSubject)
		{
			// TODO Steven - for some reason this is the test from hell!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
			CountingCollection<string> subject = new CountingCollection<string>(valuesToAddToSubject);

			// asserts
			subject.Expand.Count().Should().Be(valuesToAddToSubject.Length);
			subject.Expand.ElementsEqualPerOccurrence(valuesToAddToSubject).Should().BeTrue();
		}

		#endregion
	}
}
