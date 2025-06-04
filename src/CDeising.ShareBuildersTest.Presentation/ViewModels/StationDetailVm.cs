using CDeising.ShareBuildersTest.Application.Affiliations.Queries.GetAffiliationForStation;
using CDeising.ShareBuildersTest.Application.Markets.Queries.GetMarket;
using CDeising.ShareBuildersTest.Application.Stations.Queries.GetStation;
using System;
using System.Collections.Generic;

namespace CDeising.ShareBuildersTest.Presentation.ViewModels
{
    [Serializable]
    public class StationDetailVm
    {
        public StationDto Station { get; set; }

        public MarketDto Market { get; set; }

        public List<AffiliationDto> Affiliations { get; set; } = new List<AffiliationDto>();
    }
}