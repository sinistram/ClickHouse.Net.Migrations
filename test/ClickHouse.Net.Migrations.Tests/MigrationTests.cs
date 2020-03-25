using System;
using ClickHouse.Net.Migrations.Tests.Migrations;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClickHouse.Net.Migrations.Tests
{
    [TestClass]
    public class MigrationTests
    {
        private readonly Migration _testMigration;

        public MigrationTests()
        {
            _testMigration = new Migration_20180716_01_EmptyMigration();
        }

        [TestMethod]
        public void Name_IsCorrect()
        {
            _testMigration.DbEntity.Name.Should().Be("EmptyMigration");
        }

        [TestMethod]
        public void NumberInDay_IsCorrect()
        {
            _testMigration.DbEntity.IdInDay.Should().Be(1);
        }

        [TestMethod]
        public void CreatedAt_IsCorrect()
        {
            _testMigration.DbEntity.CreatedAt.Should().Be(new DateTime(2018, 07, 16));
        }
    }
}
