using CDeising.ShareBuildersTest.Domain.Enums;
using System;

namespace CDeising.ShareBuildersTest.Application.Stations.Queries.GetStation
{
    [Serializable]
    public class StationDto
    {
        public Guid Id { get; set; }

        public string CallSign { get; set; }

        public StationType Type { get; set; }

        public Guid MarketId { get; set; }
    }
}
