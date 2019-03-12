using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EastAdvising.Models
{
    public class ServiceBooked
    {
        public int ServiceBookedId { get; set; }
        public int AppointmentId { get; set; }
        public int ServiceId { get; set; }

        public virtual Service Service { get; set; }
        public virtual Appointment Appointment { get; set; }
    }
}
