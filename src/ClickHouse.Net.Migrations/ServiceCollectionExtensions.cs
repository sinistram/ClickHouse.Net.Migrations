using ClickHouse.Net.Migrations.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ClickHouse.Net.Migrations
{
    /// <summary>
    /// Extension methods for connecting migrations class in .net core app services.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Extension method for connecting migrations in .net core app services.
        /// </summary>
        /// <param name="services">Services of .net core app.</param>
        /// <returns>The same services collection.</returns>
        public static IServiceCollection AddClickHouseMigrations(this IServiceCollection services)
        {
            services.AddTransient<IClickHouseMigrator, ClickHouseMigrator>();
            services.AddTransient<IClickHouseMigrationLocator, DefaultClickHouseMigrationLocator>();
            return services;
        }
    }
}
