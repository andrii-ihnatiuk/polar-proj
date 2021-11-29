using System;
using System.Collections.Generic;

namespace Polar.Models
{
    public class LoginBadModel 
    {
        public bool Succeeded { get; set; } 
        public List<object> Errors { get; set; }
       
    }
}