using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EastAdvising.Models
{
    public class Advisor
    {
        public int AdvisorId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return LastName + ", " + FirstName; }
        }
        
        public bool IsAdmin { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        public virtual ICollection<Availability> Availabilities { get; set; }
        //public virtual ICollection<Location> Locations { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }

    }
}
