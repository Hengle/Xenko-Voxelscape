using System;
using Voxelscape.Utility.Data.Core.Serialization;
using Xunit;

namespace Voxelscape.Utility.Data.Core.Test.Serialization
{
	/// <summary>
	///
	/// </summary>
	public static class PrimitiveSerializersStreamingTests
	{
		[Fact]
		public static void Bool()
		{
			var values = new bool[] { true, true, false, true, false, false };

			ByteStreamingTests.SerializeAndDeserialize(Serializer.Bool, values);
		}

		[Fact]
		public static void Char()
		{
			var values = new char[] { 'a', 'b', 'A', 'z', '!', '7', char.MinValue, char.MaxValue };

			ByteStreamingTests.SerializeAndDeserialize(Serializer.Char, values);
		}

		[Fact]
		public static void Float()
		{
			var values = new float[] { 7.13f, 0f, -7.13f, float.MinValue, float.MaxValue };

			ByteStreamingTests.SerializeAndDeserialize(Serializer.Float, values);
		}

		[Fact]
		public static void Double()
		{
			var values = new double[] { 7.13, 0.0, -7.13, double.MinValue, double.MaxValue };

			ByteStreamingTests.SerializeAndDeserialize(Serializer.Double, values);
		}

		[Fact]
		public static void Decimal()
		{
			var values = new decimal[] { 7.13m, 0m, -7.13m, decimal.MinValue, decimal.MaxValue };

			ByteStreamingTests.SerializeAndDeserialize(Serializer.Decimal, values);
		}

		[Fact]
		public static void Byte()
		{
			var values = new byte[] { 3, 99, 20, 7, byte.MinValue, byte.MaxValue };

			ByteStreamingTests.SerializeAndDeserialize(Serializer.Byte, values);
		}

		[Fact]
		public static void SByte()
		{
			var values = new sbyte[] { 7, 0, 20, -5, sbyte.MinValue, sbyte.MaxValue };

			ByteStreamingTests.SerializeAndDeserialize(Serializer.SByte, values);
		}

		[Fact]
		public static void Short()
		{
			var values = new short[] { 7, 0, 20, -5, short.MinValue, short.MaxValue };

			ByteStreamingTests.SerializeAndDeserialize(Serializer.Short, values);
		}

		[Fact]
		public static void Int()
		{
			var values = new int[] { 7, 0, 20, -5, int.MinValue, int.MaxValue };

			ByteStreamingTests.SerializeAndDeserialize(Serializer.Int, values);
		}

		[Fact]
		public static void Long()
		{
			var values = new long[] { 7, 0, 20, -5, long.MinValue, long.MaxValue };

			ByteStreamingTests.SerializeAndDeserialize(Serializer.Long, values);
		}

		[Fact]
		public static void UShort()
		{
			var values = new ushort[] { 7, 999, 20, ushort.MinValue, ushort.MaxValue };

			ByteStreamingTests.SerializeAndDeserialize(Serializer.UShort, values);
		}

		[Fact]
		public static void UInt()
		{
			var values = new uint[] { 7, 999, 20, uint.MinValue, uint.MaxValue };

			ByteStreamingTests.SerializeAndDeserialize(Serializer.UInt, values);
		}

		[Fact]
		public static void ULong()
		{
			var values = new ulong[] { 7, 999, 20, ulong.MinValue, ulong.MaxValue };

			ByteStreamingTests.SerializeAndDeserialize(Serializer.ULong, values);
		}

		[Fact]
		public static void String()
		{
			var values = new string[] { string.Empty, "a", "abc", "Hello World!", $"New{Environment.NewLine}Line" };

			ByteStreamingTests.SerializeAndDeserialize(StringSerializer.IncludeLength, values);
		}
	}
}
