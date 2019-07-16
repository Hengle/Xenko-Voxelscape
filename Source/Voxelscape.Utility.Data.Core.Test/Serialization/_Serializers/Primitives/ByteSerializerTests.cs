using System.Collections.Generic;
using Voxelscape.Utility.Data.Core.Serialization;
using Voxelscape.Utility.Data.Pact.Serialization;
using Voxelscape.Utility.Data.Pact.Test.Serialization;
using Xunit;

namespace Voxelscape.Utility.Data.Core.Test.Serialization
{
	public static class ByteSerializerTests
	{
		public static IEnumerable<object[]> Values() =>
			new object[][]
			{
				new object[] { byte.MinValue },
				new object[] { byte.MaxValue },
				new object[] { 7 },
				new object[] { 200 },
			};

		[Theory]
		[MemberData(nameof(Values))]
		public static void RunTests(byte value) =>
			ConstantSerializerDeserializerTests.RunTests(Serializer.Byte, value, ByteLength.Byte);
	}
}
