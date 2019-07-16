using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Pact.Test.Collections
{
	/// <summary>
	/// Tests for implementations of the <see cref="ICollection{T}" /> interface.
	/// </summary>
	public static class ICollectionTests
	{
		/// <summary>
		/// Tests that the Add method works.
		/// </summary>
		/// <typeparam name="T">The type of the values.</typeparam>
		/// <param name="subject">The subject.</param>
		/// <param name="valuesToAdd">The values to add.</param>
		/// <remarks>
		/// This method only tests adding unique values and there is no test for adding duplicates because
		/// <see cref="IDictionary{TKey, TValue}" /> and <see cref="ISet{T}" /> both extend <see cref="ICollection{T}" />
		/// and handle adding duplicates in contradictory ways. This makes a uniform approach to testing not ideal.
		/// </remarks>
		public static void Add<T>(ICollection<T> subject, params T[] valuesToAdd)
		{
			Contracts.Requires.That(subject != null);
			Contracts.Requires.That(valuesToAdd != null);

			foreach (var value in valuesToAdd)
			{
				subject.Add(value);
			}

			// asserts
			subject.Count.Should().Be(valuesToAdd.Length);
			foreach (var value in valuesToAdd)
			{
				subject.Contains(value).Should().BeTrue();
			}
		}

		/// <summary>
		/// Tests that Remove works.
		/// </summary>
		/// <typeparam name="T">The type of the values.</typeparam>
		/// <param name="subject">The subject.</param>
		/// <param name="valuesToAdd">The values to add.</param>
		/// <param name="valueToRemove">The value to remove.</param>
		public static void Remove<T>(ICollection<T> subject, T[] valuesToAdd, T valueToRemove)
		{
			Contracts.Requires.That(subject != null);
			Contracts.Requires.That(valuesToAdd != null);
			Contracts.Requires.That(valueToRemove != null);

			foreach (var value in valuesToAdd)
			{
				subject.Add(value);
			}

			bool wasValueRemoved = subject.Remove(valueToRemove);

			// asserts
			if (valuesToAdd.Contains(valueToRemove))
			{
				wasValueRemoved.Should().BeTrue();
				subject.Count.Should().Be(valuesToAdd.Length - 1);
			}
			else
			{
				wasValueRemoved.Should().BeFalse();
				subject.Count.Should().Be(valuesToAdd.Length);
			}

			foreach (var value in valuesToAdd.Except(new[] { valueToRemove }))
			{
				subject.Contains(value).Should().BeTrue();
			}
		}

		/// <summary>
		/// Tests that Remove works when called multiple times.
		/// </summary>
		/// <typeparam name="T">The type of the values.</typeparam>
		/// <param name="subject">The subject.</param>
		/// <param name="valuesToAdd">The values to add.</param>
		/// <param name="valuesToRemove">The values to remove.</param>
		/// <param name="expectedWasValueRemoved">Whether or not the last value is expected to have been removed.</param>
		public static void RemoveMany<T>(
			ICollection<T> subject, T[] valuesToAdd, T[] valuesToRemove, bool expectedWasValueRemoved)
		{
			Contracts.Requires.That(subject != null);
			Contracts.Requires.That(valuesToAdd != null);
			Contracts.Requires.That(valuesToRemove != null);

			foreach (var value in valuesToAdd)
			{
				subject.Add(value);
			}

			bool wasLastValueRemoved = false;

			foreach (var value in valuesToRemove)
			{
				wasLastValueRemoved = subject.Remove(value);
			}

			wasLastValueRemoved.Should().Be(expectedWasValueRemoved);
		}

		/// <summary>
		/// Tests that Clear works.
		/// </summary>
		/// <typeparam name="T">The type of the values.</typeparam>
		/// <param name="subject">The subject.</param>
		/// <param name="valuesToAdd">The values to add.</param>
		public static void Clear<T>(ICollection<T> subject, params T[] valuesToAdd)
		{
			Contracts.Requires.That(subject != null);
			Contracts.Requires.That(valuesToAdd != null);

			foreach (var value in valuesToAdd)
			{
				subject.Add(value);
			}

			subject.Clear();

			// asserts
			subject.Count.Should().Be(0);
			foreach (var value in valuesToAdd)
			{
				subject.Contains(value).Should().BeFalse();
			}
		}

		/// <summary>
		/// Tests that adding after a clear works.
		/// </summary>
		/// <typeparam name="T">The type of the values.</typeparam>
		/// <param name="subject">The subject.</param>
		/// <param name="initialValuesToAdd">The values to add.</param>
		/// <param name="valuesToAddAfterwards">The values to add after clear is called.</param>
		public static void AddAfterClear<T>(
			ICollection<T> subject, T[] initialValuesToAdd, T[] valuesToAddAfterwards)
		{
			Contracts.Requires.That(subject != null);
			Contracts.Requires.That(initialValuesToAdd != null);
			Contracts.Requires.That(valuesToAddAfterwards != null);

			foreach (var value in initialValuesToAdd)
			{
				subject.Add(value);
			}

			subject.Clear();

			foreach (var value in valuesToAddAfterwards)
			{
				subject.Add(value);
			}

			// asserts
			subject.Count.Should().Be(valuesToAddAfterwards.Length);
			foreach (var value in valuesToAddAfterwards)
			{
				subject.Contains(value).Should().BeTrue();
			}
		}

		/// <summary>
		/// Tests that CopyTo works.
		/// </summary>
		/// <typeparam name="T">The type of the values.</typeparam>
		/// <param name="subject">The subject.</param>
		/// <param name="valuesToAdd">The values to add.</param>
		/// <param name="sizeOfArray">The size of the array to copy to.</param>
		/// <param name="arrayIndex">Index of the array to begin the copy to on.</param>
		public static void CopyTo<T>(ICollection<T> subject, T[] valuesToAdd, int sizeOfArray, int arrayIndex)
		{
			Contracts.Requires.That(subject != null);
			Contracts.Requires.That(valuesToAdd != null);
			Contracts.Requires.That(sizeOfArray > 0);
			Contracts.Requires.That(arrayIndex >= 0);

			foreach (var value in valuesToAdd)
			{
				subject.Add(value);
			}

			var array = new T[sizeOfArray];

			subject.CopyTo(array, arrayIndex);

			// asserts
			for (int index = arrayIndex; index < valuesToAdd.Length + arrayIndex; index++)
			{
				valuesToAdd.Contains(array[index]).Should().BeTrue();
			}
		}

		public static class UsingStrings
		{
			public static void RunTests(Func<ICollection<string>> createSubject)
			{
				Contracts.Requires.That(createSubject != null);

				Add(createSubject);
				Remove(createSubject);
				RemoveMany(createSubject);
				Clear(createSubject);
				AddAfterClear(createSubject);
				CopyTo(createSubject);
			}

			public static void Add(Func<ICollection<string>> createSubject)
			{
				Contracts.Requires.That(createSubject != null);

				ICollectionTests.Add(createSubject(), "a");
				ICollectionTests.Add(createSubject(), "a", "b");
				ICollectionTests.Add(createSubject(), "a", "b", "c");
			}

			public static void Remove(Func<ICollection<string>> createSubject)
			{
				Contracts.Requires.That(createSubject != null);

				ICollectionTests.Remove(createSubject(), new[] { "a" }, "a");
				ICollectionTests.Remove(createSubject(), new[] { "a" }, "b");
				ICollectionTests.Remove(createSubject(), new[] { "a", "b" }, "a");
				ICollectionTests.Remove(createSubject(), new[] { "a", "b" }, "b");
				ICollectionTests.Remove(createSubject(), new[] { "a", "b" }, "c");
				ICollectionTests.Remove(createSubject(), new[] { "a", "b", "c" }, "a");
				ICollectionTests.Remove(createSubject(), new[] { "a", "b", "c" }, "b");
				ICollectionTests.Remove(createSubject(), new[] { "a", "b", "c" }, "c");
				ICollectionTests.Remove(createSubject(), new[] { "a", "b", "c" }, "d");
			}

			public static void RemoveMany(Func<ICollection<string>> createSubject)
			{
				Contracts.Requires.That(createSubject != null);

				ICollectionTests.RemoveMany(createSubject(), new[] { "a" }, new[] { "a" }, true);
				ICollectionTests.RemoveMany(createSubject(), new[] { "a" }, new[] { "a", "a" }, false);
				ICollectionTests.RemoveMany(createSubject(), new[] { "a", "b" }, new[] { "a", "b" }, true);
				ICollectionTests.RemoveMany(createSubject(), new[] { "a", "b" }, new[] { "b", "a" }, true);
				ICollectionTests.RemoveMany(createSubject(), new[] { "a", "b" }, new[] { "a", "a" }, false);
				ICollectionTests.RemoveMany(createSubject(), new[] { "a", "b" }, new[] { "c" }, false);
				ICollectionTests.RemoveMany(createSubject(), new[] { "a", "b" }, new[] { "c", "a" }, true);
			}

			public static void Clear(Func<ICollection<string>> createSubject)
			{
				Contracts.Requires.That(createSubject != null);

				ICollectionTests.Clear(createSubject(), "a");
				ICollectionTests.Clear(createSubject(), "a", "b");
				ICollectionTests.Clear(createSubject(), "a", "b", "c");
			}

			public static void AddAfterClear(Func<ICollection<string>> createSubject)
			{
				Contracts.Requires.That(createSubject != null);

				ICollectionTests.AddAfterClear(createSubject(), new[] { "a" }, new[] { "a" });
				ICollectionTests.AddAfterClear(createSubject(), new[] { "a" }, new[] { "b" });
				ICollectionTests.AddAfterClear(createSubject(), new[] { "a" }, new[] { "a", "b" });
				ICollectionTests.AddAfterClear(createSubject(), new[] { "a" }, new[] { "b", "c" });
				ICollectionTests.AddAfterClear(createSubject(), new[] { "a", "b" }, new[] { "a" });
				ICollectionTests.AddAfterClear(createSubject(), new[] { "a", "b" }, new[] { "b" });
				ICollectionTests.AddAfterClear(createSubject(), new[] { "a", "b" }, new[] { "c" });
				ICollectionTests.AddAfterClear(createSubject(), new[] { "a", "b" }, new[] { "c", "d" });
			}

			public static void CopyTo(Func<ICollection<string>> createSubject)
			{
				Contracts.Requires.That(createSubject != null);

				ICollectionTests.CopyTo(createSubject(), new[] { "a" }, 1, 0);
			}
		}
	}
}
