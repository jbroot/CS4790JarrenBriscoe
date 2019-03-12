using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EastAdvising.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int StudentId { get; set; }
        public int AdvisorId { get; set; }
        public Location LocationId { get; set; }
        public ServiceBooked ServiceBookedId { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public string Reason { get; set; }
        public string Comments { get; set; }
        public bool Canceled { get; set; }

        public virtual Student Student { get; set; }
        public virtual Advisor Advisor { get; set; }
        public virtual ServiceBooked ServiceBooked { get; set; }
    }
}
