using System;
using System.Collections.Generic;

#nullable disable

namespace TeamManagement.Repository.Models
{
    public partial class Topic
    {
        public Topic()
        {
            TeacherTopics = new HashSet<TeacherTopic>();
            TeamTopics = new HashSet<TeamTopic>();
        }

        public int TopicId { get; set; }
        public string TopicName { get; set; }
        public int? CourseId { get; set; }
        public DateTime? DeadlineDate { get; set; }
        public string Requirement { get; set; }
        public int? Status { get; set; }

        public virtual Course Course { get; set; }
        public virtual ICollection<TeacherTopic> TeacherTopics { get; set; }
        public virtual ICollection<TeamTopic> TeamTopics { get; set; }
    }
}
