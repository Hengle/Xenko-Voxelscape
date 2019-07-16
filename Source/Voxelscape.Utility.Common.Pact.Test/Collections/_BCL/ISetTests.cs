using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Voxelscape.Utility.Common.Core.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Pact.Test.Collections
{
	/// <summary>
	/// Tests for implementations of the <see cref="ISet{T}"/> interface.
	/// </summary>
	public static class ISetTests
	{
		/// <summary>
		/// Tests that Add works.
		/// </summary>
		/// <typeparam name="T">The type of values stored in the set.</typeparam>
		/// <param name="subject">The subject.</param>
		/// <param name="valuesToAdd">The values to add.</param>
		public static void Add<T>(ISet<T> subject, params T[] valuesToAdd)
		{
			Contracts.Requires.That(subject != null);
			Contracts.Requires.That(valuesToAdd != null);

			foreach (T value in valuesToAdd)
			{
				bool shouldValueBeAdded = !subject.Contains(value);

				bool wasValueAdded = subject.Add(value);

				// assert was was added
				wasValueAdded.Should().Be(shouldValueBeAdded);
			}

			// asserts
			subject.Count.Should().Be(valuesToAdd.RemoveDuplicates().Count());
			foreach (T value in valuesToAdd)
			{
				subject.Contains(value).Should().BeTrue();
			}
		}

		/// <summary>
		/// Tests that ExceptWith works.
		/// </summary>
		/// <typeparam name="T">The type of values stored in the set.</typeparam>
		/// <param name="subject">The subject.</param>
		/// <param name="valuesToAdd">The values to add.</param>
		/// <param name="other">The other enumerable to perform the method with.</param>
		public static void ExceptWith<T>(ISet<T> subject, T[] valuesToAdd, T[] other)
		{
			Contracts.Requires.That(subject != null);
			Contracts.Requires.That(valuesToAdd != null);
			Contracts.Requires.That(other != null);

			foreach (T value in valuesToAdd)
			{
				subject.Add(value);
			}

			subject.ExceptWith(other);

			// asserts
			IEnumerable<T> expectedResult = valuesToAdd.Except(other);
			subject.Count.Should().Be(expectedResult.Count());
			subject.ElementsEqualPerOccurrence(expectedResult).Should().BeTrue();
		}

		/// <summary>
		/// Tests that IntersectWith works.
		/// </summary>
		/// <typeparam name="T">The type of values stored in the set.</typeparam>
		/// <param name="subject">The subject.</param>
		/// <param name="valuesToAdd">The values to add.</param>
		/// <param name="other">The other enumerable to perform the method with.</param>
		public static void IntersectWith<T>(ISet<T> subject, T[] valuesToAdd, T[] other)
		{
			Contracts.Requires.That(subject != null);
			Contracts.Requires.That(valuesToAdd != null);
			Contracts.Requires.That(other != null);

			foreach (T value in valuesToAdd)
			{
				subject.Add(value);
			}

			subject.IntersectWith(other);

			// asserts
			IEnumerable<T> expectedResult = valuesToAdd.Intersect(other);
			subject.Count.Should().Be(expectedResult.Count());
			subject.ElementsEqualPerOccurrence(expectedResult).Should().BeTrue();
		}

		/// <summary>
		/// Tests that UnionWith works.
		/// </summary>
		/// <typeparam name="T">The type of values stored in the set.</typeparam>
		/// <param name="subject">The subject.</param>
		/// <param name="valuesToAdd">The values to add.</param>
		/// <param name="other">The other enumerable to perform the method with.</param>
		public static void UnionWith<T>(ISet<T> subject, T[] valuesToAdd, T[] other)
		{
			Contracts.Requires.That(subject != null);
			Contracts.Requires.That(valuesToAdd != null);
			Contracts.Requires.That(other != null);

			foreach (T value in valuesToAdd)
			{
				subject.Add(value);
			}

			subject.UnionWith(other);

			// asserts
			IEnumerable<T> expectedResult = valuesToAdd.Union(other);
			subject.Count.Should().Be(expectedResult.Count());
			subject.ElementsEqualPerOccurrence(expectedResult).Should().BeTrue();
		}

		/// <summary>
		/// Tests that SymmetricExceptWith works.
		/// </summary>
		/// <typeparam name="T">The type of values stored in the set.</typeparam>
		/// <param name="subject">The subject.</param>
		/// <param name="valuesToAdd">The values to add.</param>
		/// <param name="other">The other enumerable to perform the method with.</param>
		public static void SymmetricExceptWith<T>(ISet<T> subject, T[] valuesToAdd, T[] other)
		{
			Contracts.Requires.That(subject != null);
			Contracts.Requires.That(valuesToAdd != null);
			Contracts.Requires.That(other != null);

			foreach (T value in valuesToAdd)
			{
				subject.Add(value);
			}

			subject.SymmetricExceptWith(other);

			// asserts
			IEnumerable<T> expectedResult = valuesToAdd.SymmetricExcept(other);
			subject.Count.Should().Be(expectedResult.Count());
			subject.ElementsEqualPerOccurrence(expectedResult).Should().BeTrue();
		}

		/// <summary>
		/// Tests that Overlaps works.
		/// </summary>
		/// <typeparam name="T">The type of values stored in the set.</typeparam>
		/// <param name="subject">The subject.</param>
		/// <param name="valuesToAdd">The values to add.</param>
		/// <param name="other">The other enumerable to perform the method with.</param>
		public static void Overlaps<T>(ISet<T> subject, T[] valuesToAdd, T[] other)
		{
			Contracts.Requires.That(subject != null);
			Contracts.Requires.That(valuesToAdd != null);
			Contracts.Requires.That(other != null);

			foreach (T value in valuesToAdd)
			{
				subject.Add(value);
			}

			bool result = subject.Overlaps(other);

			// asserts
			bool expectedResult = valuesToAdd.Any(value => other.Contains(value));
			result.Should().Be(expectedResult);
		}

		/// <summary>
		/// Tests that SetEquals works.
		/// </summary>
		/// <typeparam name="T">The type of values stored in the set.</typeparam>
		/// <param name="subject">The subject.</param>
		/// <param name="valuesToAdd">The values to add.</param>
		/// <param name="other">The other enumerable to perform the method with.</param>
		public static void SetEquals<T>(ISet<T> subject, T[] valuesToAdd, T[] other)
		{
			Contracts.Requires.That(subject != null);
			Contracts.Requires.That(valuesToAdd != null);
			Contracts.Requires.That(other != null);

			foreach (T value in valuesToAdd)
			{
				subject.Add(value);
			}

			bool result = subject.SetEquals(other);

			// asserts
			bool expectedResult = valuesToAdd.ElementsEqual(other);
			result.Should().Be(expectedResult);
		}

		/// <summary>
		/// Tests that IsProperSubsetOf works.
		/// </summary>
		/// <typeparam name="T">The type of values stored in the set.</typeparam>
		/// <param name="subject">The subject.</param>
		/// <param name="valuesToAdd">The values to add.</param>
		/// <param name="other">The other enumerable to perform the method with.</param>
		/// <param name="expectedResult">The expected result.</param>
		public static void IsProperSubsetOf<T>(ISet<T> subject, T[] valuesToAdd, T[] other, bool expectedResult)
		{
			Contracts.Requires.That(subject != null);
			Contracts.Requires.That(valuesToAdd != null);
			Contracts.Requires.That(other != null);

			foreach (T value in valuesToAdd)
			{
				subject.Add(value);
			}

			bool result = subject.IsProperSubsetOf(other);

			// asserts
			result.Should().Be(expectedResult);
		}

		/// <summary>
		/// Tests that IsProperSupersetOf works.
		/// </summary>
		/// <typeparam name="T">The type of values stored in the set.</typeparam>
		/// <param name="subject">The subject.</param>
		/// <param name="other">The other enumerable to perform the method with.</param>
		/// <param name="valuesToAdd">The values to add.</param>
		/// <param name="expectedResult">The expected result.</param>
		public static void IsProperSupersetOf<T>(ISet<T> subject, T[] other, T[] valuesToAdd, bool expectedResult)
		{
			Contracts.Requires.That(subject != null);
			Contracts.Requires.That(valuesToAdd != null);
			Contracts.Requires.That(other != null);

			// valuesToAdd and other have been swapped in order to reverse the subset/superset notion
			// allowing the same set of test data to be used
			foreach (T value in valuesToAdd)
			{
				subject.Add(value);
			}

			bool result = subject.IsProperSupersetOf(other);

			// asserts
			result.Should().Be(expectedResult);
		}

		/// <summary>
		/// Tests that IsSubsetOf works.
		/// </summary>
		/// <typeparam name="T">The type of values stored in the set.</typeparam>
		/// <param name="subject">The subject.</param>
		/// <param name="valuesToAdd">The values to add.</param>
		/// <param name="other">The other enumerable to perform the method with.</param>
		/// <param name="expectedResult">The expected result.</param>
		public static void IsSubsetOf<T>(ISet<T> subject, T[] valuesToAdd, T[] other, bool expectedResult)
		{
			Contracts.Requires.That(subject != null);
			Contracts.Requires.That(valuesToAdd != null);
			Contracts.Requires.That(other != null);

			foreach (T value in valuesToAdd)
			{
				subject.Add(value);
			}

			bool result = subject.IsSubsetOf(other);

			// asserts
			result.Should().Be(expectedResult);
		}

		/// <summary>
		/// Tests that IsSupersetOf works.
		/// </summary>
		/// <typeparam name="T">The type of values stored in the set.</typeparam>
		/// <param name="subject">The subject.</param>
		/// <param name="other">The other enumerable to perform the method with.</param>
		/// <param name="valuesToAdd">The values to add.</param>
		/// <param name="expectedResult">The expected result.</param>
		public static void IsSupersetOf<T>(ISet<T> subject, T[] other, T[] valuesToAdd, bool expectedResult)
		{
			Contracts.Requires.That(subject != null);
			Contracts.Requires.That(valuesToAdd != null);
			Contracts.Requires.That(other != null);

			// valuesToAdd and other have been swapped in order to reverse the subset/superset notion
			// allowing the same set of test data to be used
			foreach (T value in valuesToAdd)
			{
				subject.Add(value);
			}

			bool result = subject.IsSupersetOf(other);

			// asserts
			result.Should().Be(expectedResult);
		}

		public static class UsingStrings
		{
			public static void RunTests(Func<ISet<string>> createSubject)
			{
				Contracts.Requires.That(createSubject != null);

				Add(createSubject);
				ExceptWith(createSubject);
				IntersectWith(createSubject);
				UnionWith(createSubject);
				SymmetricExceptWith(createSubject);
				Overlaps(createSubject);
				SetEquals(createSubject);
				IsProperSubsetOf(createSubject);
				IsProperSupersetOf(createSubject);
				IsSubsetOf(createSubject);
				IsSupersetOf(createSubject);
			}

			public static void Add(Func<ISet<string>> createSubject)
			{
				Contracts.Requires.That(createSubject != null);

				ISetTests.Add(createSubject(), "a");
				ISetTests.Add(createSubject(), "a", "b");
				ISetTests.Add(createSubject(), "a", "a");
				ISetTests.Add(createSubject(), "a", "b", "a");
				ISetTests.Add(createSubject(), "a", "b", "b");
				ISetTests.Add(createSubject(), "a", "b", "c", "a");
			}

			public static void ExceptWith(Func<ISet<string>> createSubject)
			{
				Contracts.Requires.That(createSubject != null);

				foreach (var data in SetData.Data)
				{
					ISetTests.ExceptWith(createSubject(), data.Item1, data.Item2);
				}
			}

			public static void IntersectWith(Func<ISet<string>> createSubject)
			{
				Contracts.Requires.That(createSubject != null);

				foreach (var data in SetData.Data)
				{
					ISetTests.IntersectWith(createSubject(), data.Item1, data.Item2);
				}
			}

			public static void UnionWith(Func<ISet<string>> createSubject)
			{
				Contracts.Requires.That(createSubject != null);

				foreach (var data in SetData.Data)
				{
					ISetTests.UnionWith(createSubject(), data.Item1, data.Item2);
				}
			}

			public static void SymmetricExceptWith(Func<ISet<string>> createSubject)
			{
				Contracts.Requires.That(createSubject != null);

				foreach (var data in SetData.Data)
				{
					ISetTests.SymmetricExceptWith(createSubject(), data.Item1, data.Item2);
				}
			}

			public static void Overlaps(Func<ISet<string>> createSubject)
			{
				Contracts.Requires.That(createSubject != null);

				foreach (var data in SetData.Data)
				{
					ISetTests.Overlaps(createSubject(), data.Item1, data.Item2);
				}
			}

			public static void SetEquals(Func<ISet<string>> createSubject)
			{
				Contracts.Requires.That(createSubject != null);

				foreach (var data in SetData.Data)
				{
					ISetTests.SetEquals(createSubject(), data.Item1, data.Item2);
				}
			}

			public static void IsProperSubsetOf(Func<ISet<string>> createSubject)
			{
				Contracts.Requires.That(createSubject != null);

				foreach (var data in ProperSubsetSupersetData())
				{
					ISetTests.IsProperSubsetOf(createSubject(), data.Item1, data.Item2, data.Item3);
				}
			}

			public static void IsProperSupersetOf(Func<ISet<string>> createSubject)
			{
				Contracts.Requires.That(createSubject != null);

				foreach (var data in ProperSubsetSupersetData())
				{
					ISetTests.IsProperSupersetOf(createSubject(), data.Item1, data.Item2, data.Item3);
				}
			}

			public static void IsSubsetOf(Func<ISet<string>> createSubject)
			{
				Contracts.Requires.That(createSubject != null);

				foreach (var data in SubsetSupersetData())
				{
					ISetTests.IsSubsetOf(createSubject(), data.Item1, data.Item2, data.Item3);
				}
			}

			public static void IsSupersetOf(Func<ISet<string>> createSubject)
			{
				Contracts.Requires.That(createSubject != null);

				foreach (var data in SubsetSupersetData())
				{
					ISetTests.IsSupersetOf(createSubject(), data.Item1, data.Item2, data.Item3);
				}
			}

			/// <summary>
			/// The data set to use for the proper subset/superset tests.
			/// </summary>
			/// <returns>The data set.</returns>
			private static IEnumerable<TupleStruct<string[], string[], bool>> ProperSubsetSupersetData()
			{
				yield return TupleStruct.Create(new string[] { }, new string[] { }, false);
				yield return TupleStruct.Create(new string[] { }, new string[] { "a" }, true);
				yield return TupleStruct.Create(new string[] { "a" }, new string[] { "a" }, false);
				yield return TupleStruct.Create(new string[] { "a" }, new string[] { "b" }, false);
				yield return TupleStruct.Create(new string[] { "a" }, new string[] { "a", "b" }, true);
				yield return TupleStruct.Create(new string[] { "a", "b" }, new string[] { "a", "c" }, false);
			}

			/// <summary>
			/// The data set to use for the subset/superset tests.
			/// </summary>
			/// <returns>The data set.</returns>
			private static IEnumerable<TupleStruct<string[], string[], bool>> SubsetSupersetData()
			{
				yield return TupleStruct.Create(new string[] { }, new string[] { }, true);
				yield return TupleStruct.Create(new string[] { }, new string[] { "a" }, true);
				yield return TupleStruct.Create(new string[] { "a" }, new string[] { "a" }, true);
				yield return TupleStruct.Create(new string[] { "a" }, new string[] { "b" }, false);
				yield return TupleStruct.Create(new string[] { "a" }, new string[] { "a", "b" }, true);
				yield return TupleStruct.Create(new string[] { "a", "b" }, new string[] { "a", "c" }, false);
			}
		}
	}
}
