using FetchMe.Models.GigLocationModels;
using FetchMe.Models.PhotographerLocationModels;
using FetchMe.Models.PhotographModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.LocationModels
{
    public class Location
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

        [Column(TypeName = "decimal(9, 6)")]
        public decimal? Lat { get; set; }

        [Column(TypeName = "decimal(9, 6)")]
        public decimal? Lng { get; set; }

        public bool Serviceable { get; set; }

        public virtual List<PhotographerLocation> Photographers { get; set; }

        public virtual List<Photograph> Photographs { get; set; }

        public virtual List<GigLocation> Gigs { get; set; }
    }
}
