using System;

namespace CDeising.ShareBuildersTest.Application.Markets.Queries.GetMarket
{
    [Serializable]
    public class MarketDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
