using System.Collections.Generic;
using Voxelscape.Utility.Data.Core.Serialization;
using Voxelscape.Utility.Data.Pact.Serialization;
using Voxelscape.Utility.Data.Pact.Test.Serialization;
using Xunit;

namespace Voxelscape.Utility.Data.Core.Test.Serialization
{
	/// <summary>
	///
	/// </summary>
	public static class LongSerializerTests
	{
		public static IEnumerable<object[]> Values() =>
			new object[][]
			{
				new object[] { long.MinValue },
				new object[] { long.MaxValue },
				new object[] { -7 },
				new object[] { 7 },
				new object[] { 0 },
			};

		[Theory]
		[MemberData(nameof(Values))]
		public static void RunTests(long value) =>
			ConstantSerializerDeserializerTests.RunTests(Serializer.Long, value, ByteLength.Long);
	}
}
