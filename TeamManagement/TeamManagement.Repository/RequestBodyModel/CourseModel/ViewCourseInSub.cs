using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamManagement.Repository.RequestBodyModel.CourseModel
{
    public class ViewCourseInSub
    {
        public int CourseId { get; set; }
        public string? CourseName { get; set; }
        public string? SemName { get; set; }
    }
}
