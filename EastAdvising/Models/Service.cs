using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EastAdvising.Models
{
    public class Service
    {
        public int ServiceId { get; set; }
        public int AvailabilityId { get; set; }
        public string ServiceType { get; set; }
        public bool IsPhone { get; set; }

        //Navigation Properties
        public virtual Appointment Appointment { get; set; }

        [ForeignKey("AvailabilityId")]
        public virtual Availability Availability { get; set; }
    }
}
