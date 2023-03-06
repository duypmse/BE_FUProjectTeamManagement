using System;
using System.Collections.Generic;

#nullable disable

namespace TeamManagement.Repository.Models
{
    public partial class Subject
    {
        public Subject()
        {
            Courses = new HashSet<Course>();
        }

        public int SubId { get; set; }
        public string SubName { get; set; }
        public int? DeptId { get; set; }
        public int? Status { get; set; }

        public virtual Department Dept { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
