using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SQLite.Net;
using SQLite.Net.Async;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Stores;
using Voxelscape.Utility.Data.SQLite.Utilities;

namespace Voxelscape.Utility.Data.SQLite.Stores
{
	public class SQLiteStoreMigrator : IAsyncStoreMigrator
	{
		private readonly string databasePath;

		private readonly Type[] entityTypes;

		public SQLiteStoreMigrator(IPersistenceConfig persistenceConfig, IEnumerable<Type> entityTypes)
			: this(persistenceConfig, entityTypes?.ToArray())
		{
		}

		public SQLiteStoreMigrator(IPersistenceConfig persistenceConfig, params Type[] entityTypes)
		{
			Contracts.Requires.That(persistenceConfig != null);
			Contracts.Requires.That(entityTypes.AllAndSelfNotNull());

			this.databasePath = persistenceConfig.DatabasePath;
			this.entityTypes = entityTypes;
		}

		/// <inheritdoc />
		public async Task MigrateAsync(CancellationToken cancellation = default(CancellationToken))
		{
			cancellation.ThrowIfCancellationRequested();
			var connection = new SQLiteAsyncConnection(() => new SQLiteConnectionWithLock(
				SQLitePlatform.New(), new SQLiteConnectionString(this.databasePath, false)));

			cancellation.ThrowIfCancellationRequested();
			await connection.CreateTablesAsync(cancellation, this.entityTypes).DontMarshallContext();
		}
	}
}
