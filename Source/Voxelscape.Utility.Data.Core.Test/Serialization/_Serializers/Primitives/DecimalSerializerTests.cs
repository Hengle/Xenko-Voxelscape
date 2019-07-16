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
	public static class DecimalSerializerTests
	{
		public static IEnumerable<object[]> Values() =>
			new object[][]
			{
				new object[] { decimal.MinValue },
				new object[] { decimal.MaxValue },
				new object[] { -7.13m },
				new object[] { 7.13m },
				new object[] { 0m },
			};

		[Theory]
		[MemberData(nameof(Values))]
		public static void RunTests(decimal value) =>
			ConstantSerializerDeserializerTests.RunTests(Serializer.Decimal, value, ByteLength.Decimal);
	}
}
