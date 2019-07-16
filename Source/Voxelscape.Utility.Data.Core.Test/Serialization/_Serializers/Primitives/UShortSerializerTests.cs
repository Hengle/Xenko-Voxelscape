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
	public static class UShortSerializerTests
	{
		public static IEnumerable<object[]> Values() =>
			new object[][]
			{
				new object[] { ushort.MinValue },
				new object[] { ushort.MaxValue },
				new object[] { 7 },
			};

		[Theory]
		[MemberData(nameof(Values))]
		public static void RunTests(ushort value) =>
			ConstantSerializerDeserializerTests.RunTests(Serializer.UShort, value, ByteLength.UShort);
	}
}
