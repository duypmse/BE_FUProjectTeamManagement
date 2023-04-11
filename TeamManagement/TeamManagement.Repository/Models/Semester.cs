using System;
using System.Collections.Generic;

#nullable disable

namespace TeamManagement.Repository.Models
{
    public partial class Semester
    {
        public Semester()
        {
            Courses = new HashSet<Course>();
        }

        public int SemId { get; set; }
        public string SemName { get; set; }
        public DateTime? StartDay { get; set; }
        public DateTime? EndDay { get; set; }
        public int? Status { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
