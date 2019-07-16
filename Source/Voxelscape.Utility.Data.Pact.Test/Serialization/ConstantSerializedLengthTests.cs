using FluentAssertions;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Utility.Data.Pact.Test.Serialization
{
	/// <summary>
	///
	/// </summary>
	public static class ConstantSerializedLengthTests
	{
		public static void RunTests<TSerializer>(IEndianProvider<TSerializer> subject, int expectedSerializedLength)
			where TSerializer : IConstantSerializedLength
		{
			Contracts.Requires.That(subject != null);

			RunTests(subject.BigEndian, expectedSerializedLength);
			RunTests(subject.LittleEndian, expectedSerializedLength);
		}

		public static void RunTests(IConstantSerializedLength subject, int expectedSerializedLength)
		{
			Contracts.Requires.That(subject != null);

			SerializedLength(subject, expectedSerializedLength);
		}

		private static void SerializedLength(IConstantSerializedLength subject, int expectedSerializedLength)
		{
			Contracts.Requires.That(subject != null);

			subject.SerializedLength.Should().Be(expectedSerializedLength);
		}
	}
}
