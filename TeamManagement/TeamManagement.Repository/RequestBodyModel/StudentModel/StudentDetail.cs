using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamManagement.Repository.RequestBodyModel.StudentModel
{
    public class StudentDetail
    {
        public int StuId { get; set; }
        public string? StuCode { get; set; }
        public string? StuName { get; set; }
        public string? StuEmail { get; set; }
        public string? StuPhone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? StuGender { get; set; }
        public string? TeacherNote { get; set; }
        public int? Score { get; set; }
        public int? Status { get; set; }
    }
}
