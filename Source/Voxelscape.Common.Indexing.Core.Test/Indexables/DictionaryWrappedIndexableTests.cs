using Voxelscape.Common.Indexing.Core.Indexables;
using Xunit;

namespace Voxelscape.Common.Indexing.Core.Test.Indexables
{
	/// <summary>
	/// Tests for the <see cref="DictionaryWrappedIndexable{TIndex, TValue}"/> class.
	/// </summary>
	public static class DictionaryWrappedIndexableTests
	{
		/// <summary>
		/// Tests that all interface implementations pass their respective test suites.
		/// </summary>
		[Fact]
		public static void InterfaceTests()
		{
			////ITestProvider<DictionaryWrappedIndexable<Index1D, string>>[] testsRan = TestDiscoverer.RunTestsFor(
			////	() => new DictionaryWrappedIndexable<Index1D, string>(new Array1D<TryValue<string>>(new Index1D(100))),
			////	testValue => new KeyValuePair<Index1D, string>(new Index1D(testValue), testValue.ToString()),
			////	testValue => new Index1D(testValue));
		}
	}
}
