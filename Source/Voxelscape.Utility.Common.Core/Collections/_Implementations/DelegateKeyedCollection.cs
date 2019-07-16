using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Voxelscape.Utility.Common.Pact.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Core.Collections
{
	public class DelegateKeyedCollection<TKey, TValue> :
		KeyedCollection<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>
	{
		private readonly Func<TValue, TKey> getKey;

		public DelegateKeyedCollection(Func<TValue, TKey> getKey)
		{
			Contracts.Requires.That(getKey != null);

			this.getKey = getKey;
		}

		public DelegateKeyedCollection(Func<TValue, TKey> getKey, IEqualityComparer<TKey> comparer)
			: base(comparer)
		{
			Contracts.Requires.That(getKey != null);

			this.getKey = getKey;
		}

		public DelegateKeyedCollection(Func<TValue, TKey> getKey, int dictionaryCreationThreshold)
			: base(null, dictionaryCreationThreshold)
		{
			Contracts.Requires.That(getKey != null);
			Contracts.Requires.That(dictionaryCreationThreshold >= -1);

			this.getKey = getKey;
		}

		public DelegateKeyedCollection(
			Func<TValue, TKey> getKey, IEqualityComparer<TKey> comparer, int dictionaryCreationThreshold)
			: base(comparer, dictionaryCreationThreshold)
		{
			Contracts.Requires.That(getKey != null);
			Contracts.Requires.That(dictionaryCreationThreshold >= -1);

			this.getKey = getKey;
		}

		/// <inheritdoc />
		public IEnumerable<TKey> Keys
		{
			get
			{
				foreach (var item in this)
				{
					yield return this.GetKeyForItem(item);
				}
			}
		}

		/// <inheritdoc />
		public IEnumerable<TValue> Values => this;

		public new bool Contains(TValue item) => this.Contains(this.GetKeyForItem(item));

		/// <inheritdoc />
		public bool ContainsKey(TKey key)
		{
			IReadOnlyDictionaryContracts.ContainsKey(key);

			return this.Contains(key);
		}

		/// <inheritdoc />
		public bool TryGetValue(TKey key, out TValue value)
		{
			IReadOnlyDictionaryContracts.TryGetValue(key);

			if (this.Dictionary != null)
			{
				return this.Dictionary.TryGetValue(key, out value);
			}

			foreach (var item in this)
			{
				if (this.Comparer.Equals(key, this.GetKeyForItem(item)))
				{
					value = item;
					return true;
				}
			}

			value = default(TValue);
			return false;
		}

		/// <inheritdoc />
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
		{
			foreach (var item in this)
			{
				yield return new KeyValuePair<TKey, TValue>(this.GetKeyForItem(item), item);
			}
		}

		/// <inheritdoc />
		protected override TKey GetKeyForItem(TValue item) => this.getKey(item);
	}
}
