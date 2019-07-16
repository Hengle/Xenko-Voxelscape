using System.Collections.Generic;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Core.Serialization;
using Voxelscape.Utility.Data.Pact.Serialization;
using Voxelscape.Utility.Data.Pact.Test.Serialization;
using Xunit;

namespace Voxelscape.Common.Indexing.Core.Test.Serialization
{
	/// <summary>
	///
	/// </summary>
	public static class Index3DSerializerTests
	{
		private static readonly int ExpectedLength = ByteLength.Int * 3;

		public static IEnumerable<object[]> Values() =>
			new object[][]
			{
				new object[] { new Index3D(int.MinValue) },
				new object[] { new Index3D(int.MaxValue) },
				new object[] { new Index3D(-7, 13, 42) },
				new object[] { Index3D.Zero },
			};

		[Theory]
		[MemberData(nameof(Values))]
		public static void RunTests(Index3D value) =>
			ConstantSerializerDeserializerTests.RunTests(Index3DSerializer.Get, value, ExpectedLength);
	}
}
