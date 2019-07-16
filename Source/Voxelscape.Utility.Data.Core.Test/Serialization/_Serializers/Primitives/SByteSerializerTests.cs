using System.Collections.Generic;
using Voxelscape.Utility.Data.Core.Serialization;
using Voxelscape.Utility.Data.Pact.Serialization;
using Voxelscape.Utility.Data.Pact.Test.Serialization;
using Xunit;

namespace Voxelscape.Utility.Data.Core.Test.Serialization
{
	public static class SByteSerializerTests
	{
		public static IEnumerable<object[]> Values() =>
			new object[][]
			{
				new object[] { sbyte.MinValue },
				new object[] { sbyte.MaxValue },
				new object[] { -7 },
				new object[] { 7 },
				new object[] { 0 },
			};

		[Theory]
		[MemberData(nameof(Values))]
		public static void RunTests(sbyte value) =>
			ConstantSerializerDeserializerTests.RunTests(Serializer.SByte, value, ByteLength.SByte);
	}
}
