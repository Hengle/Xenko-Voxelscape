using FluentAssertions;
using Microsoft.Xna.Framework;
using Voxelscape.Common.MonoGame.Mathematics;
using Xunit;

namespace Voxelscape.Common.MonoGame.Test.Mathematics
{
	/// <summary>
	///
	/// </summary>
	public static class PolyMathTests
	{
		/* Imagine the polygon as;
		 * A
		 * | \
		 * B - C
		 * Vertices in order A, B, C are counterclockwise.
		 * Vertices in order A, C, B are clockwise.
		 * Which is clockwise/counterclockwise isn't actually important,
		 * all that matters is that they are opposite winding orders of each other.
		 */

		[Fact]
		public static void OppositeVertexWindingOrderHasOppositeSurfaceNormalDirection()
		{
			var a = new Vector3(1, 0, 0);
			var b = new Vector3(0, 1, 0);
			var c = new Vector3(0, 0, 1);

			var surfaceNormal = PolyMath.GetSurfaceNormal(a, b, c);
			var oppositeWindingSurfaceNormal = PolyMath.GetSurfaceNormal(a, c, b);

			surfaceNormal.Should().Be(oppositeWindingSurfaceNormal * -1);
		}

		[Fact]
		public static void WindingOrderDoesNotChangeArea()
		{
			var a = new Vector3(1, 0, 0);
			var b = new Vector3(0, 1, 0);
			var c = new Vector3(0, 0, 1);

			var area = PolyMath.GetArea(a, b, c);
			var oppositeWindingArea = PolyMath.GetArea(a, c, b);

			area.Should().Be(oppositeWindingArea);
		}

		[Fact]
		public static void WindingOrderDoesNotChangeMidpoint2D()
		{
			var a = new Vector2(1, 0);
			var b = new Vector2(0, 1);
			var c = new Vector2(1, 1);

			var midpoint = PolyMath.GetMidpoint(a, b, c);
			var oppositeWindingMidpoint = PolyMath.GetMidpoint(a, c, b);

			midpoint.Should().Be(oppositeWindingMidpoint);
		}

		[Fact]
		public static void WindingOrderDoesNotChangeMidpoint3D()
		{
			var a = new Vector3(1, 0, 0);
			var b = new Vector3(0, 1, 0);
			var c = new Vector3(0, 0, 1);

			var midpoint = PolyMath.GetMidpoint(a, b, c);
			var oppositeWindingMidpoint = PolyMath.GetMidpoint(a, c, b);

			midpoint.Should().Be(oppositeWindingMidpoint);
		}
	}
}
