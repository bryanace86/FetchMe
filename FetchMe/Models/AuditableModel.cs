using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models
{
    public class AuditableModel
    {
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
    }
}
