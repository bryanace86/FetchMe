using FetchMe.Models.BidModels;
using FetchMe.Models.GigDateTimeModels;
using FetchMe.Models.GigDateTimeTypeModels;
using FetchMe.Models.GigLocationModels;
using FetchMe.Models.GigLocationTypeModels;
using FetchMe.Models.GigTagModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.GigModels
{
    public class Gig
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public Guid LocationTypeId { get; set; }
        public virtual GigLocationType LocationType { get; set; }
        public virtual List<GigLocation> Locations { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal MinBudget { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal MaxBudget { get; set; }
        
        public Guid GigDateTimeTypeId { get; set; }
        public GigDateTimeType GigDateTimeType { get; set; }
        public virtual List<GigDateTime> GigDateTimes { get; set; }
        public virtual List<GigTagGig> Tags { get; set; }
        public virtual List<Bid> Bids { get; set; }
        public Guid GigStatusId { get; set; }
        public virtual GigStatus GigStatus { get; set; }

    }
}
