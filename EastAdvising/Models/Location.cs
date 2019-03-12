using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EastAdvising.Models
{
    public class Location
    {
        public int LocationId { get; set; }
        [Required]
        public int Campus { get; set; }
        public string Building { get; set; }
        public string RoomNumber { get; set; }

        //public ICollection<Advisor> Advisors{ get; set; }
    }
}
