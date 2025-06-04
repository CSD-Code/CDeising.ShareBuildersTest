using CDeising.ShareBuildersTest.Domain.Enums;
using System;

namespace CDeising.ShareBuildersTest.Domain.Entities
{
    public class Station : BaseEntity
    {
        public string CallSign { get; set; }

        public StationType Type { get; set; }

        public Guid MarketId { get; set; }
    }
}
