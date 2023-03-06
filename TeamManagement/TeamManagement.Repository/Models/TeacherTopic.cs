using System;
using System.Collections.Generic;

#nullable disable

namespace TeamManagement.Repository.Models
{
    public partial class TeacherTopic
    {
        public int TeacherId { get; set; }
        public int TopicId { get; set; }

        public virtual Teacher Teacher { get; set; }
        public virtual Topic Topic { get; set; }
    }
}
