using System;
using System.Collections.Generic;

#nullable disable

namespace TeamManagement.Repository.Models
{
    public partial class Notification
    {
        public int NotificationId { get; set; }
        public int? TeacherId { get; set; }
        public int? CourseId { get; set; }
        public string Title { get; set; }
        public string FileNoti { get; set; }
        public string Message { get; set; }
        public DateTime? CreatedDate { get; set; } 
        public int? Status { get; set; }

        public virtual Course Course { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
