using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Voxelscape.Utility.Common.Pact.Test.Enums
{
	/// <summary>
	/// Tests for the enum extension methods.
	/// </summary>
	[SuppressMessage("StyleCop", "SA1600:ElementsMustBeDocumented", Justification = "Simple test enums.")]
	[SuppressMessage("StyleCop", "SA1602:EnumerationItemsMustBeDocumented", Justification = "Simple test enums.")]
	public static class EnumExtensionsTests
	{
		#region IsValidFlagsEnumValueTests

		[Flags]
		private enum WithFlags
		{
			First = 1,
			Second = 2,
			Third = 4,
			Fourth = 8,
		}

		private enum WithoutFlags
		{
			First = 1,
			Second = 22,
			Third = 55,
			Fourth = 13,
			Fifth = 127,
		}

		private enum WithoutNumbers
		{
			First, // 1
			Second, // 2
			Third, // 3
			Fourth, // 4
		}

		private enum WithoutFirstNumberAssigned
		{
			First = 7,
			Second, // 8
			Third, // 9
			Fourth, // 10
		}

		/// <summary>
		/// Tests that the IsValidFlagsEnumValue method works.
		/// </summary>
		[Fact]
		public static void IsValidFlagsEnumValueTests()
		{
			// test cases provided by
			// http://stackoverflow.com/questions/2674730/is-there-a-way-to-check-if-int-is-legal-enum-in-c
			Assert.True(((WithFlags)(1 | 4)).IsValidFlagsEnumValue());
			Assert.True(((WithFlags)(1 | 4)).IsValidFlagsEnumValue());
			Assert.True(((WithFlags)(1 | 4 | 2)).IsValidFlagsEnumValue());
			Assert.True(((WithFlags)2).IsValidFlagsEnumValue());
			Assert.True(((WithFlags)3).IsValidFlagsEnumValue());
			Assert.True(((WithFlags)(1 + 2 + 4 + 8)).IsValidFlagsEnumValue());

			Assert.False(((WithFlags)16).IsValidFlagsEnumValue());
			Assert.False(((WithFlags)17).IsValidFlagsEnumValue());
			Assert.False(((WithFlags)18).IsValidFlagsEnumValue());
			Assert.False(((WithFlags)0).IsValidFlagsEnumValue());

			Assert.True(((WithoutFlags)1).IsValidFlagsEnumValue());
			Assert.True(((WithoutFlags)22).IsValidFlagsEnumValue());
			Assert.True(((WithoutFlags)(53 | 6)).IsValidFlagsEnumValue()); // Will end up being Third
			Assert.True(((WithoutFlags)(22 | 25 | 99)).IsValidFlagsEnumValue()); // Will end up being Fifth
			Assert.True(((WithoutFlags)55).IsValidFlagsEnumValue());
			Assert.True(((WithoutFlags)127).IsValidFlagsEnumValue());

			Assert.False(((WithoutFlags)48).IsValidFlagsEnumValue());
			Assert.False(((WithoutFlags)50).IsValidFlagsEnumValue());
			Assert.False(((WithoutFlags)(1 | 22)).IsValidFlagsEnumValue());
			Assert.False(((WithoutFlags)(9 | 27 | 4)).IsValidFlagsEnumValue());

			Assert.True(((WithoutNumbers)0).IsValidFlagsEnumValue());
			Assert.True(((WithoutNumbers)1).IsValidFlagsEnumValue());
			Assert.True(((WithoutNumbers)2).IsValidFlagsEnumValue());
			Assert.True(((WithoutNumbers)3).IsValidFlagsEnumValue());
			Assert.True(((WithoutNumbers)(1 | 2)).IsValidFlagsEnumValue()); // Will end up being Third
			Assert.True(((WithoutNumbers)(1 + 2)).IsValidFlagsEnumValue()); // Will end up being Third

			Assert.False(((WithoutNumbers)4).IsValidFlagsEnumValue());
			Assert.False(((WithoutNumbers)5).IsValidFlagsEnumValue());
			Assert.False(((WithoutNumbers)25).IsValidFlagsEnumValue());
			Assert.False(((WithoutNumbers)(1 + 2 + 3)).IsValidFlagsEnumValue());

			Assert.True(((WithoutFirstNumberAssigned)7).IsValidFlagsEnumValue());
			Assert.True(((WithoutFirstNumberAssigned)8).IsValidFlagsEnumValue());
			Assert.True(((WithoutFirstNumberAssigned)9).IsValidFlagsEnumValue());
			Assert.True(((WithoutFirstNumberAssigned)10).IsValidFlagsEnumValue());

			Assert.False(((WithoutFirstNumberAssigned)11).IsValidFlagsEnumValue());
			Assert.False(((WithoutFirstNumberAssigned)6).IsValidFlagsEnumValue());
			Assert.False(((WithoutFirstNumberAssigned)(7 | 9)).IsValidFlagsEnumValue());
			Assert.False(((WithoutFirstNumberAssigned)(8 + 10)).IsValidFlagsEnumValue());
		}

		#endregion
	}
}
