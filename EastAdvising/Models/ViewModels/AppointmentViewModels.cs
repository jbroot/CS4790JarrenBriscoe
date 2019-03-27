using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EastAdvising.Models.ViewModels
{
    public class AppointmentViewModels
    {
        public Appointment Appointment { get; set; }
        public Student Student { get; set; }
        public IEnumerable<Advisor> Advisor { get; set; }
        public IEnumerable<Location> Location { get; set; }
        public IEnumerable<Service> Service { get; set; }
        public Availability Availability { get; set; }
    }
}
