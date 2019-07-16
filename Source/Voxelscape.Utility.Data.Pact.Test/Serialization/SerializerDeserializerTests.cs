using System.Collections.Generic;
using FluentAssertions;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Core.Serialization;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Utility.Data.Pact.Test.Serialization
{
	/// <summary>
	///
	/// </summary>
	public static class SerializerDeserializerTests
	{
		public static void RunTests<TSerializer, TValue>(
			IEndianProvider<TSerializer> subject, TValue value, int expectedSerializedLength)
			where TSerializer : ISerializerDeserializer<TValue>
		{
			Contracts.Requires.That(subject != null);

			RunTests(subject.BigEndian, value, expectedSerializedLength);
			RunTests(subject.LittleEndian, value, expectedSerializedLength);
		}

		public static void RunTests<TValue>(
			ISerializerDeserializer<TValue> subject, TValue value, int expectedSerializedLength)
		{
			Contracts.Requires.That(subject != null);

			GetSerializedLength(subject, value, expectedSerializedLength);
			SerializeToArray(subject, value, expectedSerializedLength);
			SerializeToArrayMidway(subject, value, expectedSerializedLength);
			SerializeWriteAction(subject, value, expectedSerializedLength);
			DeserializeFromBufferedArray(subject, value);
		}

		private static void GetSerializedLength<TValue>(
			ISerializerDeserializer<TValue> subject, TValue value, int expectedSerializedLength)
		{
			Contracts.Requires.That(subject != null);

			subject.GetSerializedLength(value).Should().Be(expectedSerializedLength);
		}

		private static void SerializeToArray<TValue>(
			ISerializerDeserializer<TValue> subject, TValue value, int expectedSerializedLength)
		{
			Contracts.Requires.That(subject != null);

			var dataBuffer = subject.Serialize(value);
			dataBuffer.Length.Should().Be(expectedSerializedLength);
			subject.Deserialize(dataBuffer).Should().Be(value);
		}

		private static void SerializeToArrayMidway<TValue>(
			ISerializerDeserializer<TValue> subject, TValue value, int expectedSerializedLength)
		{
			SerializeToArrayMidway(subject, value, 0, expectedSerializedLength);
			SerializeToArrayMidway(subject, value, 1, expectedSerializedLength);
			SerializeToArrayMidway(subject, value, 2, expectedSerializedLength);
		}

		private static void SerializeToArrayMidway<TValue>(
			ISerializerDeserializer<TValue> subject, TValue value, int startingIndex, int expectedSerializedLength)
		{
			Contracts.Requires.That(subject != null);

			var dataBuffer = new byte[subject.GetSerializedLength(value) + startingIndex];
			int serializedIndex = startingIndex;

			// serialize the object and check the serialized length of the object
			var serializedLength = subject.Serialize(value, dataBuffer, ref serializedIndex);

			serializedLength.Should().Be(expectedSerializedLength);

			// index should have moved to one past the last index the object was serialized into
			serializedIndex.Should().Be(startingIndex + serializedLength);

			// difference between starting and ending index should be the serialized length of the object
			(serializedIndex - startingIndex).Should().Be(serializedLength);

			int deserializedIndex = startingIndex;

			// deserialized object should be logically equivalent to the original object
			var deserializedValue = subject.Deserialize(dataBuffer, ref deserializedIndex);

			deserializedValue.Should().Be(value);

			// index should have moved to one past the last index the object was deserialized from
			deserializedIndex.Should().Be(startingIndex + serializedLength);

			// difference between starting and ending index should be the serialized length of the object
			(deserializedIndex - startingIndex).Should().Be(serializedLength);
		}

		private static void SerializeWriteAction<TValue>(
			ISerializerDeserializer<TValue> subject, TValue value, int expectedSerializedLength)
		{
			Contracts.Requires.That(subject != null);

			var dataBuffer = new List<byte>(subject.GetSerializedLength(value));
			subject.Serialize(value, dataBuffer.Add).Should().Be(expectedSerializedLength);
			dataBuffer.Count.Should().Be(expectedSerializedLength);
			subject.Deserialize(new BufferedArray(dataBuffer.AsCounted())).Should().Be(value);
		}

		private static void DeserializeFromBufferedArray<TValue>(
			ISerializerDeserializer<TValue> subject, TValue value)
		{
			Contracts.Requires.That(subject != null);

			var dataBuffer = new BufferedArray(subject.Serialize(value).AsCounted());
			subject.Deserialize(dataBuffer).Should().Be(value);
		}
	}
}
