using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Voxelscape.Common.MonoGame.Serialization;
using Voxelscape.Utility.Data.Pact.Serialization;
using Voxelscape.Utility.Data.Pact.Test.Serialization;
using Xunit;

namespace Voxelscape.Common.MonoGame.Test.Serialization
{
	/// <summary>
	///
	/// </summary>
	public static class AlphaColorSerializerTests
	{
		private static readonly int ExpectedLength = ByteLength.Byte * 4;

		public static IEnumerable<object[]> Values() =>
			new object[][]
			{
				new object[] { new Color(0, 0, 0) },
				new object[] { Color.CadetBlue },
				new object[] { Color.IndianRed },
				new object[] { new Color(255, 0, 0) },
				new object[] { new Color(0, 255, 0) },
				new object[] { new Color(0, 0, 255) },
				new object[] { new Color(0, 0, 0, 0) },
				new object[] { new Color(0, 0, 0, 255) },
			};

		[Theory]
		[MemberData(nameof(Values))]
		public static void RunTests(Color value) =>
			ConstantSerializerDeserializerTests.RunTests(RGBAColorSerializer.Get, value, ExpectedLength);
	}
}
