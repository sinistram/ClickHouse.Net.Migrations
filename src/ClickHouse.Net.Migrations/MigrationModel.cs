﻿using System;
using JetBrains.Annotations;

namespace ClickHouse.Net.Migrations
{
    /// <inheritdoc />
    /// <summary>
    /// Model class for migration table in database.
    /// </summary>
    public class MigrationModel : IEquatable<MigrationModel>
    {
        /// <summary>
        /// Creates new instance of MigrationModel.
        /// </summary>
        public MigrationModel([NotNull] string name, DateTime createdAt, int idInDay)
        {
            Name = name;
            IdInDay = idInDay;
            CreatedAt = createdAt;
        }

        /// <summary>
        /// Name of the migration.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Migration creation date in yyyyMMdd format (without time).
        /// </summary>
        public DateTime CreatedAt { get; }

        /// <summary>
        /// Id of the migration for the current day.
        /// </summary>
        public int IdInDay { get;  }

        /// <inheritdoc />
        public override string ToString() => $"Name: {Name}, CreatedAt: {CreatedAt.ToShortDateString()}, Id: {IdInDay}";

        /// <inheritdoc />
        public bool Equals(MigrationModel other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return string.Equals(Name, other.Name) && IdInDay == other.IdInDay && CreatedAt.Equals(other.CreatedAt);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((MigrationModel)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ IdInDay;
                hashCode = (hashCode * 397) ^ CreatedAt.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// Autogenerated equality operator.
        /// </summary>
        public static bool operator ==(MigrationModel left, MigrationModel right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Autogenerated not equal operator.
        /// </summary>
        public static bool operator !=(MigrationModel left, MigrationModel right)
        {
            return !Equals(left, right);
        }
    }
}