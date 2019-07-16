using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Core.Serialization;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Utility.Data.Core.Test.Serialization
{
	/// <summary>
	///
	/// </summary>
	public static class ByteStreamingTests
	{
		public static void SerializeAndDeserialize<TSerializer, TValue>(
			IEndianProvider<TSerializer> provider, IEnumerable<TValue> values)
			where TSerializer : ISerializerDeserializer<TValue>
		{
			Contracts.Requires.That(provider != null);

			SerializeAndDeserialize(provider.BigEndian, values);
			SerializeAndDeserialize(provider.LittleEndian, values);
		}

		public static void SerializeAndDeserialize<T>(ISerializerDeserializer<T> serializer, IEnumerable<T> values)
		{
			Contracts.Requires.That(serializer != null);
			Contracts.Requires.That(values.AllAndSelfNotNull());

			var initialCapacity = 0;
			var stream = new MemoryStream(initialCapacity);
			var writer = new ByteStreamSerializer<T>(stream, serializer, initialCapacity);

			foreach (var value in values)
			{
				writer.SerializeToStream(value);
			}

			// move back to the start of the stream to start reading from
			stream.Position = 0;
			var buffer = new BufferedByteStream(stream);

			foreach (var value in values)
			{
				serializer.Deserialize(buffer).Should().Be(value);
			}
		}
	}
}
