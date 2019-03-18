using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EastAdvising.Models
{
    public class Availability
    {
        public int AvailabilityId { get; set; }
        public int AdvisorId { get; set; }
        public DateTime StartTime { get; set; }
        public virtual Advisor Advisor { get; set; }
    }
}
