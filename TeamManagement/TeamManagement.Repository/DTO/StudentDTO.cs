using System;

namespace TeamManagement.DTO
{
    public class StudentDTO
    {
        public int StuId { get; set; }
        public string? StuCode { get; set; }
        public string? StuName { get; set; }
        public string? StuEmail { get; set; }
        public string? StuPhone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? StuGender { get; set; }
        public int? Status { get; set; }
    }
}
