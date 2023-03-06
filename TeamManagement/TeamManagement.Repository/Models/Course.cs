using System;
using System.Collections.Generic;

#nullable disable

namespace TeamManagement.Repository.Models
{
    public partial class Course
    {
        public Course()
        {
            CourseTeams = new HashSet<CourseTeam>();
            Participants = new HashSet<Participant>();
            TeacherCourses = new HashSet<TeacherCourse>();
            Topics = new HashSet<Topic>();
        }

        public int CourseId { get; set; }
        public string Image { get; set; }
        public string CourseName { get; set; }
        public string KeyEnroll { get; set; }
        public int? SubId { get; set; }
        public int? SemId { get; set; }
        public int? Status { get; set; }

        public virtual Semester Sem { get; set; }
        public virtual Subject Sub { get; set; }
        public virtual ICollection<CourseTeam> CourseTeams { get; set; }
        public virtual ICollection<Participant> Participants { get; set; }
        public virtual ICollection<TeacherCourse> TeacherCourses { get; set; }
        public virtual ICollection<Topic> Topics { get; set; }
    }
}
