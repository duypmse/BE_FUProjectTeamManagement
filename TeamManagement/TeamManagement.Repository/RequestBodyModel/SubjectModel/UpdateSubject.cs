using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamManagement.Repository.RequestBodyModel.SubjectModel
{
    public class UpdateSubject
    {
        public int SubId { get; set; }
        public string? SubName { get; set; }
        public string? SubFullName { get; set; }
        public string? Image { get; set; }
        public string? DeptName { get; set; }
    }
}
