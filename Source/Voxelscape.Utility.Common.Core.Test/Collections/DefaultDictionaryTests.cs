using System.Collections.Generic;
using Voxelscape.Utility.Common.Core.Collections;
using Voxelscape.Utility.Common.Pact.Test.Collections;
using Xunit;

namespace Voxelscape.Utility.Common.Core.Test.Collections
{
	/// <summary>
	/// Tests for the <see cref="DefaultDictionary{TKey, TValue}"/> class.
	/// </summary>
	public static class DefaultDictionaryTests
	{
		/// <summary>
		/// Tests that all interface implementations pass their respective test suites.
		/// </summary>
		[Fact]
		public static void InterfaceTests()
		{
			IDictionaryTests.UsingStrings.RunTests(
				() => new DefaultDictionary<string, string>(new Dictionary<string, string>(), key => string.Empty));
		}
	}
}
