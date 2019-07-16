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
	public static class Index4DSerializerTests
	{
		private static readonly int ExpectedLength = ByteLength.Int * 4;

		public static IEnumerable<object[]> Values() =>
			new object[][]
			{
				new object[] { new Index4D(int.MinValue) },
				new object[] { new Index4D(int.MaxValue) },
				new object[] { new Index4D(-7, 13, 42, -1337) },
				new object[] { Index4D.Zero },
			};

		[Theory]
		[MemberData(nameof(Values))]
		public static void RunTests(Index4D value) =>
			ConstantSerializerDeserializerTests.RunTests(Index4DSerializer.Get, value, ExpectedLength);
	}
}
