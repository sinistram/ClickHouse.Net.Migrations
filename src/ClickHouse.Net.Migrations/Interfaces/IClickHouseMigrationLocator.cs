using System.Linq;
using System.Reflection;
using JetBrains.Annotations;

namespace ClickHouse.Net.Migrations.Interfaces
{
    /// <summary>
    /// Service for locating migrations.
    /// </summary>
    public interface IClickHouseMigrationLocator
    {
        /// <summary>
        /// Locate migrations in specified assembly.
        /// </summary>
        /// <param name="assembly">Current assembly.</param>
        /// <returns>Ordered list of migrations.</returns>
        IOrderedEnumerable<Migration> Locate([NotNull]Assembly assembly);
    }
}