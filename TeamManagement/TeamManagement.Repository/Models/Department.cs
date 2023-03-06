using System;
using System.Collections.Generic;

#nullable disable

namespace TeamManagement.Repository.Models
{
    public partial class Department
    {
        public Department()
        {
            Subjects = new HashSet<Subject>();
        }

        public int DeptId { get; set; }
        public string DeptName { get; set; }
        public int? Status { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
