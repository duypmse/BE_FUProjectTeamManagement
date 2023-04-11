using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamManagement.Repository.RequestBodyModel.TopicModel
{
    public class UpdateTopic
    {
        public int TopicId { get; set; }
        public string? TopicName { get; set; }
        public DateTime? DeadlineDate { get; set; }
        public string? Requirement { get; set; }
    }
}
