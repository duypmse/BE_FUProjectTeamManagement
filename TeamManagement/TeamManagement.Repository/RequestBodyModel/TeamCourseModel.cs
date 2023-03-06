using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamManagement.Repository.RequestBodyModel
{
    public class TeamCourseModel
    {
        public int TeamId { get; set; }
        public string? TeamName { get; set; }
        public int? TeamCount { get; set; }
        public string? CourseName { get; set; }
        public string? TopicName { get; set; }
    }
}
