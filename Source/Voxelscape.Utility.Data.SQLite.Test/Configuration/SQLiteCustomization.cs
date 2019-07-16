using System;
using System.IO;
using System.Reflection;
using AutoFixture;
using Voxelscape.Utility.Data.Core.Stores;
using Voxelscape.Utility.Data.Pact.Stores;
using Voxelscape.Utility.Data.SQLite.Stores;
using Voxelscape.Utility.Data.SQLite.Test.Entities;

namespace Voxelscape.Utility.Data.SQLite.Test.Configuration
{
	public class SQLiteCustomization : ICustomization
	{
		public static string GetDatabasePath() => Path.Combine(
			Path.GetDirectoryName(Uri.UnescapeDataString(
				new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path)),
			$"{Guid.NewGuid()}_Test.db");

		public void Customize(IFixture fixture)
		{
			var perConfService = new PersistenceConfig(GetDatabasePath());
			fixture.Register<IPersistenceConfig>(() => perConfService);
			fixture.Register(() => new SQLiteStoreMigrator(perConfService, typeof(TestEntity)));
		}
	}
}
