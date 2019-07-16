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
	public static class UIntSerializerTests
	{
		public static IEnumerable<object[]> Values() =>
			new object[][]
			{
				new object[] { uint.MinValue },
				new object[] { uint.MaxValue },
				new object[] { 7 },
			};

		[Theory]
		[MemberData(nameof(Values))]
		public static void RunTests(uint value) =>
			ConstantSerializerDeserializerTests.RunTests(Serializer.UInt, value, ByteLength.UInt);
	}
}
