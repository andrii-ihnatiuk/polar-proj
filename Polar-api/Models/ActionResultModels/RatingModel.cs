using System;
using System.Collections.Generic;

namespace Polar.Models
{
    public class RatingModel 
    {
        public bool Succeeded { get; set; } 
        public List<RatingUserModel> Rating { get; set; }
       
    }
}