using System;
using FluentAssertions;
using Voxelscape.Utility.Common.Core.Conversions;
using Voxelscape.Utility.Common.Core.Types;
using Xunit;

namespace Voxelscape.Utility.Common.Core.Test.Conversions
{
	/// <summary>
	/// Tests for the <see cref="PooledValueConverter"/> class.
	/// </summary>
	public static class PooledValueConverterTests
	{
		/// <summary>
		/// Tests that the type safe overload of TryConvert works when there is an appropriate converter available.
		/// </summary>
		[Fact]
		public static void TryConvertTypeSafeSuccess()
		{
			MultiTypeConverter converter = new MultiTypeConverter();
			converter.AddConverter<int, object>(number => new Wrapper<int>(number));

			PooledValueConverter subject = new PooledValueConverter(converter);

			object result1;
			Exception exception1;
			ConversionStatus status1 = subject.TryConvert(1, out result1, out exception1);

			object result2;
			Exception exception2;
			ConversionStatus status2 = subject.TryConvert(1, out result2, out exception2);

			object result3;
			Exception exception3;
			ConversionStatus status3 = subject.TryConvert(2, out result3, out exception3);

			// asserts
			result1.Should().BeSameAs(result2);
			result1.Should().Be(result2);
			result1.Should().NotBeSameAs(result3);
			result1.Should().NotBe(result3);

			status1.Should().Be(ConversionStatus.Success);
			status2.Should().Be(ConversionStatus.Success);
			status3.Should().Be(ConversionStatus.Success);

			exception1.Should().BeNull();
			exception2.Should().BeNull();
			exception3.Should().BeNull();
		}

		/// <summary>
		/// Tests that the not type safe overload of TryConvert works when there is an appropriate converter available.
		/// </summary>
		[Fact]
		public static void TryConvertNotTypeSafeSuccess()
		{
			MultiTypeConverter converter = new MultiTypeConverter();
			converter.AddConverter<int, object>(number => new Wrapper<int>(number));

			PooledValueConverter subject = new PooledValueConverter(converter);

			object result1;
			Exception exception1;
			ConversionStatus status1 = subject.TryConvert(
				ConversionPair.CreateNew<int, object>(), 1, out result1, out exception1);

			object result2;
			Exception exception2;
			ConversionStatus status2 = subject.TryConvert(
				ConversionPair.CreateNew<int, object>(), 1, out result2, out exception2);

			object result3;
			Exception exception3;
			ConversionStatus status3 = subject.TryConvert(
				ConversionPair.CreateNew<int, object>(), 2, out result3, out exception3);

			// asserts
			result1.Should().BeSameAs(result2);
			result1.Should().Be(result2);
			result1.Should().NotBeSameAs(result3);
			result1.Should().NotBe(result3);

			status1.Should().Be(ConversionStatus.Success);
			status2.Should().Be(ConversionStatus.Success);
			status3.Should().Be(ConversionStatus.Success);

			exception1.Should().BeNull();
			exception2.Should().BeNull();
			exception3.Should().BeNull();
		}

		/// <summary>
		/// Tests that the type safe overload of TryConvert works when there is no appropriate converter found.
		/// </summary>
		[Fact]
		public static void TryConvertTypeSafeNoConverterFound()
		{
			MultiTypeConverter converter = new MultiTypeConverter();
			PooledValueConverter subject = new PooledValueConverter(converter);

			object result;
			Exception exception;
			ConversionStatus status = subject.TryConvert(1, out result, out exception);

			// asserts
			status.Should().Be(ConversionStatus.NoConverterFound);
			result.Should().BeNull();
			exception.Should().BeNull();
		}

		/// <summary>
		/// Tests that the not type safe overload of TryConvert works when there is no appropriate converter found.
		/// </summary>
		[Fact]
		public static void TryConvertNotTypeSafeNoConverterFound()
		{
			MultiTypeConverter converter = new MultiTypeConverter();
			PooledValueConverter subject = new PooledValueConverter(converter);

			object result;
			Exception exception;
			ConversionStatus status = subject.TryConvert(
				ConversionPair.CreateNew<int, object>(), 1, out result, out exception);

			// asserts
			status.Should().Be(ConversionStatus.NoConverterFound);
			result.Should().BeNull();
			exception.Should().BeNull();
		}

		/// <summary>
		/// Tests that the type safe overload of TryConvert works when the converter throws an exception.
		/// </summary>
		[Fact]
		public static void TryConvertTypeSafeException()
		{
			MultiTypeConverter converter = new MultiTypeConverter();
			converter.AddConverter<int, object>(number => { throw new ArgumentException(); });
			PooledValueConverter subject = new PooledValueConverter(converter);

			object result;
			Exception exception;
			ConversionStatus status = subject.TryConvert(1, out result, out exception);

			// asserts
			status.Should().Be(ConversionStatus.Exception);
			result.Should().BeNull();
			exception.Should().NotBeNull();
			exception.GetType().Should().Be(typeof(ArgumentException));
		}

		/// <summary>
		/// Tests that the not type safe overload of TryConvert works when the converter throws an exception.
		/// </summary>
		[Fact]
		public static void TryConvertNotTypeSafeException()
		{
			MultiTypeConverter converter = new MultiTypeConverter();
			converter.AddConverter<int, object>(number => { throw new ArgumentException(); });
			PooledValueConverter subject = new PooledValueConverter(converter);

			object result;
			Exception exception;
			ConversionStatus status = subject.TryConvert(
				ConversionPair.CreateNew<int, object>(), 1, out result, out exception);

			// asserts
			status.Should().Be(ConversionStatus.Exception);
			result.Should().BeNull();
			exception.Should().NotBeNull();
			exception.GetType().Should().Be(typeof(ArgumentException));
		}
	}
}
