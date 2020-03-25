using System;
using System.Globalization;
using ClickHouse.Ado;
using JetBrains.Annotations;

namespace ClickHouse.Net.Migrations
{
    /// <summary>
    /// Base class for all user migrations.
    /// </summary>
    public abstract class Migration
    {
        /// <summary>
        /// Processing method for applying the migration.
        /// </summary>
        /// <param name="node">Node over which migration will be applied.</param>
        /// <param name="connection">OPENED connection to DB.</param>
        /// <returns>Was processing successful or not.</returns>
        public abstract bool Process([NotNull] ClickHouseNode node, [NotNull] ClickHouseConnection connection);

        /// <summary>
        /// Initializes DbEntities for a migration. Class name should be in the format: "Migration_{yyyyMMdd}_{IdInDay}_{Name}".
        /// </summary>
        /// <exception cref="Exception">When class name is in incorrect format.</exception>
        protected Migration()
        {
            try
            {
                var fields = GetType().Name.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
                DbEntity = new MigrationModel(
                    name: fields[3],
                    createdAt: DateTime.ParseExact(fields[1], "yyyyMMdd", CultureInfo.InvariantCulture),
                    idInDay: int.Parse(fields[2]));
            }
            catch
            {
                throw new Exception($"Class name {GetType().Name} is in incorrect format. Correct format is: \"Migration_{{yyyyMMdd}}_{{IdInDay}}_{{Name}}\"");
            }
        }

        /// <summary>
        /// Migration entity for the database.
        /// </summary>
        public MigrationModel DbEntity { get; }

        /// <inheritdoc />
        public override string ToString() => DbEntity.ToString();
    }
}