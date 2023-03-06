using System;
using System.Collections.Generic;

#nullable disable

namespace TeamManagement.Repository.Models
{
    public partial class Student
    {
        public Student()
        {
            Participants = new HashSet<Participant>();
        }

        public int StuId { get; set; }
        public string StuCode { get; set; }
        public string StuName { get; set; }
        public string StuEmail { get; set; }
        public string StuPhone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string StuGender { get; set; }
        public int? Status { get; set; }

        public virtual ICollection<Participant> Participants { get; set; }
    }
}
