using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TeamManagement.DTO;
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

        [HttpPost]
        public async Task<IActionResult> CreateAStudent([FromBody] StudentDTO studentDTO)
        {
            var student = await _student.CreateAStudentAsync(studentDTO);
            if (!student)
            {
                return BadRequest();
            }
            return Ok("Successfully created");
        }
    }
}
