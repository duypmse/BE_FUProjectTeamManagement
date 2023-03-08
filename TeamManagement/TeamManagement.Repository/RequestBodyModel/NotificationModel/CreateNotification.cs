using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamManagement.Repository.RequestBodyModel.NotificationModel
{
    public class CreateNotification
    {
        public int? TeacherId { get; set; }
        public int? CourseId { get; set; }
        public string? Title { get; set; }
        public string? FileNoti { get; set; }
        public string? Message { get; set; }
    }
}
