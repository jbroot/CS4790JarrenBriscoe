using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EastAdvising.Models
{
    public class College
    {
        public int CollegeId { get; set; }
        public string CollegeName { get; set; }

        //Navigation Properties
        public virtual ICollection<Department> Departments { get; set; }
    }
}
