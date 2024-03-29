﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamManagement.Repository.RequestBodyModel.NotificationModel
{
    public class GetAnNotification
    {
        public int NotificationId { get; set; }
        public string Title { get; set; }
        public string? FileNoti { get; set; }
        public string Message { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
