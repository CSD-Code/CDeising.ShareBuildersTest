using System;

namespace CDeising.ShareBuildersTest.Domain.Entities
{
    /// <summary>
    /// Abstract entity that others should inherit from.
    /// </summary>
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
    }
}
