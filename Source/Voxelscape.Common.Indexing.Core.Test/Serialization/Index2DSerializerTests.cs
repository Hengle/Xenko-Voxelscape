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
	public static class Index2DSerializerTests
	{
		private static readonly int ExpectedLength = ByteLength.Int * 2;

		public static IEnumerable<object[]> Values() =>
			new object[][]
			{
				new object[] { new Index2D(int.MinValue) },
				new object[] { new Index2D(int.MaxValue) },
				new object[] { new Index2D(-7, 13) },
				new object[] { Index2D.Zero },
			};

		[Theory]
		[MemberData(nameof(Values))]
		public static void RunTests(Index2D value) =>
			ConstantSerializerDeserializerTests.RunTests(Index2DSerializer.Get, value, ExpectedLength);
	}
}
