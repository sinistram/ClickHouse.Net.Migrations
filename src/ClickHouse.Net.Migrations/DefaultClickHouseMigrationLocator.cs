using System;
using System.Linq;
using System.Reflection;
using ClickHouse.Net.Migrations.Interfaces;

namespace ClickHouse.Net.Migrations
{
    /// <inheritdoc />
    public class DefaultClickHouseMigrationLocator : IClickHouseMigrationLocator
    {
        /// <inheritdoc />
        public IOrderedEnumerable<Migration> Locate(Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            var migrations = assembly
                .GetTypes()
                .Where(t => typeof(Migration).IsAssignableFrom(t) && !t.IsAbstract)
                .Select(t => (Migration)Activator.CreateInstance(t)).ToList();

            if (migrations.GroupBy(m => (m.DbEntity.CreatedAt, m.DbEntity.IdInDay)).Any(g => g.Count() > 1))
            {
                throw new Exception("There are some migrations with the same date and id.");
            }

            return migrations.OrderBy(m => m.DbEntity.CreatedAt).ThenBy(m => m.DbEntity.IdInDay);
        }
    }
}
