using System;
using FluentAssertions;
using Voxelscape.Utility.Common.Core.Conversions;
using Xunit;

namespace Voxelscape.Utility.Common.Core.Test.Conversions
{
	/// <summary>
	/// Tests for the <see cref="MultiTypeConverter"/> class.
	/// </summary>
	public static class MultiTypeConverterTests
	{
		/// <summary>
		/// Tests that AddConverter works when there isn't already a converter for that type registered.
		/// </summary>
		[Fact]
		public static void AddConverterSuccessful()
		{
			MultiTypeConverter subject = new MultiTypeConverter();

			bool wasConverterAdded = subject.AddConverter<int, string>(number => number.ToString());

			// asserts
			wasConverterAdded.Should().BeTrue();
			subject.ContainsConverter<int, string>().Should().BeTrue();
			subject.ContainsConverter(ConversionPair.CreateNew<int, string>()).Should().BeTrue();
		}

		/// <summary>
		/// Tests that AddFunction works when there isn't already a function for that type registered.
		/// </summary>
		[Fact]
		public static void AddFunctionSuccessful()
		{
			MultiTypeConverter subject = new MultiTypeConverter();

			bool wasConverterAdded = subject.AddFunction<int, string>(number => number.ToString());

			// asserts
			wasConverterAdded.Should().BeTrue();
			subject.ContainsConverter<int, string>().Should().BeTrue();
			subject.ContainsConverter(ConversionPair.CreateNew<int, string>()).Should().BeTrue();
		}

		/// <summary>
		/// Tests that AddConverter works when there is already a converter for that type registered.
		/// </summary>
		[Fact]
		public static void AddConverterFailed()
		{
			MultiTypeConverter subject = new MultiTypeConverter();
			subject.AddConverter<int, string>(number => number.ToString());

			bool wasConverterAdded = subject.AddConverter<int, string>(number => number.ToString());

			// asserts
			wasConverterAdded.Should().BeFalse();
			subject.ContainsConverter<int, string>().Should().BeTrue();
			subject.ContainsConverter(ConversionPair.CreateNew<int, string>()).Should().BeTrue();
		}

		/// <summary>
		/// Tests that AddFunction works when there is already a function for that type registered.
		/// </summary>
		[Fact]
		public static void AddFunctionFailed()
		{
			MultiTypeConverter subject = new MultiTypeConverter();
			subject.AddFunction<int, string>(number => number.ToString());

			bool wasConverterAdded = subject.AddFunction<int, string>(number => number.ToString());

			// asserts
			wasConverterAdded.Should().BeFalse();
			subject.ContainsConverter<int, string>().Should().BeTrue();
			subject.ContainsConverter(ConversionPair.CreateNew<int, string>()).Should().BeTrue();
		}

		/// <summary>
		/// Tests that TryGetConverter works when there is a converter to retrieve.
		/// </summary>
		[Fact]
		public static void TryGetConverterSuccessful()
		{
			MultiTypeConverter subject = new MultiTypeConverter();
			NonTypedConverter converterAdded = new NonTypedConverter(new Func<int, string>(number => number.ToString()));
			subject.AddNonTypedConverter(converterAdded);

			NonTypedConverter converterRetrieved;
			bool wasConverterRetrieved = subject.TryGetConverter<int, string>(out converterRetrieved);

			// asserts
			wasConverterRetrieved.Should().BeTrue();
			converterRetrieved.Should().BeSameAs(converterAdded);
		}

		/// <summary>
		/// Tests that TryGetConverter works when there is not a converter to retrieve.
		/// </summary>
		[Fact]
		public static void TryGetConverterFailed()
		{
			MultiTypeConverter subject = new MultiTypeConverter();

			NonTypedConverter converterRetrieved;
			bool wasConverterRetrieved = subject.TryGetConverter<int, string>(out converterRetrieved);

			// asserts
			wasConverterRetrieved.Should().BeFalse();
			converterRetrieved.Should().BeNull();
		}

		/// <summary>
		/// Tests that the type safe overload of TryConvert works when there is an appropriate converter available.
		/// </summary>
		[Fact]
		public static void TryConvertTypeSafeSuccessful()
		{
			int inputValue = 1;
			string expectedResult = "1";

			MultiTypeConverter subject = new MultiTypeConverter();
			subject.AddConverter<int, string>(number => number.ToString());

			string convertedValue;
			Exception exception;
			ConversionStatus status = subject.TryConvert<int, string>(inputValue, out convertedValue, out exception);

			// asserts
			status.Should().Be(ConversionStatus.Success);
			convertedValue.Should().Be(expectedResult);
			exception.Should().BeNull();
		}

		/// <summary>
		/// Tests that the not type safe overload of TryConvert works when there is an appropriate converter available.
		/// </summary>
		[Fact]
		public static void TryConvertNotTypeSafeSuccessful()
		{
			int inputValue = 1;
			string expectedResult = "1";

			MultiTypeConverter subject = new MultiTypeConverter();
			subject.AddConverter<int, string>(number => number.ToString());

			object convertedValue;
			Exception exception;
			ConversionStatus status = subject.TryConvert(
				ConversionPair.CreateNew<int, string>(), inputValue, out convertedValue, out exception);

			// asserts
			status.Should().Be(ConversionStatus.Success);
			convertedValue.Should().Be(expectedResult);
			exception.Should().BeNull();
		}

		/// <summary>
		/// Tests that the type safe overload of TryConvert works when there is no appropriate converter found.
		/// </summary>
		[Fact]
		public static void TryConvertTypeSafeNoConverterFound()
		{
			MultiTypeConverter subject = new MultiTypeConverter();

			string convertedValue;
			Exception exception;
			ConversionStatus status = subject.TryConvert<int, string>(1, out convertedValue, out exception);

			// asserts
			status.Should().Be(ConversionStatus.NoConverterFound);
			convertedValue.Should().BeNull();
			exception.Should().BeNull();
		}

		/// <summary>
		/// Tests that the not type safe overload of TryConvert works when there is no appropriate converter found.
		/// </summary>
		[Fact]
		public static void TryConvertNotTypeSafeNoConverterFound()
		{
			MultiTypeConverter subject = new MultiTypeConverter();

			object convertedValue;
			Exception exception;
			ConversionStatus status = subject.TryConvert(
				ConversionPair.CreateNew<int, string>(), 1, out convertedValue, out exception);

			// asserts
			status.Should().Be(ConversionStatus.NoConverterFound);
			convertedValue.Should().BeNull();
			exception.Should().BeNull();
		}

		/// <summary>
		/// Tests that the type safe overload of TryConvert works when the converter throws an exception.
		/// </summary>
		[Fact]
		public static void TryConvertTypeSafeException()
		{
			MultiTypeConverter subject = new MultiTypeConverter();
			subject.AddConverter<int, string>(number => { throw new ArgumentException(); });

			string result;
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
			MultiTypeConverter subject = new MultiTypeConverter();
			subject.AddConverter<int, string>(number => { throw new ArgumentException(); });

			object result;
			Exception exception;
			ConversionStatus status = subject.TryConvert(
				ConversionPair.CreateNew<int, string>(), 1, out result, out exception);

			// asserts
			status.Should().Be(ConversionStatus.Exception);
			result.Should().BeNull();
			exception.Should().NotBeNull();
			exception.GetType().Should().Be(typeof(ArgumentException));
		}
	}
}
