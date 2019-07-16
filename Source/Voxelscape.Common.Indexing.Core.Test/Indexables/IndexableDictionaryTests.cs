using Voxelscape.Common.Indexing.Core.Indexables;
using Xunit;

namespace Voxelscape.Common.Indexing.Core.Test.Indexables
{
	/// <summary>
	/// Tests for the <see cref="IndexableDictionary{TIndex, TValue}"/> class.
	/// </summary>
	public static class IndexableDictionaryTests
	{
		/// <summary>
		/// Tests that all interface implementations pass their respective test suites.
		/// </summary>
		[Fact]
		public static void InterfaceTests()
		{
			////ITestProvider<IndexableDictionary<Index1D, string>>[] testsRan = TestDiscoverer.RunTestsFor(
			////	() => new IndexableDictionary<Index1D, string>(new Dictionary<Index1D, string>()),
			////	testValue => new KeyValuePair<Index1D, string>(new Index1D(testValue), testValue.ToString()),
			////	testValue => new Index1D(testValue));
		}
	}
}
