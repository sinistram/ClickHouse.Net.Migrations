using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClickHouse.Net.Migrations.Tests
{
    [TestClass]
    public class MigrationModelTests
    {
        [TestMethod]
        public void Equals_ShouldReturnTrue_AllFieldsAreEqual()
        {
            var date = DateTime.Now;
            var model1 = new MigrationModel("name1", date, 2);
            var model2 = new MigrationModel("name1", date, 2);
            model1.Should().BeEquivalentTo(model2);
        }

        [TestMethod]
        public void Equals_ShouldReturnTrue_ReferenceTheSame()
        {
            var date = DateTime.Now;
            var model1 = new MigrationModel("name1", date, 2);
            var model2 = model1;
            model1.Should().BeEquivalentTo(model2);
        }

        [TestMethod]
        public void Equals_ShouldReturnFalse_DifferentNames()
        {
            var date = DateTime.Now;
            var model1 = new MigrationModel("name1", date, 2);
            var model2 = new MigrationModel("name2", date, 2);
            model1.Should().NotBeEquivalentTo(model2);
        }

        [TestMethod]
        public void Equals_ShouldReturnFalse_DifferentDates()
        {
            var date = DateTime.Now;
            var model1 = new MigrationModel("name1", date, 2);
            var model2 = new MigrationModel("name1", date + TimeSpan.FromHours(1), 2);
            model1.Should().NotBeEquivalentTo(model2);
        }

        [TestMethod]
        public void Equals_ShouldReturnFalse_DifferentIds()
        {
            var date = DateTime.Now;
            var model1 = new MigrationModel("name1", date, 2);
            var model2 = new MigrationModel("name1", date, 3);
            model1.Should().NotBeEquivalentTo(model2);
        }
    }
}
