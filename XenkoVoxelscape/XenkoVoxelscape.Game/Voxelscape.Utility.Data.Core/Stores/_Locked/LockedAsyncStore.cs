﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Nito.AsyncEx;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Stores;

namespace Voxelscape.Utility.Data.Core.Stores
{
	/// <summary>
	///
	/// </summary>
	public class LockedAsyncStore : IAsyncStore
	{
		private readonly IAsyncStore store;

		public LockedAsyncStore(IAsyncStore store)
		{
			Contracts.Requires.That(store != null);

			this.store = store;
		}

		protected AsyncReaderWriterLock Lock { get; } = new AsyncReaderWriterLock();

		/// <inheritdoc />
		public async Task<IEnumerable<TEntity>> AllAsync<TEntity>(
			CancellationToken cancellation = default(CancellationToken))
			where TEntity : class
		{
			cancellation.ThrowIfCancellationRequested();
			using (await this.Lock.ReaderLockAsync(cancellation).DontMarshallContext())
			{
				cancellation.ThrowIfCancellationRequested();
				return await this.store.AllAsync<TEntity>(cancellation).DontMarshallContext();
			}
		}

		/// <inheritdoc />
		public async Task<IEnumerable<TEntity>> WhereAsync<TEntity>(
			Expression<Func<TEntity, bool>> predicate, CancellationToken cancellation = default(CancellationToken))
			where TEntity : class
		{
			IAsyncStoreContracts.WhereAsync(predicate);

			cancellation.ThrowIfCancellationRequested();
			using (await this.Lock.ReaderLockAsync(cancellation).DontMarshallContext())
			{
				cancellation.ThrowIfCancellationRequested();
				return await this.store.WhereAsync(predicate, cancellation).DontMarshallContext();
			}
		}

		/// <inheritdoc />
		public async Task AddAsync<TEntity>(
			TEntity entity, CancellationToken cancellation = default(CancellationToken))
			where TEntity : class
		{
			IAsyncStoreContracts.AddAsync(entity);

			cancellation.ThrowIfCancellationRequested();
			using (await this.Lock.WriterLockAsync(cancellation).DontMarshallContext())
			{
				cancellation.ThrowIfCancellationRequested();
				await this.store.AddAsync(entity, cancellation).DontMarshallContext();
			}
		}

		/// <inheritdoc />
		public async Task AddAllAsync<TEntity>(
			IEnumerable<TEntity> entities, CancellationToken cancellation = default(CancellationToken))
			where TEntity : class
		{
			IAsyncStoreContracts.AddAllAsync(entities);

			cancellation.ThrowIfCancellationRequested();
			using (await this.Lock.WriterLockAsync(cancellation).DontMarshallContext())
			{
				cancellation.ThrowIfCancellationRequested();
				await this.store.AddAllAsync(entities, cancellation).DontMarshallContext();
			}
		}

		/// <inheritdoc />
		public async Task AddOrIgnoreAsync<TEntity>(
			TEntity entity, CancellationToken cancellation = default(CancellationToken))
			where TEntity : class
		{
			IAsyncStoreContracts.AddOrIgnoreAsync(entity);

			cancellation.ThrowIfCancellationRequested();
			using (await this.Lock.WriterLockAsync(cancellation).DontMarshallContext())
			{
				cancellation.ThrowIfCancellationRequested();
				await this.store.AddOrIgnoreAsync(entity, cancellation).DontMarshallContext();
			}
		}

		/// <inheritdoc />
		public async Task AddOrIgnoreAllAsync<TEntity>(
			IEnumerable<TEntity> entities, CancellationToken cancellation = default(CancellationToken))
			where TEntity : class
		{
			IAsyncStoreContracts.AddOrIgnoreAllAsync(entities);

			cancellation.ThrowIfCancellationRequested();
			using (await this.Lock.WriterLockAsync(cancellation).DontMarshallContext())
			{
				cancellation.ThrowIfCancellationRequested();
				await this.store.AddOrIgnoreAllAsync(entities, cancellation).DontMarshallContext();
			}
		}

		/// <inheritdoc />
		public async Task AddOrUpdateAsync<TEntity>(
			TEntity entity, CancellationToken cancellation = default(CancellationToken))
			where TEntity : class
		{
			IAsyncStoreContracts.AddOrUpdateAsync(entity);

			cancellation.ThrowIfCancellationRequested();
			using (await this.Lock.WriterLockAsync(cancellation).DontMarshallContext())
			{
				cancellation.ThrowIfCancellationRequested();
				await this.store.AddOrUpdateAsync(entity, cancellation).DontMarshallContext();
			}
		}

		/// <inheritdoc />
		public async Task AddOrUpdateAllAsync<TEntity>(
			IEnumerable<TEntity> entities, CancellationToken cancellation = default(CancellationToken))
			where TEntity : class
		{
			IAsyncStoreContracts.AddOrUpdateAllAsync(entities);

			cancellation.ThrowIfCancellationRequested();
			using (await this.Lock.WriterLockAsync(cancellation).DontMarshallContext())
			{
				cancellation.ThrowIfCancellationRequested();
				await this.store.AddOrUpdateAllAsync(entities, cancellation).DontMarshallContext();
			}
		}

		/// <inheritdoc />
		public async Task UpdateAsync<TEntity>(
			TEntity entity, CancellationToken cancellation = default(CancellationToken))
			where TEntity : class
		{
			IAsyncStoreContracts.UpdateAsync(entity);

			cancellation.ThrowIfCancellationRequested();
			using (await this.Lock.WriterLockAsync(cancellation).DontMarshallContext())
			{
				cancellation.ThrowIfCancellationRequested();
				await this.store.UpdateAsync(entity, cancellation).DontMarshallContext();
			}
		}

		/// <inheritdoc />
		public async Task UpdateAllAsync<TEntity>(
			IEnumerable<TEntity> entities, CancellationToken cancellation = default(CancellationToken))
			where TEntity : class
		{
			IAsyncStoreContracts.UpdateAllAsync(entities);

			cancellation.ThrowIfCancellationRequested();
			using (await this.Lock.WriterLockAsync(cancellation).DontMarshallContext())
			{
				cancellation.ThrowIfCancellationRequested();
				await this.store.UpdateAllAsync(entities, cancellation).DontMarshallContext();
			}
		}

		/// <inheritdoc />
		public async Task RemoveAsync<TEntity>(
			TEntity entity, CancellationToken cancellation = default(CancellationToken))
			where TEntity : class
		{
			IAsyncStoreContracts.RemoveAsync(entity);

			cancellation.ThrowIfCancellationRequested();
			using (await this.Lock.WriterLockAsync(cancellation).DontMarshallContext())
			{
				cancellation.ThrowIfCancellationRequested();
				await this.store.RemoveAsync(entity, cancellation).DontMarshallContext();
			}
		}

		/// <inheritdoc />
		public async Task RemoveAllAsync<TEntity>(
			IEnumerable<TEntity> entities, CancellationToken cancellation = default(CancellationToken))
			where TEntity : class
		{
			IAsyncStoreContracts.RemoveAllAsync(entities);

			cancellation.ThrowIfCancellationRequested();
			using (await this.Lock.WriterLockAsync(cancellation).DontMarshallContext())
			{
				cancellation.ThrowIfCancellationRequested();
				await this.store.RemoveAllAsync(entities, cancellation).DontMarshallContext();
			}
		}

		/// <inheritdoc />
		public async Task RemoveAllAsync<TEntity>(CancellationToken cancellation = default(CancellationToken))
			where TEntity : class
		{
			cancellation.ThrowIfCancellationRequested();
			using (await this.Lock.WriterLockAsync(cancellation).DontMarshallContext())
			{
				cancellation.ThrowIfCancellationRequested();
				await this.store.RemoveAllAsync<TEntity>(cancellation).DontMarshallContext();
			}
		}
	}
}
