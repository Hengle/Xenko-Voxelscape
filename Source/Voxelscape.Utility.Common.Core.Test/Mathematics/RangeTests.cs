using System.Collections.Generic;
using FluentAssertions;
using Voxelscape.Utility.Common.Core.Collections;
using Voxelscape.Utility.Common.Core.Mathematics;
using Xunit;

namespace Voxelscape.Utility.Common.Core.Test.Mathematics
{
	/// <summary>
	///
	/// </summary>
	public static class RangeTests
	{
		[Fact]
		public static void ContainsValue()
		{
			// in range
			foreach (var bounds in AllClusivities())
			{
				Range.New(0, 2, bounds).Contains(1).Should().BeTrue();
			}

			// below range
			foreach (var bounds in AllClusivities())
			{
				Range.New(0, 2, bounds).Contains(-1).Should().BeFalse();
			}

			// above range
			foreach (var bounds in AllClusivities())
			{
				Range.New(0, 2, bounds).Contains(3).Should().BeFalse();
			}

			// on min boundary of range
			Range.New(0, 1, RangeClusivity.Inclusive).Contains(0).Should().BeTrue();
			Range.New(0, 1, RangeClusivity.InclusiveMin).Contains(0).Should().BeTrue();
			Range.New(0, 1, RangeClusivity.InclusiveMax).Contains(0).Should().BeFalse();
			Range.New(0, 1, RangeClusivity.Exclusive).Contains(0).Should().BeFalse();

			// on max boundary of range
			Range.New(0, 1, RangeClusivity.Inclusive).Contains(1).Should().BeTrue();
			Range.New(0, 1, RangeClusivity.InclusiveMin).Contains(1).Should().BeFalse();
			Range.New(0, 1, RangeClusivity.InclusiveMax).Contains(1).Should().BeTrue();
			Range.New(0, 1, RangeClusivity.Exclusive).Contains(1).Should().BeFalse();
		}

		[Fact]
		public static void ContainsRange()
		{
			// completely below
			foreach (var bounds in AllClusivityCombinations())
			{
				Range.New(0, 3, bounds.Item1).Contains(Range.New(-2, -1, bounds.Item2)).Should().BeFalse();
			}

			// completely below
			foreach (var bounds in AllClusivityCombinations())
			{
				Range.New(0, 3, bounds.Item1).Contains(Range.New(4, 5, bounds.Item2)).Should().BeFalse();
			}

			// overlapping below
			foreach (var bounds in AllClusivityCombinations())
			{
				Range.New(0, 3, bounds.Item1).Contains(Range.New(-1, 1, bounds.Item2)).Should().BeFalse();
			}

			// overlapping above
			foreach (var bounds in AllClusivityCombinations())
			{
				Range.New(0, 3, bounds.Item1).Contains(Range.New(2, 4, bounds.Item2)).Should().BeFalse();
			}

			// completely overlapping
			foreach (var bounds in AllClusivityCombinations())
			{
				Range.New(0, 3, bounds.Item1).Contains(Range.New(-1, 4, bounds.Item2)).Should().BeFalse();
			}

			// completely contains, no overlapping min/max
			foreach (var bounds in AllClusivityCombinations())
			{
				Range.New(0, 3, bounds.Item1).Contains(Range.New(1, 2, bounds.Item2)).Should().BeTrue();
			}

			// same min/max, different clusivities
			Range.New(0, 3, RangeClusivity.Inclusive).Contains(Range.New(0, 3, RangeClusivity.Inclusive)).Should().BeTrue();
			Range.New(0, 3, RangeClusivity.Inclusive).Contains(Range.New(0, 3, RangeClusivity.InclusiveMin)).Should().BeTrue();
			Range.New(0, 3, RangeClusivity.Inclusive).Contains(Range.New(0, 3, RangeClusivity.InclusiveMax)).Should().BeTrue();
			Range.New(0, 3, RangeClusivity.Inclusive).Contains(Range.New(0, 3, RangeClusivity.Exclusive)).Should().BeTrue();

			Range.New(0, 3, RangeClusivity.InclusiveMin).Contains(Range.New(0, 3, RangeClusivity.Inclusive)).Should().BeFalse();
			Range.New(0, 3, RangeClusivity.InclusiveMin).Contains(Range.New(0, 3, RangeClusivity.InclusiveMin)).Should().BeTrue();
			Range.New(0, 3, RangeClusivity.InclusiveMin).Contains(Range.New(0, 3, RangeClusivity.InclusiveMax)).Should().BeFalse();
			Range.New(0, 3, RangeClusivity.InclusiveMin).Contains(Range.New(0, 3, RangeClusivity.Exclusive)).Should().BeTrue();

			Range.New(0, 3, RangeClusivity.InclusiveMax).Contains(Range.New(0, 3, RangeClusivity.Inclusive)).Should().BeFalse();
			Range.New(0, 3, RangeClusivity.InclusiveMax).Contains(Range.New(0, 3, RangeClusivity.InclusiveMin)).Should().BeFalse();
			Range.New(0, 3, RangeClusivity.InclusiveMax).Contains(Range.New(0, 3, RangeClusivity.InclusiveMax)).Should().BeTrue();
			Range.New(0, 3, RangeClusivity.InclusiveMax).Contains(Range.New(0, 3, RangeClusivity.Exclusive)).Should().BeTrue();

			Range.New(0, 3, RangeClusivity.Exclusive).Contains(Range.New(0, 3, RangeClusivity.Inclusive)).Should().BeFalse();
			Range.New(0, 3, RangeClusivity.Exclusive).Contains(Range.New(0, 3, RangeClusivity.InclusiveMin)).Should().BeFalse();
			Range.New(0, 3, RangeClusivity.Exclusive).Contains(Range.New(0, 3, RangeClusivity.InclusiveMax)).Should().BeFalse();
			Range.New(0, 3, RangeClusivity.Exclusive).Contains(Range.New(0, 3, RangeClusivity.Exclusive)).Should().BeTrue();
		}

		[Fact]
		public static void Overlaps()
		{
			// same min and max (overlaps)
			foreach (var bounds in AllClusivityCombinations())
			{
				Range.New(0, 1, bounds.Item1).Overlaps(Range.New(0, 1, bounds.Item2)).Should().BeTrue();
			}

			// min and max don't overlap at all
			foreach (var bounds in AllClusivityCombinations())
			{
				Range.New(0, 1, bounds.Item1).Overlaps(Range.New(2, 3, bounds.Item2)).Should().BeFalse();
				Range.New(2, 3, bounds.Item1).Overlaps(Range.New(0, 1, bounds.Item2)).Should().BeFalse();
			}

			foreach (var bounds in AllClusivityCombinations())
			{
				// max of first less than min of second (overlaps)
				Range.New(0, 2, bounds.Item1).Overlaps(Range.New(1, 3, bounds.Item2)).Should().BeTrue();

				// min of first less than max of second (overlaps)
				Range.New(1, 3, bounds.Item1).Overlaps(Range.New(0, 2, bounds.Item2)).Should().BeTrue();
			}

			// max of first equals min of second and are inclusive (overlaps)
			Range.New(0, 1, RangeClusivity.Inclusive).Overlaps(Range.New(1, 2, RangeClusivity.Inclusive)).Should().BeTrue();
			Range.New(0, 1, RangeClusivity.InclusiveMax).Overlaps(Range.New(1, 2, RangeClusivity.Inclusive)).Should().BeTrue();
			Range.New(0, 1, RangeClusivity.Inclusive).Overlaps(Range.New(1, 2, RangeClusivity.InclusiveMin)).Should().BeTrue();
			Range.New(0, 1, RangeClusivity.InclusiveMax).Overlaps(Range.New(1, 2, RangeClusivity.InclusiveMin)).Should().BeTrue();

			// max of first equals min of second but one or both are exclusive (no overlaps)
			Range.New(0, 1, RangeClusivity.Exclusive).Overlaps(Range.New(1, 2, RangeClusivity.Inclusive)).Should().BeFalse();
			Range.New(0, 1, RangeClusivity.Exclusive).Overlaps(Range.New(1, 2, RangeClusivity.Exclusive)).Should().BeFalse();
			Range.New(0, 1, RangeClusivity.Exclusive).Overlaps(Range.New(1, 2, RangeClusivity.InclusiveMin)).Should().BeFalse();
			Range.New(0, 1, RangeClusivity.Exclusive).Overlaps(Range.New(1, 2, RangeClusivity.InclusiveMax)).Should().BeFalse();
			Range.New(0, 1, RangeClusivity.InclusiveMin).Overlaps(Range.New(1, 2, RangeClusivity.Inclusive)).Should().BeFalse();
			Range.New(0, 1, RangeClusivity.InclusiveMin).Overlaps(Range.New(1, 2, RangeClusivity.Exclusive)).Should().BeFalse();
			Range.New(0, 1, RangeClusivity.InclusiveMin).Overlaps(Range.New(1, 2, RangeClusivity.InclusiveMin)).Should().BeFalse();
			Range.New(0, 1, RangeClusivity.InclusiveMin).Overlaps(Range.New(1, 2, RangeClusivity.InclusiveMax)).Should().BeFalse();
			Range.New(0, 1, RangeClusivity.Inclusive).Overlaps(Range.New(1, 2, RangeClusivity.Exclusive)).Should().BeFalse();
			Range.New(0, 1, RangeClusivity.Inclusive).Overlaps(Range.New(1, 2, RangeClusivity.InclusiveMax)).Should().BeFalse();
			Range.New(0, 1, RangeClusivity.InclusiveMax).Overlaps(Range.New(1, 2, RangeClusivity.Exclusive)).Should().BeFalse();
			Range.New(0, 1, RangeClusivity.InclusiveMax).Overlaps(Range.New(1, 2, RangeClusivity.InclusiveMax)).Should().BeFalse();

			// max of first equals min of second and are inclusive (overlaps)
			Range.New(1, 2, RangeClusivity.Inclusive).Overlaps(Range.New(0, 1, RangeClusivity.Inclusive)).Should().BeTrue();
			Range.New(1, 2, RangeClusivity.Inclusive).Overlaps(Range.New(0, 1, RangeClusivity.InclusiveMax)).Should().BeTrue();
			Range.New(1, 2, RangeClusivity.InclusiveMin).Overlaps(Range.New(0, 1, RangeClusivity.Inclusive)).Should().BeTrue();
			Range.New(1, 2, RangeClusivity.InclusiveMin).Overlaps(Range.New(0, 1, RangeClusivity.InclusiveMax)).Should().BeTrue();

			// max of first equals min of second but one or both are exclusive (no overlaps)
			Range.New(1, 2, RangeClusivity.InclusiveMax).Overlaps(Range.New(0, 1, RangeClusivity.Inclusive)).Should().BeFalse();
			Range.New(1, 2, RangeClusivity.InclusiveMax).Overlaps(Range.New(0, 1, RangeClusivity.InclusiveMin)).Should().BeFalse();
			Range.New(1, 2, RangeClusivity.InclusiveMax).Overlaps(Range.New(0, 1, RangeClusivity.InclusiveMax)).Should().BeFalse();
			Range.New(1, 2, RangeClusivity.InclusiveMax).Overlaps(Range.New(0, 1, RangeClusivity.Exclusive)).Should().BeFalse();
			Range.New(1, 2, RangeClusivity.Exclusive).Overlaps(Range.New(0, 1, RangeClusivity.Inclusive)).Should().BeFalse();
			Range.New(1, 2, RangeClusivity.Exclusive).Overlaps(Range.New(0, 1, RangeClusivity.InclusiveMin)).Should().BeFalse();
			Range.New(1, 2, RangeClusivity.Exclusive).Overlaps(Range.New(0, 1, RangeClusivity.InclusiveMax)).Should().BeFalse();
			Range.New(1, 2, RangeClusivity.Exclusive).Overlaps(Range.New(0, 1, RangeClusivity.Exclusive)).Should().BeFalse();

			Range.New(1, 2, RangeClusivity.Inclusive).Overlaps(Range.New(0, 1, RangeClusivity.InclusiveMin)).Should().BeFalse();
			Range.New(1, 2, RangeClusivity.Inclusive).Overlaps(Range.New(0, 1, RangeClusivity.Exclusive)).Should().BeFalse();
			Range.New(1, 2, RangeClusivity.InclusiveMin).Overlaps(Range.New(0, 1, RangeClusivity.InclusiveMin)).Should().BeFalse();
			Range.New(1, 2, RangeClusivity.InclusiveMin).Overlaps(Range.New(0, 1, RangeClusivity.Exclusive)).Should().BeFalse();
		}

		private static IEnumerable<TupleStruct<RangeClusivity, RangeClusivity>> AllClusivityCombinations()
		{
			foreach (var first in AllClusivities())
			{
				foreach (var second in AllClusivities())
				{
					yield return TupleStruct.Create(first, second);
				}
			}
		}

		private static IEnumerable<RangeClusivity> AllClusivities()
		{
			yield return RangeClusivity.Inclusive;
			yield return RangeClusivity.InclusiveMin;
			yield return RangeClusivity.InclusiveMax;
			yield return RangeClusivity.Exclusive;
		}
	}
}
