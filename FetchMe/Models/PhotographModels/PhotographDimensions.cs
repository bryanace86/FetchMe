using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Models.PhotographModels
{
    public class PhotographDimensions
    {
        public PhotographDimensions()
        {
            Dimensions = new List<PhotographDimension>();
        }
        public List<PhotographDimension> Dimensions { get; set; }
    }
}
