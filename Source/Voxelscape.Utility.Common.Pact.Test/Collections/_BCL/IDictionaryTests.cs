using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Pact.Test.Collections
{
	/// <summary>
	/// Tests for implementations of the <see cref="IDictionary{TKey, TValue}"/> interface.
	/// </summary>
	public static class IDictionaryTests
	{
		/// <summary>
		/// Tests that Add works.
		/// </summary>
		/// <typeparam name="TKey">The type of the key.</typeparam>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <param name="subject">The subject.</param>
		/// <param name="pairsToAdd">The key value pairs to add.</param>
		public static void Add<TKey, TValue>(
			IDictionary<TKey, TValue> subject, params KeyValuePair<TKey, TValue>[] pairsToAdd)
		{
			Contracts.Requires.That(subject != null);
			Contracts.Requires.That(pairsToAdd != null);

			foreach (var pair in pairsToAdd)
			{
				subject.Add(pair.Key, pair.Value);
			}

			// asserts
			subject.Count.Should().Be(pairsToAdd.Length);
			foreach (var pair in pairsToAdd)
			{
				subject.ContainsKey(pair.Key).Should().BeTrue();
			}
		}

		/// <summary>
		/// Tests that Remove works.
		/// </summary>
		/// <typeparam name="TKey">The type of the key.</typeparam>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <param name="subject">The subject.</param>
		/// <param name="keysToAdd">The keys to add (use default for values).</param>
		/// <param name="keyToRemove">The key to remove.</param>
		public static void Remove<TKey, TValue>(
			IDictionary<TKey, TValue> subject, TKey[] keysToAdd, TKey keyToRemove)
		{
			Contracts.Requires.That(subject != null);
			Contracts.Requires.That(keysToAdd != null);

			foreach (var value in keysToAdd)
			{
				subject.Add(value, default(TValue));
			}

			bool wasValueRemoved = subject.Remove(keyToRemove);

			// asserts
			if (keysToAdd.Contains(keyToRemove))
			{
				wasValueRemoved.Should().BeTrue();
				subject.Count.Should().Be(keysToAdd.Length - 1);
			}
			else
			{
				wasValueRemoved.Should().BeFalse();
				subject.Count.Should().Be(keysToAdd.Length);
			}

			foreach (var value in keysToAdd.Except(new[] { keyToRemove }))
			{
				subject.ContainsKey(value).Should().BeTrue();
			}
		}

		/// <summary>
		/// Tests that TryGetValue works when the value is present to be retrieved.
		/// </summary>
		/// <typeparam name="TKey">The type of the key.</typeparam>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <param name="subject">The subject.</param>
		/// <param name="pairToAdd">The key value pair to add and retrieve.</param>
		public static void TryGetValueSuccess<TKey, TValue>(
			IDictionary<TKey, TValue> subject, KeyValuePair<TKey, TValue> pairToAdd)
		{
			Contracts.Requires.That(subject != null);

			subject.Add(pairToAdd);

			TValue valueRetrieved;
			bool wasValueRetrieved = subject.TryGetValue(pairToAdd.Key, out valueRetrieved);

			wasValueRetrieved.Should().BeTrue();
			valueRetrieved.Should().BeSameAs(pairToAdd.Value);
		}

		/// <summary>
		/// Tests that TryGetValue works when the value is not able to be retrieved.
		/// </summary>
		/// <typeparam name="TKey">The type of the key.</typeparam>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <param name="subject">The subject.</param>
		/// <param name="keyToRetrieve">The key to retrieve.</param>
		public static void TryGetValueFail<TKey, TValue>(IDictionary<TKey, TValue> subject, TKey keyToRetrieve)
		{
			Contracts.Requires.That(subject != null);

			// this check is necessary because DefaultDictionary always returns a value
			if (subject.ContainsKey(keyToRetrieve))
			{
				return;
			}

			TValue valueRetrieved;
			bool wasValueRetrieved = subject.TryGetValue(keyToRetrieve, out valueRetrieved);

			wasValueRetrieved.Should().BeFalse();
			valueRetrieved.Should().Be(default(string));
		}

		/// <summary>
		/// Tests that indexer works.
		/// </summary>
		/// <typeparam name="TKey">The type of the key.</typeparam>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <param name="subject">The subject.</param>
		/// <param name="pairsToAdd">The key value pairs to add.</param>
		public static void ItemIndexer<TKey, TValue>(
			IDictionary<TKey, TValue> subject, params KeyValuePair<TKey, TValue>[] pairsToAdd)
		{
			Contracts.Requires.That(subject != null);
			Contracts.Requires.That(pairsToAdd != null);

			foreach (var pair in pairsToAdd)
			{
				subject[pair.Key] = pair.Value;
			}

			// asserts
			foreach (var pair in pairsToAdd)
			{
				subject[pair.Key].Should().BeSameAs(pair.Value);
			}
		}

		/// <summary>
		/// Tests that the Keys collection works.
		/// </summary>
		/// <typeparam name="TKey">The type of the key.</typeparam>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <param name="subject">The subject.</param>
		/// <param name="keysToAdd">The keys to add.</param>
		public static void Keys<TKey, TValue>(IDictionary<TKey, TValue> subject, params TKey[] keysToAdd)
		{
			Contracts.Requires.That(subject != null);
			Contracts.Requires.That(keysToAdd != null);

			foreach (var key in keysToAdd)
			{
				subject[key] = default(TValue);
			}

			// asserts
			subject.Keys.Count.Should().Be(keysToAdd.Length);
			subject.Keys.ElementsEqualPerOccurrence(keysToAdd);
			subject.Keys.IsReadOnly.Should().BeTrue();
		}

		/// <summary>
		/// Tests that the Values collection works.
		/// </summary>
		/// <typeparam name="TKey">The type of the key.</typeparam>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <param name="subject">The subject.</param>
		/// <param name="pairsToAdd">The key value pairs to add.</param>
		public static void Values<TKey, TValue>(
			IDictionary<TKey, TValue> subject, params KeyValuePair<TKey, TValue>[] pairsToAdd)
		{
			Contracts.Requires.That(subject != null);
			Contracts.Requires.That(pairsToAdd != null);

			foreach (var pair in pairsToAdd)
			{
				subject[pair.Key] = pair.Value;
			}

			// asserts
			subject.Values.Count.Should().Be(pairsToAdd.Length);
			subject.Values.ElementsEqualPerOccurrence(pairsToAdd.Select(pair => pair.Value));
			subject.Values.IsReadOnly.Should().BeTrue();
		}

		public static class UsingStrings
		{
			public static void RunTests(Func<IDictionary<string, string>> createSubject)
			{
				Contracts.Requires.That(createSubject != null);

				Add(createSubject);
				Remove(createSubject);
				TryGetValueSuccess(createSubject);
				TryGetValueFail(createSubject);
				ItemIndexer(createSubject);
				Keys(createSubject);
				Values(createSubject);
			}

			public static void Add(Func<IDictionary<string, string>> createSubject)
			{
				Contracts.Requires.That(createSubject != null);

				IDictionaryTests.Add(createSubject(), NewPair("a"));
				IDictionaryTests.Add(createSubject(), NewPair("a"), NewPair("b"));
				IDictionaryTests.Add(createSubject(), NewPair("a"), NewPair("b"), NewPair("c"));
			}

			public static void Remove(Func<IDictionary<string, string>> createSubject)
			{
				Contracts.Requires.That(createSubject != null);

				IDictionaryTests.Remove(createSubject(), new[] { "a" }, "a");
				IDictionaryTests.Remove(createSubject(), new[] { "a" }, "b");
				IDictionaryTests.Remove(createSubject(), new[] { "a", "b" }, "a");
				IDictionaryTests.Remove(createSubject(), new[] { "a", "b" }, "b");
				IDictionaryTests.Remove(createSubject(), new[] { "a", "b" }, "c");
				IDictionaryTests.Remove(createSubject(), new[] { "a", "b", "c" }, "a");
				IDictionaryTests.Remove(createSubject(), new[] { "a", "b", "c" }, "b");
				IDictionaryTests.Remove(createSubject(), new[] { "a", "b", "c" }, "c");
				IDictionaryTests.Remove(createSubject(), new[] { "a", "b", "c" }, "d");
			}

			public static void TryGetValueSuccess(Func<IDictionary<string, string>> createSubject)
			{
				Contracts.Requires.That(createSubject != null);

				IDictionaryTests.TryGetValueSuccess(createSubject(), NewPair("a"));
			}

			public static void TryGetValueFail(Func<IDictionary<string, string>> createSubject)
			{
				Contracts.Requires.That(createSubject != null);

				IDictionaryTests.TryGetValueFail(createSubject(), "a");
			}

			public static void ItemIndexer(Func<IDictionary<string, string>> createSubject)
			{
				Contracts.Requires.That(createSubject != null);

				IDictionaryTests.ItemIndexer(createSubject(), NewPair("a"));
				IDictionaryTests.ItemIndexer(createSubject(), NewPair("a"), NewPair("b"));
				IDictionaryTests.ItemIndexer(createSubject(), NewPair("a"), NewPair("b"), NewPair("c"));
			}

			public static void Keys(Func<IDictionary<string, string>> createSubject)
			{
				Contracts.Requires.That(createSubject != null);

				IDictionaryTests.Keys(createSubject(), "a");
				IDictionaryTests.Keys(createSubject(), "a", "b");
				IDictionaryTests.Keys(createSubject(), "a", "b", "c");
			}

			public static void Values(Func<IDictionary<string, string>> createSubject)
			{
				Contracts.Requires.That(createSubject != null);

				IDictionaryTests.Values(createSubject(), NewPair("a"));
				IDictionaryTests.Values(createSubject(), NewPair("a"), NewPair("b"));
				IDictionaryTests.Values(createSubject(), NewPair("a"), NewPair("b"), NewPair("c"));
			}

			private static KeyValuePair<string, string> NewPair(string key) =>
				new KeyValuePair<string, string>(key, string.Empty);
		}
	}
}
