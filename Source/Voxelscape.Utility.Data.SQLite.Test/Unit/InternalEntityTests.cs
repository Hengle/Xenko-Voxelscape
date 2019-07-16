using System.Threading.Tasks;
using FluentAssertions;
using Voxelscape.Utility.Data.Core.Stores;
using Voxelscape.Utility.Data.SQLite.Stores;
using Voxelscape.Utility.Data.SQLite.Test.Configuration;
using Voxelscape.Utility.Data.SQLite.Test.Entities;
using Xunit;

namespace Voxelscape.Utility.Data.SQLite.Test.Unit
{
	/// <summary>
	///
	/// </summary>
	public static class InternalEntityTests
	{
		[Fact, Trait("TestType", "Manual")]
		public static async Task InternalVisibilityEntityShouldWork()
		{
			var config = new PersistenceConfig(SQLiteCustomization.GetDatabasePath());
			var store = new LockedSQLiteStore(config);
			var migrator = new SQLiteStoreMigrator(config, typeof(InternalTestEntity));
			var entity = new InternalTestEntity()
			{
				Key = 42,
				TestInt = 7,
				TestString = "Hello world, because of course it would be hello world",
			};

			await migrator.MigrateAsync();
			await store.AddAsync(entity);

			var entityRetrieved = await store.GetAsync<int, InternalTestEntity>(entity.Key);
			entityRetrieved.Should().BeEquivalentTo(entity);
		}
	}
}
