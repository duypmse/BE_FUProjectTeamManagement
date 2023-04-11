using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamManagement.Repository.RequestBodyModel.StudentModel
{
    public class AddNote
    {
        public int TeamId { get; set; }
        public int StudentId { get; set; }
        public string? Note { get; set; }
    }
}
