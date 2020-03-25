using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ClickHouse.Ado;
using ClickHouse.Net.Migrations.Interfaces;
using Microsoft.Extensions.Logging;

namespace ClickHouse.Net.Migrations
{
    /// <inheritdoc />
    public class ClickHouseMigrator : IClickHouseMigrator
    {
        private readonly ILogger<ClickHouseMigrator> _logger;
        private readonly ClickHouseMigrationSettings _migrationSettings;
        private readonly IClickHouseMigrationLocator _locator;

        /// <summary>
        /// Constructor for ClickHouseMigrations class.
        /// </summary>
        /// <param name="logger">An instance of ILogger.</param>
        /// <param name="migrationSettings">Migration settings for the database.</param>
        /// <param name="locator">IClickHouseMigrationLocator for handling located migrations.</param>
        public ClickHouseMigrator(
            ILogger<ClickHouseMigrator> logger,
            ClickHouseMigrationSettings migrationSettings,
            IClickHouseMigrationLocator locator)
        {
            _logger = logger;
            _migrationSettings = migrationSettings;
            _locator = locator;
        }

        /// <inheritdoc />
        public bool ApplyMigrations()
        {
            var assembly = Assembly.GetEntryAssembly();
            if (assembly is null)
            {
                _logger.LogError("Cannot find current assembly");
                return false;
            }

            return ApplyMigrations(assembly);
        }

        /// <inheritdoc />
        public bool ApplyMigrations(Assembly assembly)
        {
            foreach (var node in _migrationSettings.Nodes)
            {
                var success = TryApplyMigrations(node, assembly);
                if (!success)
                {
                    _logger.LogError($"Migration process stopped because migration failed for the node {node.Name}");
                    return false;
                }
            }

            _logger.LogInformation("All migrations were successfully applied.");
            return true;
        }

        private bool TryApplyMigrations(ClickHouseNode node, Assembly assembly)
        {
            try
            {
                using (var connection = new ClickHouseConnection(node.ConnectionSettings))
                {
                    _logger.LogInformation($"Applying migrations for the node {node.Name}");
                    connection.Open();
                    ClickHouseDbProvider.CreateMigrationTableIfNotExists(connection,
                        _migrationSettings.MigrationsTableName);

                    var migrations = _locator.Locate(assembly);
                    var appliedMigrations =
                        ClickHouseDbProvider.GetAppliedMigrations(connection, _migrationSettings.MigrationsTableName);

                    var notAppliedMigrations = GetNotAppliedMigrations(migrations.ToList(), appliedMigrations).ToList();

                    _logger.LogInformation(
                        $"Not applied migrations:{Environment.NewLine}{string.Join(Environment.NewLine, notAppliedMigrations.Select(m => m.ToString()))}");

                    var success = TryApplyMigrations(notAppliedMigrations, node, connection);
                    if (success)
                    {
                        _logger.LogInformation($"All migrations for the node {node.Name} successfully applied");
                    }
                    else
                    {
                        _logger.LogError($"Migrations for {node.Name} were not applied");
                    }

                    return success;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Migration applying error.");
                return false;
            }
        }

        private bool TryApplyMigrations(IEnumerable<Migration> notAppliedMigrations, ClickHouseNode node, ClickHouseConnection connection)
        {
            foreach (var migration in notAppliedMigrations)
            {
                try
                {
                    if (!ApplyMigration(migration, node, connection))
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Exception was thrown while trying to apply {migration.DbEntity}");
                    return false;
                }
            }

            return true;
        }

        private bool ApplyMigration(Migration migration, ClickHouseNode node, ClickHouseConnection connection)
        {
            _logger.LogInformation($"Applying {migration}...");

            var success = migration.Process(node, connection) && ClickHouseDbProvider.MakeMigrationRecord(connection, migration.DbEntity, _migrationSettings.MigrationsTableName);
            if (success)
            {
                _logger.LogInformation($"{migration} applied");
            }
            else
            {
                _logger.LogError($"Can't apply {migration}");
            }

            return success;
        }

        private static IEnumerable<Migration> GetNotAppliedMigrations(IEnumerable<Migration> allMigration, IEnumerable<MigrationModel> appliedMigration)
        {
            return allMigration.Where(m => !appliedMigration.Contains(m.DbEntity));
        }
    }
}