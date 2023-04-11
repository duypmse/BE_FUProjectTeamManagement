using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamManagement.Repository.RequestBodyModel.StudentModel
{
    public class AddScore
    {
        public int TeamId { get; set; }
        public int StudentId { get; set; }
        public int Score { get; set; }
    }
}
