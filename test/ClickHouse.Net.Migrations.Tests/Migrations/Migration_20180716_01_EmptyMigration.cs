using ClickHouse.Ado;

namespace ClickHouse.Net.Migrations.Tests.Migrations
{
    public class Migration_20180716_01_EmptyMigration : Migration
    {
        public override bool Process(ClickHouseNode node, ClickHouseConnection connection)
        {
            throw new System.NotImplementedException();
        }
    }
}