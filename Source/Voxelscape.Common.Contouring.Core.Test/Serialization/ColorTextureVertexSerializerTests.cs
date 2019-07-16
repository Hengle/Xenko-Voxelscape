using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Voxelscape.Common.Contouring.Core.Serialization;
using Voxelscape.Common.Contouring.Core.Vertices;
using Voxelscape.Utility.Data.Pact.Test.Serialization;
using Xunit;

namespace Voxelscape.Common.Contouring.Core.Test.Serialization
{
	/// <summary>
	///
	/// </summary>
	public static class ColorTextureVertexSerializerTests
	{
		private static readonly int ExpectedLength = Marshal.SizeOf<NormalColorTextureVertex>();

		public static IEnumerable<object[]> Values() =>
			new object[][]
			{
				new object[] { new NormalColorTextureVertex(Vector3.Zero, Vector3.Zero, Color.White, Vector2.Zero) },
				new object[] { new NormalColorTextureVertex(Vector3.UnitX, Vector3.UnitZ, Color.Black, Vector2.UnitY) },
				new object[] { new NormalColorTextureVertex(-Vector3.UnitX, -Vector3.UnitZ, Color.Orange, -Vector2.UnitX) },
			};

		[Theory]
		[MemberData(nameof(Values))]
		public static void RunTests(NormalColorTextureVertex value) => ConstantSerializerDeserializerTests.RunTests(
			NormalColorTextureVertexSerializer.WithColorAlpha, value, ExpectedLength);
	}
}
