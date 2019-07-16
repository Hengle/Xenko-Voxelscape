using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Utility.Data.Pact.Test.Serialization
{
	/// <summary>
	///
	/// </summary>
	public static class ConstantSerializerDeserializerTests
	{
		public static void RunTests<TSerializer, TValue>(
			IEndianProvider<TSerializer> subject, TValue value, int expectedSerializedLength)
			where TSerializer : IConstantSerializerDeserializer<TValue>
		{
			Contracts.Requires.That(subject != null);

			RunTests(subject.BigEndian, value, expectedSerializedLength);
			RunTests(subject.LittleEndian, value, expectedSerializedLength);
		}

		public static void RunTests<TValue>(
			IConstantSerializerDeserializer<TValue> subject, TValue value, int expectedSerializedLength)
		{
			Contracts.Requires.That(subject != null);

			ConstantSerializedLengthTests.RunTests(subject, expectedSerializedLength);
			SerializerDeserializerTests.RunTests(subject, value, expectedSerializedLength);
		}
	}
}
