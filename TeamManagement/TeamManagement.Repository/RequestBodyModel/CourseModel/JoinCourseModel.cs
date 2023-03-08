using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamManagement.Repository.RequestBodyModel.CourseModel
{
    public class JoinCourseModel
    {
        public int CourseId { get; set; }
        public string? KeyEnroll { get; set; }
        public int StuId { get; set; }
    }
}
