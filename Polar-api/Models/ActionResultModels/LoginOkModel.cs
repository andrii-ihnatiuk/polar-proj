using System;
using System.Collections.Generic;

namespace Polar.Models
{
    public class LoginOkModel 
    {
        public bool Succeeded { get; set; } 
        public string Token { get; set; }
       
    }
}