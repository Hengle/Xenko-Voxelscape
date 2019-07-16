using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace Voxelscape.Utility.Data.SQLite.Test.Configuration
{
	public class MoqSqliteData : AutoDataAttribute
	{
		public MoqSqliteData()
			: base(() => new Fixture()
			.Customize(new AutoMoqCustomization())
			.Customize(new SQLiteCustomization()))
		{
		}
	}
}
