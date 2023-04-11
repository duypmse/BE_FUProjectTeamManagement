using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamManagement.Repository.RequestBodyModel.TopicModel
{
    public class CreateTopicInSub
    {
        public string? TopicName { get; set; }
        public int? SubId { get; set; }
        public DateTime? DeadlineDate { get; set; }
        public string? Requirement { get; set; }
    }
}
