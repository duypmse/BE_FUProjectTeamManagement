using System;
using System.Collections.Generic;

#nullable disable

namespace TeamManagement.Repository.Models
{
    public partial class Team
    {
        public Team()
        {
            CourseTeams = new HashSet<CourseTeam>();
            Participants = new HashSet<Participant>();
            TeacherTeams = new HashSet<TeacherTeam>();
            TeamTopics = new HashSet<TeamTopic>();
        }

        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public int? TeamCount { get; set; }
        public int? Status { get; set; }

        public virtual ICollection<CourseTeam> CourseTeams { get; set; }
        public virtual ICollection<Participant> Participants { get; set; }
        public virtual ICollection<TeacherTeam> TeacherTeams { get; set; }
        public virtual ICollection<TeamTopic> TeamTopics { get; set; }
    }
}
