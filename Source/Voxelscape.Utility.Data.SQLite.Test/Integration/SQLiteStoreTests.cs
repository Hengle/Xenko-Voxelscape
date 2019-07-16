using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Voxelscape.Utility.Data.SQLite.Stores;
using Voxelscape.Utility.Data.SQLite.Test.Configuration;
using Voxelscape.Utility.Data.SQLite.Test.Entities;
using Xunit;

namespace Voxelscape.Utility.Data.SQLite.Test.Integration
{
	public class SQLiteStoreTests
	{
		public class AddAsyncTests
		{
			[Theory, MoqSqliteData, Trait("TestType", "Manual")]
			public async Task ShouldAddEntityToDatabase(
				LockedSQLiteStore store, TestEntity entity1, SQLiteStoreMigrator migrator)
			{
				await migrator.MigrateAsync();
				await store.AddAsync(entity1);

				var entityRetrieved = await store.GetAsync<int, TestEntity>(entity1.Key);
				entityRetrieved.Should().BeEquivalentTo(entity1);
			}
		}

		public class GetAsyncTests
		{
			[Theory, MoqSqliteData, Trait("TestType", "Manual")]
			public async Task ShouldGetSpecifiedEntityFromDatabase(
				LockedSQLiteStore store, TestEntity entity1, SQLiteStoreMigrator migrator)
			{
				await migrator.MigrateAsync();
				await store.AddAsync(entity1);

				var entityRetrieved = await store.GetAsync<int, TestEntity>(entity1.Key);
				entityRetrieved.Should().BeEquivalentTo(entity1);
			}
		}

		public class UpdateAsyncTests
		{
			[Theory, MoqSqliteData, Trait("TestType", "Manual")]
			public async Task ShouldUpdateEntityInDatabase(
				LockedSQLiteStore store, TestEntity entity1, SQLiteStoreMigrator migrator)
			{
				await migrator.MigrateAsync();
				await store.AddAsync(entity1);

				entity1.TestString = "test";

				await store.UpdateAsync(entity1);
				var retrievedEntity = await store.GetAsync<int, TestEntity>(entity1.Key);

				retrievedEntity.Should().BeEquivalentTo(entity1);
			}
		}

		public class AllAsyncTests
		{
			[Theory, MoqSqliteData, Trait("TestType", "Manual")]
			public async Task ShouldRetrieveAllEntitiesFromTable(
				LockedSQLiteStore store, TestEntity entity1, TestEntity entity2, SQLiteStoreMigrator migrator)
			{
				await migrator.MigrateAsync();
				await store.AddAsync(entity1);

				await store.AddAsync(entity2);

				var entities = await store.AllAsync<TestEntity>();

				entities.Should().HaveCount(x => x >= 2);
				entities.Should().Contain(x => x.Key == entity1.Key)
					.And.Contain(x => x.Key == entity2.Key);
			}

			[Theory, MoqSqliteData, Trait("TestType", "Manual")]
			public async Task ShouldReturnEmptyListForEmptyTable(LockedSQLiteStore store, SQLiteStoreMigrator migrator)
			{
				await migrator.MigrateAsync();

				var result = await store.AllAsync<TestEntity>();
				result.Should().BeEmpty();
			}
		}

		public class RemoveAsyncTests
		{
			[Theory, MoqSqliteData, Trait("TestType", "Manual")]
			public async Task ShouldRemoveEntityFromDatabase(
				LockedSQLiteStore store, TestEntity entity1, SQLiteStoreMigrator migrator)
			{
				await migrator.MigrateAsync();
				await store.AddAsync(entity1);

				await store.RemoveAsync(entity1);

				var entities = await store.AllAsync<TestEntity>();
				entities.Should().NotContain(x => x.Key == entity1.Key);
			}
		}

		public class WhereAsyncTests
		{
			[Theory, MoqSqliteData, Trait("TestType", "Manual")]
			public async Task ShouldFindCorrectEntityInDatabase(
				LockedSQLiteStore store, TestEntity entity1, SQLiteStoreMigrator migrator)
			{
				await migrator.MigrateAsync();
				await store.AddAsync(entity1);

				var results = await store.WhereAsync<TestEntity>(x => x.TestString == entity1.TestString);
				results.Should().Contain(x => x.TestString == entity1.TestString);
				results.FirstOrDefault().Should().BeEquivalentTo(entity1);

				results = await store.WhereAsync<TestEntity>(x => x.TestString == "failtestisfail");
				results.Should().HaveCount(0);
			}
		}

		public class PerformanceTests
		{
			[Theory, MoqSqliteData, Trait("TestType", "Manual")]
			public async Task ReadAsyncManyThreads(LockedSQLiteStore store, TestEntity entity1, SQLiteStoreMigrator migrator)
			{
				await migrator.MigrateAsync();
				var testEntities = new List<TestEntity>();
				var x = 0;

				while (x <= 1000)
				{
					testEntities.Add(entity1);
					x++;
				}

				await Task.WhenAll(testEntities.Select(i => store.AddAsync(i)));

				Assert.True(true);
			}

			[Theory, MoqSqliteData, Trait("TestType", "Manual")]
			public async Task BulkWrite(LockedSQLiteStore store, TestEntity entity1, SQLiteStoreMigrator migrator)
			{
				await migrator.MigrateAsync();
				var testEntities = new List<TestEntity>();
				var x = 0;
				while (x <= 100000)
				{
					entity1.Key = x;
					testEntities.Add(entity1);
					x++;
				}

				var timer = new Stopwatch();
				timer.Start();
				await store.AddAllAsync(testEntities);
				timer.Stop();
				Debug.WriteLine("BulkWrite time elapsed: " + timer.Elapsed.TotalSeconds);

				Assert.True(true);
			}

			[Theory, MoqSqliteData, Trait("TestType", "Manual")]
			public async Task TransactionAddMany(LockedSQLiteStore store, SQLiteStoreMigrator migrator)
			{
				await migrator.MigrateAsync();
				var numberToAdd = 100000;
				var x = 1;

				var initialCount = (await store.AllAsync<TestEntity>()).Count();

				await store.RunInTransactionAsync(transaction =>
				{
					while (x <= numberToAdd)
					{
						transaction.Add(new TestEntity
						{
							TestInt = x,
							TestString = Guid.NewGuid().ToString(),
						});
						x++;
					}
				});

				var newCount = (await store.AllAsync<TestEntity>()).Count();
				newCount.Should().Be(initialCount + numberToAdd);
			}
		}
	}
}