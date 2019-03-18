using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EastAdvising.Models
{
    public class Major
    {
        public int MajorId { get; set; }
        public string MajorName { get; set; }

        //Navigation Properties
        public virtual ICollection<Advisor> Advisors { get; set; }
        public virtual Department Department { get; set; }
    }
}
