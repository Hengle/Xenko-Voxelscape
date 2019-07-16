using System.Collections.Generic;
using Voxelscape.Utility.Data.Core.Serialization;
using Voxelscape.Utility.Data.Pact.Serialization;
using Voxelscape.Utility.Data.Pact.Test.Serialization;
using Xunit;

namespace Voxelscape.Utility.Data.Core.Test.Serialization
{
	public class BoolSerializerTests
	{
		public static IEnumerable<object[]> Values() =>
			new object[][]
			{
				new object[] { true },
				new object[] { false },
			};

		[Theory]
		[MemberData(nameof(Values))]
		public static void RunTests(bool value) =>
			ConstantSerializerDeserializerTests.RunTests(Serializer.Bool, value, ByteLength.Bool);
	}
}
