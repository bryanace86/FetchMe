using FetchMe.Models.GigModels;
using FetchMe.Models.MessageModels;
using FetchMe.Models.PhotographerModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.BidModels
{
    public class Bid
    {
        public Guid Id { get; set; }
        public int FeePercent { get; set; }
        public int PaymentType { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal FullAmount { get; set; }
        public DateTime FullAmountDue { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal DepositAmount { get; set; }
        public DateTime DepositAmountDue { get; set; }

        public Guid GigId { get; set; }
        public virtual Gig Gig { get; set; }

        public Guid PhotographerId { get; set; }
        public virtual Photographer Photographer { get; set; }

        public Guid MessageId { get; set; }
        public Message Message { get; set; }
    }
}
