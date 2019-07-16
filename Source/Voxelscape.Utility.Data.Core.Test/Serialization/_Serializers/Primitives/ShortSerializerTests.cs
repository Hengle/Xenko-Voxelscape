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
	public static class ShortSerializerTests
	{
		public static IEnumerable<object[]> Values() =>
			new object[][]
			{
				new object[] { short.MinValue },
				new object[] { short.MaxValue },
				new object[] { -7 },
				new object[] { 7 },
				new object[] { 0 },
			};

		[Theory]
		[MemberData(nameof(Values))]
		public static void RunTests(short value) =>
			ConstantSerializerDeserializerTests.RunTests(Serializer.Short, value, ByteLength.Short);
	}
}
