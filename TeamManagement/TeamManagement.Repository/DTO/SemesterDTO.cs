namespace TeamManagement.DTO
{
    public class SemesterDTO
    {
        public int SemId { get; set; }
        public string? SemName { get; set; }
        public DateTime? StartDay { get; set; }
        public DateTime? EndDay { get; set; }
        public int? Status { get; set; }
    }
}
