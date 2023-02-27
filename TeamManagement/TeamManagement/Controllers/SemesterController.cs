using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.Repositories.SemesterRepository;

namespace TeamManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SemesterController : ControllerBase
    {
        private readonly ISemesterRepository _semesterRepository;

        public SemesterController(ISemesterRepository semesterRepository)
        {
            _semesterRepository = semesterRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetActionResultAsync()
        {
            var listSemester = await _semesterRepository.GetAllSemesterAsync();
            if (!listSemester.Any())
            {
                return NoContent();
            }
            return Ok(listSemester);
        }
        [HttpGet("{semesterId}")]
        public async Task<IActionResult> GetSemesterByIdAsync(int semesterId)
        {
            var semester = await _semesterRepository.GetSemesterByIdAsync(semesterId); 
            if (semester == null) return NotFound();
            return Ok(semester);
        }
        [HttpGet("semesterName")]
        public async Task<IActionResult> GetSemesterByNameAsync(string semesterName)
        {
            var semester = await _semesterRepository.GetSemesterByNameAsync(semesterName);
            if (semester == null)
            {
                return NotFound();
            }
            return Ok(semester);
        }
        [HttpPost]
        public async Task<IActionResult> CreateSemesterAsync(SemesterDTO semesterDto)
        {
            var newSemester = await _semesterRepository.CreateSemesterAsync(semesterDto);
            if (!newSemester)
            {
                return BadRequest();
            }
            return Ok("Successfully created");
        }
    }
}
