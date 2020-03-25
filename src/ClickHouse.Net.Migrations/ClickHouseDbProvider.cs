using System.Collections.Generic;
using System.Data;
using ClickHouse.Ado;

namespace ClickHouse.Net.Migrations
{
    internal class ClickHouseDbProvider
    {
        public static IEnumerable<MigrationModel> GetAppliedMigrations(ClickHouseConnection connection, string tableName)
        {
            var result = new List<MigrationModel>();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"SELECT Name, CreatedAt, IdInDay FROM {tableName}";
                using (var reader = command.ExecuteReader())
                {
                    if (reader.NextResult())
                    {
                        reader.ReadAll(r =>
                        {
                            result.Add(new MigrationModel(
                                name: r.GetString(0),
                                createdAt: r.GetDateTime(1),
                                idInDay: r.GetInt32(2)));
                        });
                    }
                }
            }

            return result;
        }

        public static void CreateMigrationTableIfNotExists(ClickHouseConnection connection, string tableName)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"CREATE TABLE IF NOT EXISTS {tableName} " +
                                      "(Name String, CreatedAt Date, IdInDay Int32, AppliedAt DateTime DEFAULT now()) " +
                                      "ENGINE = TinyLog";
                command.ExecuteNonQuery();
            }
        }

        public static bool MakeMigrationRecord(ClickHouseConnection connection, MigrationModel migration, string tableName)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"INSERT INTO {tableName} " +
                                      "(Name, CreatedAt, IdInDay) " +
                                      "VALUES(@name, @createdAt, @idInDay)";
                command.AddParameter("name", DbType.String, migration.Name);
                command.AddParameter("createdAt", DbType.Date, migration.CreatedAt);
                command.AddParameter("idInDay", DbType.Int32, migration.IdInDay);
                command.ExecuteNonQuery();
            }

            return true;
        }
    }
}