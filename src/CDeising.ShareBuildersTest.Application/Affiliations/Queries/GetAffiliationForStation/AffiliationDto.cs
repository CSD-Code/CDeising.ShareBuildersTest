using System;

namespace CDeising.ShareBuildersTest.Application.Affiliations.Queries.GetAffiliationForStation
{
    [Serializable]
    public class AffiliationDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
