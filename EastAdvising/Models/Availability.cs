using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EastAdvising.Models
{
    public class Availability
    {
        public int AvailabilityId { get; set; }
        public int AdvisorId { get; set; }
        public int LocationId { get; set; }

        [Column("StartTime", TypeName = "SmallDateTime")]
        public DateTime StartTime { get; set; }

        [Column("EndTime", TypeName = "SmallDateTime")]
        public DateTime EndTime { get; set; }

        //Navigation Properties
        public virtual ICollection<Service> Services { get; set; }

        [ForeignKey("LocationId")]
        public virtual Location Location { get; set; }

        [ForeignKey("AdvisorId")]
        public virtual Advisor Advisor { get; set; }
    }
}
