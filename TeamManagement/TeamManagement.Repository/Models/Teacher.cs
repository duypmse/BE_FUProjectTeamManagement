using System;
using System.Collections.Generic;

#nullable disable

namespace TeamManagement.Repository.Models
{
    public partial class Teacher
    {
        public Teacher()
        {
            Notifications = new HashSet<Notification>();
            TeacherCourses = new HashSet<TeacherCourse>();
            TeacherTeams = new HashSet<TeacherTeam>();
            TeacherTopics = new HashSet<TeacherTopic>();
        }

        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public string TeacherEmail { get; set; }
        public string TeacherPhone { get; set; }
        public int? Status { get; set; }

        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<TeacherCourse> TeacherCourses { get; set; }
        public virtual ICollection<TeacherTeam> TeacherTeams { get; set; }
        public virtual ICollection<TeacherTopic> TeacherTopics { get; set; }
    }
}
