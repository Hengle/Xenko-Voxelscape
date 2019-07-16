using FluentAssertions;
using Voxelscape.Utility.Common.Pact.Mathematics;
using Xunit;

namespace Voxelscape.Utility.Common.Pact.Test.Mathematics
{
	/// <summary>
	///
	/// </summary>
	public static class MathUtilitiesTests
	{
		[Theory]
		[InlineData(-1, 1, true, 0)]
		[InlineData(-1, 1, false, 0)]
		[InlineData(1, 3, true, 2)]
		[InlineData(1, 3, false, 2)]
		[InlineData(-1, -3, true, -2)]
		[InlineData(-1, -3, false, -2)]
		[InlineData(2, 2, true, 2)]
		[InlineData(2, 2, false, 2)]
		[InlineData(-2, -2, true, -2)]
		[InlineData(-2, -2, false, -2)]
		[InlineData(0, 0, true, 0)]
		[InlineData(0, 0, false, 0)]
		[InlineData(-1, 2, true, 1)]
		[InlineData(-1, 2, false, 0)]
		[InlineData(-2, 1, true, 0)]
		[InlineData(-2, 1, false, -1)]
		[InlineData(1, 2, true, 2)]
		[InlineData(1, 2, false, 1)]
		[InlineData(1, 4, true, 3)]
		[InlineData(1, 4, false, 2)]
		[InlineData(-1, -2, true, -1)]
		[InlineData(-1, -2, false, -2)]
		[InlineData(-1, -4, true, -2)]
		[InlineData(-1, -4, false, -3)]
		public static void IntegerMidpoint(int value1, int value2, bool roundUp, int expectedResult)
		{
			var result = MathUtilities.IntegerMidpoint(value1, value2, roundUp);
			result.Should().Be(expectedResult);
		}
	}
}
