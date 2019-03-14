using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;


namespace EastAdvising.Models
{
    public enum Locations
    {
        Online, Phone, Davis, Main, SLCC
    }

    public class Advisor : IdentityUser
    {
        public Locations locations { get; set; }
        public String firstName { get; set; }
    }
}
