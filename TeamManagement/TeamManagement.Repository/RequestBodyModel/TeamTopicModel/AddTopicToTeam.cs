using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamManagement.Repository.RequestBodyModel.TeamTopicModel
{
    public class AddTopicToTeam
    {
        public int TeamId { get; set; }
        public int TopicId { get; set; }
    }
}
