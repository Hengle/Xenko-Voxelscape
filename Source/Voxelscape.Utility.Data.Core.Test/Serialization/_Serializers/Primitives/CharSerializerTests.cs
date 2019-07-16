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
	public static class CharSerializerTests
	{
		public static IEnumerable<object[]> Values() =>
			new object[][]
			{
				new object[] { char.MinValue },
				new object[] { char.MaxValue },
				new object[] { 'c' },
			};

		[Theory]
		[MemberData(nameof(Values))]
		public static void RunTests(char value) =>
			ConstantSerializerDeserializerTests.RunTests(Serializer.Char, value, ByteLength.Char);
	}
}
