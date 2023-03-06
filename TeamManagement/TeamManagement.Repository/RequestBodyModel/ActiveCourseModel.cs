using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamManagement.Repository.RequestBodyModel
{
    public class ActiveCourseModel
    {
        public int CourseId { get; set; }
        public string? Image { get; set; }
        public string? CourseName { get; set; }
        public string? TeacherName { get; set; } = string.Empty;
        public bool? IsEnrolled { get; set; }
    }
}
