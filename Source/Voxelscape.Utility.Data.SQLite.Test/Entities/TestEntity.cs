using SQLite.Net.Attributes;
using Voxelscape.Utility.Common.Pact.Types;

namespace Voxelscape.Utility.Data.SQLite.Test.Entities
{
	[Table(nameof(TestEntity))]
	public class TestEntity : IKeyed<int>
	{
		[PrimaryKey, AutoIncrement]
		public int Key { get; set; }

		public int TestInt { get; set; }

		public string TestString { get; set; }
	}
}
