using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamManagement.Repository.RequestBodyModel.SubjectModel
{
    public class ViewSubject
    {
        public int SubId { get; set; }
        public string? SubName { get; set; }
        public string? DeptName { get; set; }
        public int? Status { get; set; }
    }
}
