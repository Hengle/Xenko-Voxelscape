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
	public static class StringSerializerTests
	{
		public static IEnumerable<object[]> Values() =>
			new object[][]
			{
				new object[] { string.Empty },
				new object[] { "a" },
				new object[] { "abc" },
			};

		public static int GetLengthInclude(string value) => (value.Length * ByteLength.Char) + ByteLength.Int;

		public static int GetLengthInferOrFixed(string value) => value.Length * ByteLength.Char;

		[Theory]
		[MemberData(nameof(Values))]
		public static void IncludeLengthTests(string value) => SerializerDeserializerTests.RunTests(
			StringSerializer.IncludeLength, value, GetLengthInclude(value));

		[Theory]
		[MemberData(nameof(Values))]
		public static void InferLengthTests(string value) => SerializerDeserializerTests.RunTests(
			StringSerializer.InferLength, value, GetLengthInferOrFixed(value));

		[Theory]
		[MemberData(nameof(Values))]
		public static void FixedLengthTests(string value) => SerializerDeserializerTests.RunTests(
			StringSerializer.FixedLength(value.Length), value, GetLengthInferOrFixed(value));
	}
}
