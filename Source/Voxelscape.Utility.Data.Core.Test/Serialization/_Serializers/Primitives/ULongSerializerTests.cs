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
	public static class ULongSerializerTests
	{
		public static IEnumerable<object[]> Values() =>
			new object[][]
			{
				new object[] { ulong.MinValue },
				new object[] { ulong.MaxValue },
				new object[] { 7 },
			};

		[Theory]
		[MemberData(nameof(Values))]
		public static void RunTests(ulong value) =>
			ConstantSerializerDeserializerTests.RunTests(Serializer.ULong, value, ByteLength.ULong);
	}
}
