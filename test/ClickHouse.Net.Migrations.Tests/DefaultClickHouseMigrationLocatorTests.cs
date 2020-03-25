using System.Linq;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClickHouse.Net.Migrations.Tests
{
    [TestClass]
    public class DefaultClickHouseMigrationLocatorTests
    {
        private readonly DefaultClickHouseMigrationLocator _target;

        public DefaultClickHouseMigrationLocatorTests()
        {
            _target = new DefaultClickHouseMigrationLocator();
        }

        [TestMethod]
        public void Locate_ShouldFindMigrations()
        {
            var result = _target.Locate(Assembly.GetExecutingAssembly()).ToArray();
            result.Length.Should().Be(1);
        }
    }
}