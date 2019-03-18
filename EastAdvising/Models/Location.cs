using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EastAdvising.Models
{
    public class Location
    {
        public int LocationId { get; set; }

        public string Building { get; set; }
        public string RoomNumber { get; set; }

        //[RegularExpression("\bD\b|\bM\b|\bS\b)")] //Campus = D (Davis) | M (Main) | S(SLCC)
        //TODO: Maybe break this out into it's own table (?)
        public string Campus { get; set; }

        //Navigation Properties
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Availability> Availabilities { get; set; }
    }
}
