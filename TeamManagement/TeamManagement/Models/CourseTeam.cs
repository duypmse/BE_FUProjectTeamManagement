using System;
using System.Collections.Generic;

#nullable disable

namespace TeamManagement.Models
{
    public partial class CourseTeam
    {
        public int CourseId { get; set; }
        public int TeamId { get; set; }

        public virtual Course Course { get; set; }
        public virtual Team Team { get; set; }
    }
}
