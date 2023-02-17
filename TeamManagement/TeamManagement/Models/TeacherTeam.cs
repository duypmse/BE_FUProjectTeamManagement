using System;
using System.Collections.Generic;

#nullable disable

namespace TeamManagement.Models
{
    public partial class TeacherTeam
    {
        public int TeacherId { get; set; }
        public int TeamId { get; set; }

        public virtual Teacher Teacher { get; set; }
        public virtual Team Team { get; set; }
    }
}
