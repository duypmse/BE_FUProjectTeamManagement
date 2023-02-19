using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TeamManagement.Repositories.StudentRepository;

namespace TeamManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _student;

        public StudentController(IStudentRepository student)
        {
            _student = student;
        }

        [HttpGet] 
        public async Task<IActionResult> GetAllStudent() {
            try
            {
                return Ok(await _student.GetAllStudent());
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
