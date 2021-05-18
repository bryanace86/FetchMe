using FetchMe.Models.BidModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.MessageModels
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Body { get; set; }

        public virtual Bid Bid { get; set; }
    }
}
