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
	public static class DoubleSerializerTests
	{
		public static IEnumerable<object[]> Values() =>
			new object[][]
			{
				new object[] { double.MinValue },
				new object[] { double.MaxValue },
				new object[] { -7.13 },
				new object[] { 7.13 },
				new object[] { 0 },
			};

		[Theory]
		[MemberData(nameof(Values))]
		public static void RunTests(double value) =>
			ConstantSerializerDeserializerTests.RunTests(Serializer.Double, value, ByteLength.Double);
	}
}
