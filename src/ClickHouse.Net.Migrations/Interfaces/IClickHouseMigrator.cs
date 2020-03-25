using System.Reflection;
using JetBrains.Annotations;

namespace ClickHouse.Net.Migrations.Interfaces
{
    /// <summary>
    /// Service for managing ClickHouse migrations.
    /// </summary>
    public interface IClickHouseMigrator
    {
        /// <summary>
        /// Apply all migrations not yet applied.
        /// </summary>
        bool ApplyMigrations();

        /// <summary>
        /// Apply all migrations not yed applied and located in specific assembly.
        /// </summary>
        /// <param name="assembly">Assembly to search migrations for.</param>
        bool ApplyMigrations([NotNull] Assembly assembly);
    }
}