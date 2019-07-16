using FluentAssertions;
using Voxelscape.Common.Indexing.Core.Arrays;
using Voxelscape.Common.Indexing.Core.Enums;
using Voxelscape.Common.Indexing.Core.Indices;
using Xunit;

namespace Voxelscape.Common.Indexing.Core.Test.Arrays
{
	/// <summary>
	/// Experimental tests I wrote to help me out while updating some Indexable bounds contracts.
	/// </summary>
	public static class BoundsTests
	{
		/// <summary>
		/// Testing the bounds of a 1D array.
		/// </summary>
		/// <param name="length">The length of the array.</param>
		[Theory]
		[InlineData(1)]
		[InlineData(2)]
		[InlineData(3)]
		public static void TestingBoundsOfArray1D(int length)
		{
			Array1D<int> subject = new Array1D<int>(new Index1D(length));

			subject.GetLowerBound(Axis1D.X).Should().Be(0);
			subject.GetUpperBound(Axis1D.X).Should().Be(length - 1);
			subject.LowerBounds.Should().Be(new Index1D(0));
			subject.UpperBounds.Should().Be(new Index1D(length - 1));
			subject.Dimensions.Should().Be(new Index1D(length));
		}

		/// <summary>
		/// Testing the bounds of a 2D array.
		/// </summary>
		/// <param name="xLength">Length of the X dimension of the array.</param>
		/// <param name="yLength">Length of the Y dimension of the array.</param>
		[Theory]
		[InlineData(1, 1)]
		[InlineData(2, 2)]
		[InlineData(1, 2)]
		[InlineData(2, 1)]
		public static void TestingBoundsOfArray2D(int xLength, int yLength)
		{
			Array2D<int> subject = new Array2D<int>(new Index2D(xLength, yLength));

			subject.GetLowerBound(Axis2D.X).Should().Be(0);
			subject.GetUpperBound(Axis2D.X).Should().Be(xLength - 1);

			subject.GetLowerBound(Axis2D.Y).Should().Be(0);
			subject.GetUpperBound(Axis2D.Y).Should().Be(yLength - 1);

			subject.LowerBounds.Should().Be(new Index2D(0, 0));
			subject.UpperBounds.Should().Be(new Index2D(xLength - 1, yLength - 1));
			subject.Dimensions.Should().Be(new Index2D(xLength, yLength));
		}
	}
}
