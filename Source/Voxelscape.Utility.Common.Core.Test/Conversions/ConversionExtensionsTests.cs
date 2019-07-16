using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Voxelscape.Utility.Common.Pact.Types;
using Xunit;

namespace Voxelscape.Utility.Common.Core.Test.Conversions
{
	/// <summary>
	/// Tests for the <see cref="ConversionExtensions"/> class.
	/// </summary>
	public static class ConversionExtensionsTests
	{
		/// <summary>
		/// Tests that primitive type correctly convert to one another.
		/// </summary>
		/// <param name="sourceType">Type of the source.</param>
		/// <param name="resultType">Type of the result.</param>
		/// <param name="expectedResult">If set to <c>true</c> the source should be convertible to the result.</param>
		[Theory]
		[InlineData(PrimitiveType.Byte, PrimitiveType.Byte, true)]
		[InlineData(PrimitiveType.Byte, PrimitiveType.SByte, false)]
		[InlineData(PrimitiveType.Byte, PrimitiveType.Short, true)]
		[InlineData(PrimitiveType.Byte, PrimitiveType.UShort, true)]
		[InlineData(PrimitiveType.Byte, PrimitiveType.Int, true)]
		[InlineData(PrimitiveType.Byte, PrimitiveType.UInt, true)]
		[InlineData(PrimitiveType.Byte, PrimitiveType.Long, true)]
		[InlineData(PrimitiveType.Byte, PrimitiveType.ULong, true)]
		[InlineData(PrimitiveType.Byte, PrimitiveType.Float, true)]
		[InlineData(PrimitiveType.Byte, PrimitiveType.Double, true)]
		[InlineData(PrimitiveType.Byte, PrimitiveType.Decimal, true)]
		[InlineData(PrimitiveType.Byte, PrimitiveType.Char, false)]
		[InlineData(PrimitiveType.Byte, PrimitiveType.Bool, false)]
		[InlineData(PrimitiveType.SByte, PrimitiveType.Byte, false)]
		[InlineData(PrimitiveType.SByte, PrimitiveType.SByte, true)]
		[InlineData(PrimitiveType.SByte, PrimitiveType.Short, true)]
		[InlineData(PrimitiveType.SByte, PrimitiveType.UShort, false)]
		[InlineData(PrimitiveType.SByte, PrimitiveType.Int, true)]
		[InlineData(PrimitiveType.SByte, PrimitiveType.UInt, false)]
		[InlineData(PrimitiveType.SByte, PrimitiveType.Long, true)]
		[InlineData(PrimitiveType.SByte, PrimitiveType.ULong, false)]
		[InlineData(PrimitiveType.SByte, PrimitiveType.Float, true)]
		[InlineData(PrimitiveType.SByte, PrimitiveType.Double, true)]
		[InlineData(PrimitiveType.SByte, PrimitiveType.Decimal, true)]
		[InlineData(PrimitiveType.SByte, PrimitiveType.Char, false)]
		[InlineData(PrimitiveType.SByte, PrimitiveType.Bool, false)]
		[InlineData(PrimitiveType.Short, PrimitiveType.Byte, false)]
		[InlineData(PrimitiveType.Short, PrimitiveType.SByte, false)]
		[InlineData(PrimitiveType.Short, PrimitiveType.Short, true)]
		[InlineData(PrimitiveType.Short, PrimitiveType.UShort, false)]
		[InlineData(PrimitiveType.Short, PrimitiveType.Int, true)]
		[InlineData(PrimitiveType.Short, PrimitiveType.UInt, false)]
		[InlineData(PrimitiveType.Short, PrimitiveType.Long, true)]
		[InlineData(PrimitiveType.Short, PrimitiveType.ULong, false)]
		[InlineData(PrimitiveType.Short, PrimitiveType.Float, true)]
		[InlineData(PrimitiveType.Short, PrimitiveType.Double, true)]
		[InlineData(PrimitiveType.Short, PrimitiveType.Decimal, true)]
		[InlineData(PrimitiveType.Short, PrimitiveType.Char, false)]
		[InlineData(PrimitiveType.Short, PrimitiveType.Bool, false)]
		[InlineData(PrimitiveType.UShort, PrimitiveType.Byte, false)]
		[InlineData(PrimitiveType.UShort, PrimitiveType.SByte, false)]
		[InlineData(PrimitiveType.UShort, PrimitiveType.Short, false)]
		[InlineData(PrimitiveType.UShort, PrimitiveType.UShort, true)]
		[InlineData(PrimitiveType.UShort, PrimitiveType.Int, true)]
		[InlineData(PrimitiveType.UShort, PrimitiveType.UInt, true)]
		[InlineData(PrimitiveType.UShort, PrimitiveType.Long, true)]
		[InlineData(PrimitiveType.UShort, PrimitiveType.ULong, true)]
		[InlineData(PrimitiveType.UShort, PrimitiveType.Float, true)]
		[InlineData(PrimitiveType.UShort, PrimitiveType.Double, true)]
		[InlineData(PrimitiveType.UShort, PrimitiveType.Decimal, true)]
		[InlineData(PrimitiveType.UShort, PrimitiveType.Char, false)]
		[InlineData(PrimitiveType.UShort, PrimitiveType.Bool, false)]
		[InlineData(PrimitiveType.Int, PrimitiveType.Byte, false)]
		[InlineData(PrimitiveType.Int, PrimitiveType.SByte, false)]
		[InlineData(PrimitiveType.Int, PrimitiveType.Short, false)]
		[InlineData(PrimitiveType.Int, PrimitiveType.UShort, false)]
		[InlineData(PrimitiveType.Int, PrimitiveType.Int, true)]
		[InlineData(PrimitiveType.Int, PrimitiveType.UInt, false)]
		[InlineData(PrimitiveType.Int, PrimitiveType.Long, true)]
		[InlineData(PrimitiveType.Int, PrimitiveType.ULong, false)]
		[InlineData(PrimitiveType.Int, PrimitiveType.Float, true)]
		[InlineData(PrimitiveType.Int, PrimitiveType.Double, true)]
		[InlineData(PrimitiveType.Int, PrimitiveType.Decimal, true)]
		[InlineData(PrimitiveType.Int, PrimitiveType.Char, false)]
		[InlineData(PrimitiveType.Int, PrimitiveType.Bool, false)]
		[InlineData(PrimitiveType.UInt, PrimitiveType.Byte, false)]
		[InlineData(PrimitiveType.UInt, PrimitiveType.SByte, false)]
		[InlineData(PrimitiveType.UInt, PrimitiveType.Short, false)]
		[InlineData(PrimitiveType.UInt, PrimitiveType.UShort, false)]
		[InlineData(PrimitiveType.UInt, PrimitiveType.Int, false)]
		[InlineData(PrimitiveType.UInt, PrimitiveType.UInt, true)]
		[InlineData(PrimitiveType.UInt, PrimitiveType.Long, true)]
		[InlineData(PrimitiveType.UInt, PrimitiveType.ULong, true)]
		[InlineData(PrimitiveType.UInt, PrimitiveType.Float, true)]
		[InlineData(PrimitiveType.UInt, PrimitiveType.Double, true)]
		[InlineData(PrimitiveType.UInt, PrimitiveType.Decimal, true)]
		[InlineData(PrimitiveType.UInt, PrimitiveType.Char, false)]
		[InlineData(PrimitiveType.UInt, PrimitiveType.Bool, false)]
		[InlineData(PrimitiveType.Long, PrimitiveType.Byte, false)]
		[InlineData(PrimitiveType.Long, PrimitiveType.SByte, false)]
		[InlineData(PrimitiveType.Long, PrimitiveType.Short, false)]
		[InlineData(PrimitiveType.Long, PrimitiveType.UShort, false)]
		[InlineData(PrimitiveType.Long, PrimitiveType.Int, false)]
		[InlineData(PrimitiveType.Long, PrimitiveType.UInt, false)]
		[InlineData(PrimitiveType.Long, PrimitiveType.Long, true)]
		[InlineData(PrimitiveType.Long, PrimitiveType.ULong, false)]
		[InlineData(PrimitiveType.Long, PrimitiveType.Float, true)]
		[InlineData(PrimitiveType.Long, PrimitiveType.Double, true)]
		[InlineData(PrimitiveType.Long, PrimitiveType.Decimal, true)]
		[InlineData(PrimitiveType.Long, PrimitiveType.Char, false)]
		[InlineData(PrimitiveType.Long, PrimitiveType.Bool, false)]
		[InlineData(PrimitiveType.ULong, PrimitiveType.Byte, false)]
		[InlineData(PrimitiveType.ULong, PrimitiveType.SByte, false)]
		[InlineData(PrimitiveType.ULong, PrimitiveType.Short, false)]
		[InlineData(PrimitiveType.ULong, PrimitiveType.UShort, false)]
		[InlineData(PrimitiveType.ULong, PrimitiveType.Int, false)]
		[InlineData(PrimitiveType.ULong, PrimitiveType.UInt, false)]
		[InlineData(PrimitiveType.ULong, PrimitiveType.Long, false)]
		[InlineData(PrimitiveType.ULong, PrimitiveType.ULong, true)]
		[InlineData(PrimitiveType.ULong, PrimitiveType.Float, true)]
		[InlineData(PrimitiveType.ULong, PrimitiveType.Double, true)]
		[InlineData(PrimitiveType.ULong, PrimitiveType.Decimal, true)]
		[InlineData(PrimitiveType.ULong, PrimitiveType.Char, false)]
		[InlineData(PrimitiveType.ULong, PrimitiveType.Bool, false)]
		[InlineData(PrimitiveType.Float, PrimitiveType.Byte, false)]
		[InlineData(PrimitiveType.Float, PrimitiveType.SByte, false)]
		[InlineData(PrimitiveType.Float, PrimitiveType.Short, false)]
		[InlineData(PrimitiveType.Float, PrimitiveType.UShort, false)]
		[InlineData(PrimitiveType.Float, PrimitiveType.Int, false)]
		[InlineData(PrimitiveType.Float, PrimitiveType.UInt, false)]
		[InlineData(PrimitiveType.Float, PrimitiveType.Long, false)]
		[InlineData(PrimitiveType.Float, PrimitiveType.ULong, false)]
		[InlineData(PrimitiveType.Float, PrimitiveType.Float, true)]
		[InlineData(PrimitiveType.Float, PrimitiveType.Double, true)]
		[InlineData(PrimitiveType.Float, PrimitiveType.Decimal, false)]
		[InlineData(PrimitiveType.Float, PrimitiveType.Char, false)]
		[InlineData(PrimitiveType.Float, PrimitiveType.Bool, false)]
		[InlineData(PrimitiveType.Double, PrimitiveType.Byte, false)]
		[InlineData(PrimitiveType.Double, PrimitiveType.SByte, false)]
		[InlineData(PrimitiveType.Double, PrimitiveType.Short, false)]
		[InlineData(PrimitiveType.Double, PrimitiveType.UShort, false)]
		[InlineData(PrimitiveType.Double, PrimitiveType.Int, false)]
		[InlineData(PrimitiveType.Double, PrimitiveType.UInt, false)]
		[InlineData(PrimitiveType.Double, PrimitiveType.Long, false)]
		[InlineData(PrimitiveType.Double, PrimitiveType.ULong, false)]
		[InlineData(PrimitiveType.Double, PrimitiveType.Float, false)]
		[InlineData(PrimitiveType.Double, PrimitiveType.Double, true)]
		[InlineData(PrimitiveType.Double, PrimitiveType.Decimal, false)]
		[InlineData(PrimitiveType.Double, PrimitiveType.Char, false)]
		[InlineData(PrimitiveType.Double, PrimitiveType.Bool, false)]
		[InlineData(PrimitiveType.Decimal, PrimitiveType.Byte, false)]
		[InlineData(PrimitiveType.Decimal, PrimitiveType.SByte, false)]
		[InlineData(PrimitiveType.Decimal, PrimitiveType.Short, false)]
		[InlineData(PrimitiveType.Decimal, PrimitiveType.UShort, false)]
		[InlineData(PrimitiveType.Decimal, PrimitiveType.Int, false)]
		[InlineData(PrimitiveType.Decimal, PrimitiveType.UInt, false)]
		[InlineData(PrimitiveType.Decimal, PrimitiveType.Long, false)]
		[InlineData(PrimitiveType.Decimal, PrimitiveType.ULong, false)]
		[InlineData(PrimitiveType.Decimal, PrimitiveType.Float, false)]
		[InlineData(PrimitiveType.Decimal, PrimitiveType.Double, false)]
		[InlineData(PrimitiveType.Decimal, PrimitiveType.Decimal, true)]
		[InlineData(PrimitiveType.Decimal, PrimitiveType.Char, false)]
		[InlineData(PrimitiveType.Decimal, PrimitiveType.Bool, false)]
		[InlineData(PrimitiveType.Char, PrimitiveType.Byte, false)]
		[InlineData(PrimitiveType.Char, PrimitiveType.SByte, false)]
		[InlineData(PrimitiveType.Char, PrimitiveType.Short, false)]
		[InlineData(PrimitiveType.Char, PrimitiveType.UShort, true)]
		[InlineData(PrimitiveType.Char, PrimitiveType.Int, true)]
		[InlineData(PrimitiveType.Char, PrimitiveType.UInt, true)]
		[InlineData(PrimitiveType.Char, PrimitiveType.Long, true)]
		[InlineData(PrimitiveType.Char, PrimitiveType.ULong, true)]
		[InlineData(PrimitiveType.Char, PrimitiveType.Float, true)]
		[InlineData(PrimitiveType.Char, PrimitiveType.Double, true)]
		[InlineData(PrimitiveType.Char, PrimitiveType.Decimal, true)]
		[InlineData(PrimitiveType.Char, PrimitiveType.Char, true)]
		[InlineData(PrimitiveType.Char, PrimitiveType.Bool, false)]
		[InlineData(PrimitiveType.Bool, PrimitiveType.Byte, false)]
		[InlineData(PrimitiveType.Bool, PrimitiveType.SByte, false)]
		[InlineData(PrimitiveType.Bool, PrimitiveType.Short, false)]
		[InlineData(PrimitiveType.Bool, PrimitiveType.UShort, false)]
		[InlineData(PrimitiveType.Bool, PrimitiveType.Int, false)]
		[InlineData(PrimitiveType.Bool, PrimitiveType.UInt, false)]
		[InlineData(PrimitiveType.Bool, PrimitiveType.Long, false)]
		[InlineData(PrimitiveType.Bool, PrimitiveType.ULong, false)]
		[InlineData(PrimitiveType.Bool, PrimitiveType.Float, false)]
		[InlineData(PrimitiveType.Bool, PrimitiveType.Double, false)]
		[InlineData(PrimitiveType.Bool, PrimitiveType.Decimal, false)]
		[InlineData(PrimitiveType.Bool, PrimitiveType.Char, false)]
		[InlineData(PrimitiveType.Bool, PrimitiveType.Bool, true)]
		[SuppressMessage(
			"StyleCop.CSharp.ReadabilityRules",
			"SA1123:DoNotPlaceRegionsWithinElements",
			Justification = "Regions only used to group by source type. No actual implementation code.")]
		public static void IsImplicitlyConvertibleTo(
			PrimitiveType sourceType, PrimitiveType resultType, bool expectedResult)
		{
			sourceType.IsImplicitlyConvertibleTo(resultType).Should().Be(expectedResult);

			#region Source Byte

			////byte source = 0;
			////byte result = 0;
			////result = source; // true

			////byte source = 0;
			////sbyte result = 0;
			////result = source; // false

			////byte source = 0;
			////short result = 0;
			////result = source; // true

			////byte source = 0;
			////ushort result = 0;
			////result = source; // true

			////byte source = 0;
			////int result = 0;
			////result = source; // true

			////byte source = 0;
			////uint result = 0;
			////result = source; // true

			////byte source = 0;
			////long result = 0;
			////result = source; // true

			////byte source = 0;
			////ulong result = 0;
			////result = source; // true

			////byte source = 0;
			////float result = 0;
			////result = source; // true

			////byte source = 0;
			////double result = 0;
			////result = source; // true

			////byte source = 0;
			////decimal result = 0;
			////result = source; // true

			////byte source = 0;
			////char result = 'a';
			////result = source; // false

			////byte source = 0;
			////bool result = false;
			////result = source; // false

			#endregion

			#region Source SByte

			////sbyte source = 0;
			////byte result = 0;
			////result = source; // false

			////sbyte source = 0;
			////sbyte result = 0;
			////result = source; // true

			////sbyte source = 0;
			////short result = 0;
			////result = source; // true

			////sbyte source = 0;
			////ushort result = 0;
			////result = source; // false

			////sbyte source = 0;
			////int result = 0;
			////result = source; // true

			////sbyte source = 0;
			////uint result = 0;
			////result = source; // false

			////sbyte source = 0;
			////long result = 0;
			////result = source; // true

			////sbyte source = 0;
			////ulong result = 0;
			////result = source; // false

			////sbyte source = 0;
			////float result = 0;
			////result = source; // true

			////sbyte source = 0;
			////double result = 0;
			////result = source; // true

			////sbyte source = 0;
			////decimal result = 0;
			////result = source; // true

			////sbyte source = 0;
			////char result = 'a';
			////result = source; // false

			////sbyte source = 0;
			////bool result = false;
			////result = source; // false

			#endregion

			#region Source Short

			////short source = 0;
			////byte result = 0;
			////result = source; // false

			////short source = 0;
			////sbyte result = 0;
			////result = source; // false

			////short source = 0;
			////short result = 0;
			////result = source; // true

			////short source = 0;
			////ushort result = 0;
			////result = source; // false

			////short source = 0;
			////int result = 0;
			////result = source; // true

			////short source = 0;
			////uint result = 0;
			////result = source; // false

			////short source = 0;
			////long result = 0;
			////result = source; // true

			////short source = 0;
			////ulong result = 0;
			////result = source; // false

			////short source = 0;
			////float result = 0;
			////result = source; // true

			////short source = 0;
			////double result = 0;
			////result = source; // true

			////short source = 0;
			////decimal result = 0;
			////result = source; // true

			////short source = 0;
			////char result = 'a';
			////result = source; // false

			////short source = 0;
			////bool result = false;
			////result = source; // false

			#endregion

			#region Source UShort

			////ushort source = 0;
			////byte result = 0;
			////result = source; // false

			////ushort source = 0;
			////sbyte result = 0;
			////result = source; // false

			////ushort source = 0;
			////short result = 0;
			////result = source; // false

			////ushort source = 0;
			////ushort result = 0;
			////result = source; // true

			////ushort source = 0;
			////int result = 0;
			////result = source; // true

			////ushort source = 0;
			////uint result = 0;
			////result = source; // true

			////ushort source = 0;
			////long result = 0;
			////result = source; // true

			////ushort source = 0;
			////ulong result = 0;
			////result = source; // true

			////ushort source = 0;
			////float result = 0;
			////result = source; // true

			////ushort source = 0;
			////double result = 0;
			////result = source; // true

			////ushort source = 0;
			////decimal result = 0;
			////result = source; // true

			////ushort source = 0;
			////char result = 'a';
			////result = source; // false

			////ushort source = 0;
			////bool result = false;
			////result = source; // false

			#endregion

			#region Source Int

			////int source = 0;
			////byte result = 0;
			////result = source; // false

			////int source = 0;
			////sbyte result = 0;
			////result = source; // false

			////int source = 0;
			////short result = 0;
			////result = source; // false

			////int source = 0;
			////ushort result = 0;
			////result = source; // false

			////int source = 0;
			////int result = 0;
			////result = source; // true

			////int source = 0;
			////uint result = 0;
			////result = source; // false

			////int source = 0;
			////long result = 0;
			////result = source; // true

			////int source = 0;
			////ulong result = 0;
			////result = source; // false

			////int source = 0;
			////float result = 0;
			////result = source; // true

			////uint source = 0;
			////double result = 0;
			////result = source; // true

			////int source = 0;
			////decimal result = 0;
			////result = source; // true

			////int source = 0;
			////char result = 'a';
			////result = source; // false

			////int source = 0;
			////bool result = false;
			////result = source; // false

			#endregion

			#region Source UInt

			////uint source = 0;
			////byte result = 0;
			////result = source; // false

			////uint source = 0;
			////sbyte result = 0;
			////result = source; // false

			////uint source = 0;
			////short result = 0;
			////result = source; // false

			////uint source = 0;
			////ushort result = 0;
			////result = source; // false

			////uint source = 0;
			////int result = 0;
			////result = source; // false

			////uint source = 0;
			////uint result = 0;
			////result = source; // true

			////uint source = 0;
			////long result = 0;
			////result = source; // true

			////uint source = 0;
			////ulong result = 0;
			////result = source; // true

			////uint source = 0;
			////float result = 0;
			////result = source; // true

			////uint source = 0;
			////double result = 0;
			////result = source; // true

			////uint source = 0;
			////decimal result = 0;
			////result = source; // true

			////uint source = 0;
			////char result = 'a';
			////result = source; // false

			////uint source = 0;
			////bool result = false;
			////result = source; // false

			#endregion

			#region Source Long

			////long source = 0;
			////byte result = 0;
			////result = source; // false

			////long source = 0;
			////sbyte result = 0;
			////result = source; // false

			////long source = 0;
			////short result = 0;
			////result = source; // false

			////long source = 0;
			////ushort result = 0;
			////result = source; // false

			////long source = 0;
			////int result = 0;
			////result = source; // false

			////long source = 0;
			////uint result = 0;
			////result = source; // false

			////long source = 0;
			////long result = 0;
			////result = source; // true

			////long source = 0;
			////ulong result = 0;
			////result = source; // false

			////long source = 0;
			////float result = 0;
			////result = source; // true

			////long source = 0;
			////double result = 0;
			////result = source; // true

			////long source = 0;
			////decimal result = 0;
			////result = source; // true

			////long source = 0;
			////char result = 'a';
			////result = source; // false

			////long source = 0;
			////bool result = false;
			////result = source; // false

			#endregion

			#region Source ULong

			////ulong source = 0;
			////byte result = 0;
			////result = source; // false

			////ulong source = 0;
			////sbyte result = 0;
			////result = source; // false

			////ulong source = 0;
			////short result = 0;
			////result = source; // false

			////ulong source = 0;
			////ushort result = 0;
			////result = source; // false

			////ulong source = 0;
			////int result = 0;
			////result = source; // false

			////ulong source = 0;
			////uint result = 0;
			////result = source; // false

			////ulong source = 0;
			////long result = 0;
			////result = source; // false

			////ulong source = 0;
			////ulong result = 0;
			////result = source; // true

			////ulong source = 0;
			////float result = 0;
			////result = source; // true

			////ulong source = 0;
			////double result = 0;
			////result = source; // true

			////ulong source = 0;
			////decimal result = 0;
			////result = source; // true

			////ulong source = 0;
			////char result = 'a';
			////result = source; // false

			////ulong source = 0;
			////bool result = false;
			////result = source; // false

			#endregion

			#region Source Float

			////float source = 0;
			////byte result = 0;
			////result = source; // false

			////float source = 0;
			////sbyte result = 0;
			////result = source; // false

			////float source = 0;
			////short result = 0;
			////result = source; // false

			////float source = 0;
			////ushort result = 0;
			////result = source; // false

			////float source = 0;
			////int result = 0;
			////result = source; // false

			////float source = 0;
			////uint result = 0;
			////result = source; // false

			////float source = 0;
			////long result = 0;
			////result = source; // false

			////float source = 0;
			////ulong result = 0;
			////result = source; // false

			////float source = 0;
			////float result = 0;
			////result = source; // true

			////float source = 0;
			////double result = 0;
			////result = source; // true

			////float source = 0;
			////decimal result = 0;
			////result = source; // false

			////float source = 0;
			////char result = 'a';
			////result = source; // false

			////float source = 0;
			////bool result = false;
			////result = source; // false

			#endregion

			#region Source Double

			////double source = 0;
			////byte result = 0;
			////result = source; // false

			////double source = 0;
			////sbyte result = 0;
			////result = source; // false

			////double source = 0;
			////short result = 0;
			////result = source; // false

			////double source = 0;
			////ushort result = 0;
			////result = source; // false

			////double source = 0;
			////int result = 0;
			////result = source; // false

			////double source = 0;
			////uint result = 0;
			////result = source; // false

			////double source = 0;
			////long result = 0;
			////result = source; // false

			////double source = 0;
			////ulong result = 0;
			////result = source; // false

			////double source = 0;
			////float result = 0;
			////result = source; // false

			////double source = 0;
			////double result = 0;
			////result = source; // true

			////double source = 0;
			////decimal result = 0;
			////result = source; // false

			////double source = 0;
			////char result = 'a';
			////result = source; // false

			////double source = 0;
			////bool result = false;
			////result = source; // false

			#endregion

			#region Source Decimal

			////decimal source = 0;
			////byte result = 0;
			////result = source; // false

			////decimal source = 0;
			////sbyte result = 0;
			////result = source; // false

			////decimal source = 0;
			////short result = 0;
			////result = source; // false

			////decimal source = 0;
			////ushort result = 0;
			////result = source; // false

			////decimal source = 0;
			////int result = 0;
			////result = source; // false

			////decimal source = 0;
			////uint result = 0;
			////result = source; // false

			////decimal source = 0;
			////long result = 0;
			////result = source; // false

			////decimal source = 0;
			////ulong result = 0;
			////result = source; // false

			////decimal source = 0;
			////float result = 0;
			////result = source; // false

			////decimal source = 0;
			////double result = 0;
			////result = source; // false

			////decimal source = 0;
			////decimal result = 0;
			////result = source; // true

			////decimal source = 0;
			////char result = 'a';
			////result = source; // false

			////decimal source = 0;
			////bool result = false;
			////result = source; // false

			#endregion

			#region Source Char

			////char source = 'a';
			////byte result = 0;
			////result = source; // false

			////char source = 'a';
			////sbyte result = 0;
			////result = source; // false

			////char source = 'a';
			////short result = 0;
			////result = source; // false

			////char source = 'a';
			////ushort result = 0;
			////result = source; // true

			////char source = 'a';
			////int result = 0;
			////result = source; // true

			////char source = 'a';
			////uint result = 0;
			////result = source; // true

			////char source = 'a';
			////long result = 0;
			////result = source; // true

			////char source = 'a';
			////ulong result = 0;
			////result = source; // true

			////char source = 'a';
			////float result = 0;
			////result = source; // true

			////char source = 'a';
			////double result = 0;
			////result = source; // true

			////char source = 'a';
			////decimal result = 0;
			////result = source; // true

			////char source = 'a';
			////char result = 'a';
			////result = source; // true

			////char source = 'a';
			////bool result = false;
			////result = source; // false

			#endregion

			#region Source Bool

			////bool source = false;
			////byte result = 0;
			////result = source; // false

			////bool source = false;
			////sbyte result = 0;
			////result = source; // false

			////bool source = false;
			////short result = 0;
			////result = source; // false

			////bool source = false;
			////ushort result = 0;
			////result = source; // false

			////bool source = false;
			////int result = 0;
			////result = source; // false

			////bool source = false;
			////uint result = 0;
			////result = source; // false

			////bool source = false;
			////long result = 0;
			////result = source; // false

			////bool source = false;
			////ulong result = 0;
			////result = source; // false

			////bool source = false;
			////float result = 0;
			////result = source; // false

			////bool source = false;
			////double result = 0;
			////result = source; // false

			////bool source = false;
			////decimal result = 0;
			////result = source; // false

			////bool source = false;
			////char result = 'a';
			////result = source; // false

			////bool source = false;
			////bool result = false;
			////result = source; // true

			#endregion
		}
	}
}
