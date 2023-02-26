namespace TeamManagement.RequestBodyModel
{
    public class TeacherCourseModel
    {
        public int CourseId { get; set; }
        public string? CourseName { get; set; }
        public string? KeyEnroll { get; set; }
        public int TeacherId { get; set; }
        public string TeacherName { get; set; } = string.Empty;
        public int? SubId { get; set; }
        public int? SemId { get; set; }
        public int? Status { get; set; }
    }
}
