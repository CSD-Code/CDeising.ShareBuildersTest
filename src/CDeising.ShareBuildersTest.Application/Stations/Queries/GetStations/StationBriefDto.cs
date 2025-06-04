using System;

namespace CDeising.ShareBuildersTest.Application.Stations.Queries.GetStations
{
    [Serializable]
    public class StationBriefDto
    {
        public Guid Id { get; set; }

        public string CallSign { get; set; }
    }
}
