namespace TeamManagement.DTO
{
    public class TopicDTO
    {
        public int TopicId { get; set; }
        public string? TopicName { get; set; }
        public int? CourseId { get; set; }
        public int? SubId { get; set; }
        public DateTime? DeadlineDate { get; set; }
        public string? Requirement { get; set; }
        public int? Status { get; set; }
    }
}
