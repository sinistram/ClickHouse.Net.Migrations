using System;
using JetBrains.Annotations;

namespace ClickHouse.Net.Migrations
{
    /// <summary>
    /// Class for migrations settings.
    /// </summary>
    public class ClickHouseMigrationSettings
    {
        /// <summary>
        /// ClickHouse nodes. Usually there is one node. Useful if you are using replication.
        /// </summary>
        public ClickHouseNode[] Nodes { get; }

        /// <summary>
        /// Table name that should be used for keeping migration system data.
        /// </summary>
        public string MigrationsTableName { get; }

        /// <summary>
        /// Creates an object of ClickHouseMigrationSettings.
        /// </summary>
        public ClickHouseMigrationSettings([NotNull] string migrationsTableName, params ClickHouseNode[] nodes)
        {
            if (nodes.Length < 1)
            {
                throw new ArgumentException("At least one node should be specified.", nameof(nodes));
            }

            MigrationsTableName = migrationsTableName;
            Nodes = nodes;
        }
    }
}