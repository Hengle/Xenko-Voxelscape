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
	public static class Vector2SerializerTests
	{
		private static readonly int ExpectedLength = ByteLength.Float * 2;

		public static IEnumerable<object[]> Values() =>
			new object[][]
			{
				new object[] { new Vector2(float.MinValue, float.MinValue) },
				new object[] { new Vector2(float.MaxValue, float.MaxValue) },
				new object[] { Vector2.Zero },
				new object[] { new Vector2(7.13f, 20.4f) },
				new object[] { new Vector2(-7.13f, -20.4f) },
			};

		[Theory]
		[MemberData(nameof(Values))]
		public static void RunTests(Vector2 value) =>
			ConstantSerializerDeserializerTests.RunTests(Vector2Serializer.Get, value, ExpectedLength);
	}
}
