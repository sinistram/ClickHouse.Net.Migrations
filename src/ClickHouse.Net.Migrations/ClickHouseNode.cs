using ClickHouse.Ado;
using JetBrains.Annotations;

namespace ClickHouse.Net.Migrations
{
    /// <summary>
    /// Class for describing clickHouse database node.
    /// </summary>
    public class ClickHouseNode
    {
        /// <summary>
        /// Creates new instance of ClickHouseNode without replica.
        /// </summary>
        public ClickHouseNode([NotNull] ClickHouseConnectionSettings connectionSettings, [NotNull] string name)
        {
            ConnectionSettings = connectionSettings;
            Name = name;
            HasReplica = false;
        }

        /// <summary>
        /// Creates new instance of ClickHouseNode with replica.
        /// </summary>
        public ClickHouseNode([NotNull] ClickHouseConnectionSettings connectionSettings, [NotNull] string name, [NotNull] string tablesPathRoot, [NotNull] string replicaName)
        {
            ConnectionSettings = connectionSettings;
            Name = name;
            TablesPathRoot = tablesPathRoot;
            ReplicaName = replicaName;
            HasReplica = true;
        }

        /// <summary>
        /// Settings for the database node.
        /// </summary>
        public ClickHouseConnectionSettings ConnectionSettings { get; }

        /// <summary>
        /// Name of the node (for logging).
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Determines if node has a replica in ZooKeeper.
        /// </summary>
        public bool HasReplica { get; }

        /// <summary>
        /// Path to tables root in the ZooKeeper.
        /// </summary>
        public string TablesPathRoot { get; }

        /// <summary>
        /// The name of replica in the ZooKeeper.
        /// </summary>
        public string ReplicaName { get; }
    }
}