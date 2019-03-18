using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EastAdvising.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int StudentId { get; set; }
        public int AdvisorId { get; set; }
        public int LocationId { get; set; }
        public int? ServiceId { get; set; }

        [StringLength(100)]
        public string Comments { get; set; }

        [Column("AppointmentDateTime", TypeName = "SmallDateTime")]
        public DateTime AppointmentDateTime { get; set; }

        [RegularExpression("\bA\b|\bC\b")] //Status = A (Active) || C (Canceled)
        public char Status { get; set; }


        //Navigation Properties
        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }

        [ForeignKey("AdvisorId")]
        public virtual Advisor Advisor { get; set; }

        [ForeignKey("ServiceId")]
        public virtual Service Service { get; set; }

        [ForeignKey("LocationId")]
        public virtual Location Location { get; set; }
    }
}
