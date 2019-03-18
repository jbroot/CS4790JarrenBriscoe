using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EastAdvising.Models
{
    public class Service
    {
        public int ServicesId { get; set; }

        [Required]
        public int ServiceName { get; set; }

        public virtual ICollection<ServiceBooked> ServicesBooked { get; set; }
    }
}
