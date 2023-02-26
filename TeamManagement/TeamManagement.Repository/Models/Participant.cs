﻿using System;
using System.Collections.Generic;

#nullable disable

namespace TeamManagement.Models
{
    public partial class Participant
    {
        public int ParticipantId { get; set; }
        public int? TeamId { get; set; }
        public int? StuId { get; set; }
        public int? CourseId { get; set; }
        public int? Status { get; set; }

        public virtual Course Course { get; set; }
        public virtual Student Stu { get; set; }
        public virtual Team Team { get; set; }
    }
}