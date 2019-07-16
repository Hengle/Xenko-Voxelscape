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
	public static class Vector3SerializerTests
	{
		private static readonly int ExpectedLength = ByteLength.Float * 3;

		public static IEnumerable<object[]> Values() =>
			new object[][]
			{
				new object[] { new Vector3(float.MinValue, float.MinValue, float.MinValue) },
				new object[] { new Vector3(float.MaxValue, float.MaxValue, float.MaxValue) },
				new object[] { Vector3.Zero },
				new object[] { new Vector3(7.13f, 20.4f, 13.9f) },
				new object[] { new Vector3(-7.13f, -20.4f, -13.9f) },
			};

		[Theory]
		[MemberData(nameof(Values))]
		public static void RunTests(Vector3 value) =>
			ConstantSerializerDeserializerTests.RunTests(Vector3Serializer.Get, value, ExpectedLength);
	}
}
