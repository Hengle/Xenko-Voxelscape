using System.Collections.Generic;
using System.Linq;
using Voxelscape.Utility.Common.Core.Collections;

namespace Voxelscape.Utility.Common.Pact.Test.Collections
{
	/// <summary>
	/// Provides data for use in tests of Sets or Set-like functionality.
	/// </summary>
	public static class SetData
	{
		/// <summary>
		/// Gets the data set to use for the tests of Sets.
		/// </summary>
		/// <value>The set data.</value>
		public static IEnumerable<TupleStruct<string[], string[]>> Data =>
			GetData().Concat(GetData().Select(tuple => TupleStruct.Create(tuple.Item2, tuple.Item1)));

		private static string[] Empty => ArrayUtilities.Empty<string>();

		private static IEnumerable<TupleStruct<string[], string[]>> GetData()
		{
			yield return TupleStruct.Create(new[] { "a" }, Empty);
			yield return TupleStruct.Create(new[] { "a", "b" }, Empty);
			yield return TupleStruct.Create(new[] { "a", "b", "c" }, Empty);
			yield return TupleStruct.Create(new[] { "a" }, new[] { "a" });
			yield return TupleStruct.Create(new[] { "a", "b" }, new string[] { "a", "b" });
			yield return TupleStruct.Create(new[] { "a" }, new string[] { "b" });
			yield return TupleStruct.Create(new[] { "a", "b" }, new string[] { "a" });
			yield return TupleStruct.Create(new[] { "a", "b" }, new string[] { "b" });
			yield return TupleStruct.Create(new[] { "a", "b" }, new string[] { "c" });
			yield return TupleStruct.Create(new[] { "a", "b" }, new string[] { "b", "c" });
			yield return TupleStruct.Create(new[] { "a", "b", "c" }, new string[] { "c", "d", "a" });
			yield return TupleStruct.Create(new[] { "a", "b", "c" }, new string[] { "b", "c", "d" });
		}
	}
}
