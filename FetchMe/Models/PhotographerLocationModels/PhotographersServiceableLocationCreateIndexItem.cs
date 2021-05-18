using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.PhotographerLocationModels
{
    public class PhotographersServiceableLocationCreateIndexItem
    {
        public string Id { get; set; }
        public string FormattedAddress { get; set; }
        public string AdministrativeAreaLevel1 { get; set; }
        public string AdministrativeAreaLevel2 { get; set; }
        public string AdministrativeAreaLevel3 { get; set; }
        public string Country { get; set; }
        public string Locality { get; set; }
        public string Neighborhood { get; set; }
        public string Political { get; set; }
        public string PostalCode { get; set; }
        public string Route { get; set; }
        public string StreetNumber { get; set; }
        public decimal? Lat { get; set; }
        public decimal? Lng { get; set; }
        public bool Serviceable { get; set; }

        public int Distance { get; set; }
        public bool PhotographerHas { get; set; }
        public string Direction { get; set; }
    }
}
