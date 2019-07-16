using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Xunit;

namespace Voxelscape.Utility.Common.Pact.Test.Collections
{
	/// <summary>
	/// Tests for the <see cref="IEnumerableExtensions"/> class.
	/// </summary>
	public static class IEnumerableExtensionsTests
	{
		[Fact]
		public static void SymmetricExcept()
		{
			foreach (var data in SetData.Data)
			{
				TestSymmetricExcept(data.Item1, data.Item2);
			}
		}

		/// <summary>
		/// Tests that SymmetricExcept works.
		/// </summary>
		/// <param name="source">The source enumerable.</param>
		/// <param name="other">The other enumerable.</param>
		private static void TestSymmetricExcept(string[] source, string[] other)
		{
			Contracts.Requires.That(source != null);
			Contracts.Requires.That(other != null);

			IEnumerable<string> subject = source.SymmetricExcept(other);

			IEnumerable<string> expectedResult = source
				.Where(value => !other.Contains(value))
				.Concat(other.Where(value => !source.Contains(value)));

			subject.ElementsEqualPerOccurrence(expectedResult).Should().BeTrue();
		}
	}
}
