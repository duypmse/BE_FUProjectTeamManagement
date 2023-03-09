using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamManagement.Repository.RequestBodyModel.CourseModel
{
    public class UpdateCourseModel
    {
        public int CourseId { get; set; }
        public string? Image { get; set; }
        public string? CourseName { get; set; }
        public string? KeyEnroll { get; set; }
        public string? TeacherName { get; set; }
        public string? SubName { get; set; }
        public string? SemName { get; set; }
    }
}
