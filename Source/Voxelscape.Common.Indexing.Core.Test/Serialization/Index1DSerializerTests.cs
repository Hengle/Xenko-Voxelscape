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
	public static class Index1DSerializerTests
	{
		private static readonly int ExpectedLength = ByteLength.Int;

		public static IEnumerable<object[]> Values() =>
			new object[][]
			{
				new object[] { new Index1D(int.MinValue) },
				new object[] { new Index1D(int.MaxValue) },
				new object[] { new Index1D(-7) },
				new object[] { new Index1D(7) },
				new object[] { Index1D.Zero },
			};

		[Theory]
		[MemberData(nameof(Values))]
		public static void RunTests(Index1D value) =>
			ConstantSerializerDeserializerTests.RunTests(Index1DSerializer.Get, value, ExpectedLength);
	}
}
