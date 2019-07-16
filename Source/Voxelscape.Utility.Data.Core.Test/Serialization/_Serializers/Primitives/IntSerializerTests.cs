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
	public static class IntSerializerTests
	{
		public static IEnumerable<object[]> Values() =>
			new object[][]
			{
				new object[] { int.MinValue },
				new object[] { int.MaxValue },
				new object[] { -7 },
				new object[] { 7 },
				new object[] { 0 },
			};

		[Theory]
		[MemberData(nameof(Values))]
		public static void RunTests(int value) =>
			ConstantSerializerDeserializerTests.RunTests(Serializer.Int, value, ByteLength.Int);
	}
}
