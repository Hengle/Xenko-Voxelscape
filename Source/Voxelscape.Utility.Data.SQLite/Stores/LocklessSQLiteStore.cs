using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using SQLite.Net;
using SQLite.Net.Async;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Disposables;
using Voxelscape.Utility.Data.Pact.Stores;
using Voxelscape.Utility.Data.SQLite.Utilities;

namespace Voxelscape.Utility.Data.SQLite.Stores
{
	/// <summary>
	/// An implementation of <see cref="ITransactionalStore"/> backed by an SQLite database.
	/// </summary>
	public class LocklessSQLiteStore : ITransactionalStore
	{
		/// <summary>
		/// The connection to the SQLite database.
		/// </summary>
		private readonly SQLiteAsyncConnection connection;

		/// <summary>
		/// Initializes a new instance of the <see cref="LocklessSQLiteStore"/> class.
		/// </summary>
		/// <param name="persistenceConfig">The persistence configuration.</param>
		public LocklessSQLiteStore(IPersistenceConfig persistenceConfig)
		{
			Contracts.Requires.That(persistenceConfig != null);

			this.connection = new SQLiteAsyncConnection(() => new SQLiteConnectionWithLock(
				SQLitePlatform.New(), new SQLiteConnectionString(persistenceConfig.DatabasePath, false)));
		}

		#region IAsyncStore Members

		/// <inheritdoc />
		public async Task<IEnumerable<TEntity>> AllAsync<TEntity>(
			CancellationToken cancellation = default(CancellationToken))
			where TEntity : class
		{
			cancellation.ThrowIfCancellationRequested();
			return await this.connection.Table<TEntity>().ToListAsync(cancellation).DontMarshallContext();
		}

		/// <inheritdoc />
		public async Task<IEnumerable<TEntity>> WhereAsync<TEntity>(
			Expression<Func<TEntity, bool>> predicate, CancellationToken cancellation = default(CancellationToken))
			where TEntity : class
		{
			IAsyncStoreContracts.WhereAsync(predicate);

			cancellation.ThrowIfCancellationRequested();
			return await this.connection.Table<TEntity>()
				.Where(predicate)
				.ToListAsync(cancellation)
				.DontMarshallContext();
		}

		/// <inheritdoc />
		public Task AddAsync<TEntity>(
			TEntity entity, CancellationToken cancellation = default(CancellationToken))
			where TEntity : class
		{
			IAsyncStoreContracts.AddAsync(entity);

			cancellation.ThrowIfCancellationRequested();
			return this.connection.InsertAsync(entity, cancellation);
		}

		/// <inheritdoc />
		public Task AddAllAsync<TEntity>(
			IEnumerable<TEntity> entities, CancellationToken cancellation = default(CancellationToken))
			where TEntity : class
		{
			IAsyncStoreContracts.AddAllAsync(entities);

			cancellation.ThrowIfCancellationRequested();
			return this.connection.InsertAllAsync(entities, cancellation);
		}

		/// <inheritdoc />
		public Task AddOrIgnoreAsync<TEntity>(
			TEntity entity, CancellationToken cancellation = default(CancellationToken))
			where TEntity : class
		{
			IAsyncStoreContracts.AddOrIgnoreAsync(entity);

			// for some reason there is no overload that accepts a CancellationToken
			cancellation.ThrowIfCancellationRequested();
			return this.connection.InsertOrIgnoreAsync(entity);
		}

		/// <inheritdoc />
		public Task AddOrIgnoreAllAsync<TEntity>(
			IEnumerable<TEntity> entities, CancellationToken cancellation = default(CancellationToken))
			where TEntity : class
		{
			IAsyncStoreContracts.AddOrIgnoreAllAsync(entities);

			cancellation.ThrowIfCancellationRequested();
			return this.connection.InsertOrIgnoreAllAsync(entities, cancellation);
		}

		/// <inheritdoc />
		public Task AddOrUpdateAsync<TEntity>(
			TEntity entity, CancellationToken cancellation = default(CancellationToken))
			where TEntity : class
		{
			IAsyncStoreContracts.AddOrUpdateAsync(entity);

			cancellation.ThrowIfCancellationRequested();
			return this.connection.InsertOrReplaceAsync(entity, cancellation);
		}

		/// <inheritdoc />
		public Task AddOrUpdateAllAsync<TEntity>(
			IEnumerable<TEntity> entities, CancellationToken cancellation = default(CancellationToken))
			where TEntity : class
		{
			IAsyncStoreContracts.AddOrUpdateAllAsync(entities);

			cancellation.ThrowIfCancellationRequested();
			return this.connection.InsertOrReplaceAllAsync(entities, cancellation);
		}

		/// <inheritdoc />
		public Task UpdateAsync<TEntity>(
			TEntity entity, CancellationToken cancellation = default(CancellationToken))
			where TEntity : class
		{
			IAsyncStoreContracts.UpdateAsync(entity);

			cancellation.ThrowIfCancellationRequested();
			return this.connection.UpdateAsync(entity, cancellation);
		}

		/// <inheritdoc />
		public Task UpdateAllAsync<TEntity>(
			IEnumerable<TEntity> entities, CancellationToken cancellation = default(CancellationToken))
			where TEntity : class
		{
			IAsyncStoreContracts.UpdateAllAsync(entities);

			cancellation.ThrowIfCancellationRequested();
			return this.connection.UpdateAllAsync(entities, cancellation);
		}

		/// <inheritdoc />
		public Task RemoveAsync<TEntity>(
			TEntity entity, CancellationToken cancellation = default(CancellationToken))
			where TEntity : class
		{
			IAsyncStoreContracts.RemoveAsync(entity);

			cancellation.ThrowIfCancellationRequested();
			return this.connection.DeleteAsync(entity, cancellation);
		}

		/// <inheritdoc />
		public Task RemoveAllAsync<TEntity>(
			IEnumerable<TEntity> entities, CancellationToken cancellation = default(CancellationToken))
			where TEntity : class
		{
			IAsyncStoreContracts.RemoveAllAsync(entities);

			cancellation.ThrowIfCancellationRequested();
			return this.RunInTransactionAsync(transaction => transaction.RemoveAll(entities), cancellation);
		}

		/// <inheritdoc />
		public Task RemoveAllAsync<TEntity>(CancellationToken cancellation = default(CancellationToken))
			where TEntity : class
		{
			cancellation.ThrowIfCancellationRequested();
			return this.connection.DeleteAllAsync<TEntity>(cancellation);
		}

		#endregion

		#region ITransactionalStore Members

		/// <inheritdoc />
		public Task RunInTransactionAsync(
			Action<ITransaction> action, CancellationToken cancellation = default(CancellationToken))
		{
			ITransactionalStoreContracts.RunInTransactionAsync(action);

			cancellation.ThrowIfCancellationRequested();
			return this.connection.RunInTransactionAsync(
				(SQLiteConnection conn) =>
				{
					using (var transaction = new Transaction(conn))
					{
						action(transaction);
					}
				},
				cancellation);
		}

		#endregion

		#region Private Classes

		private class Transaction : AbstractDisposable, ITransaction
		{
			/// <summary>
			/// The connection to the SQLite database.
			/// </summary>
			private readonly SQLiteConnection connection;

			public Transaction(SQLiteConnection connection)
			{
				Contracts.Requires.That(connection != null);

				// SQLiteAsyncConnection.RunInTransactionAsync(Action<SQLiteConnection> action)
				// handles managing the connection passed in so DO NOT start a transaction here
				this.connection = connection;
			}

			/// <inheritdoc />
			public IEnumerable<TEntity> All<TEntity>()
				where TEntity : class
			{
				ITransactionContracts.All<TEntity>(this);

				return this.connection.Table<TEntity>();
			}

			/// <inheritdoc />
			public IEnumerable<TEntity> Where<TEntity>(Expression<Func<TEntity, bool>> predicate)
				where TEntity : class
			{
				ITransactionContracts.Where(this, predicate);

				return this.connection.Table<TEntity>().Where(predicate);
			}

			/// <inheritdoc />
			public void Add<TEntity>(TEntity entity)
				where TEntity : class
			{
				ITransactionContracts.Add(this, entity);

				this.connection.Insert(entity);
			}

			/// <inheritdoc />
			public void AddAll<TEntity>(IEnumerable<TEntity> entities)
				where TEntity : class
			{
				ITransactionContracts.AddAll(this, entities);

				this.connection.InsertAll(entities);
			}

			/// <inheritdoc />
			public void AddOrIgnore<TEntity>(TEntity entity)
				where TEntity : class
			{
				ITransactionContracts.AddOrIgnore(this, entity);

				this.connection.InsertOrIgnore(entity);
			}

			/// <inheritdoc />
			public void AddOrIgnoreAll<TEntity>(IEnumerable<TEntity> entities)
				where TEntity : class
			{
				ITransactionContracts.AddOrIgnoreAll(this, entities);

				this.connection.InsertOrIgnoreAll(entities);
			}

			/// <inheritdoc />
			public void AddOrUpdate<TEntity>(TEntity entity)
				where TEntity : class
			{
				ITransactionContracts.AddOrUpdate(this, entity);

				this.connection.InsertOrReplace(entity);
			}

			/// <inheritdoc />
			public void AddOrUpdateAll<TEntity>(IEnumerable<TEntity> entities)
				where TEntity : class
			{
				ITransactionContracts.AddOrUpdateAll(this, entities);

				this.connection.InsertOrReplaceAll(entities);
			}

			/// <inheritdoc />
			public void Update<TEntity>(TEntity entity)
				where TEntity : class
			{
				ITransactionContracts.Update(this, entity);

				this.connection.Update(entity);
			}

			/// <inheritdoc />
			public void UpdateAll<TEntity>(IEnumerable<TEntity> entities)
				where TEntity : class
			{
				ITransactionContracts.UpdateAll(this, entities);

				this.connection.UpdateAll(entities);
			}

			/// <inheritdoc />
			public void Remove<TEntity>(TEntity entity)
				where TEntity : class
			{
				ITransactionContracts.Remove(this, entity);

				this.connection.Delete(entity);
			}

			/// <inheritdoc />
			public void RemoveAll<TEntity>(IEnumerable<TEntity> entities)
				where TEntity : class
			{
				ITransactionContracts.RemoveAll(this, entities);

				foreach (var entity in entities)
				{
					this.connection.Delete(entity);
				}
			}

			/// <inheritdoc />
			public void RemoveAll<TEntity>()
				where TEntity : class
			{
				ITransactionContracts.RemoveAll<TEntity>(this);

				this.connection.DeleteAll<TEntity>();
			}

			/// <inheritdoc />
			protected override void ManagedDisposal()
			{
				// SQLiteAsyncConnection.RunInTransactionAsync(Action<SQLiteConnection> action)
				// handles managing the connection passed in so DO NOT commit or dispose it here
			}
		}

		#endregion
	}
}
