using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManagement.DTO;

namespace TeamManagement.Repository.RequestBodyModel
{
    public class TeamDetailModel
    {
        public string? TeamName { get; set; }
        public int TeamCount { get; set; }
        public List<StudentDTO>? Students { get; set; }
        public string? TopicName { get; set; }
        public DateTime? DeadlineDate { get; set; }
        public string? Requirement { get; set; }
    }
}
