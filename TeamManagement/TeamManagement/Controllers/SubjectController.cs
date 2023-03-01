using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.Repositories.SubjectRepository;

namespace TeamManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectController(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSubjecttAsync()
        {
            var listSub = await _subjectRepository.GetAllSubjectAsync();
            return Ok(listSub);
        }
        [HttpGet("{subjectId}")]
        public async Task<IActionResult> GetSubjectByIdAsync(int subjectId)
        {
            var sub = await _subjectRepository.GetSubjectByIdAsync(subjectId);
            if(sub == null) return NotFound();
            return Ok(sub);
        }
        [HttpPost]
        public async Task<IActionResult> CreateASubjectAsync(SubjectDTO subjectDTO)
        {
            var newSub = await _subjectRepository.CreateASubjectAsync(subjectDTO);
            if(!newSub)
            {
                return BadRequest();
            }
            return Ok("Successfully created!");
        }
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateASubjectAsync(SubjectDTO subjectDTO)
        {
            var updateSubject = await _subjectRepository.UpdateASubjectAsync(subjectDTO);
            if(!updateSubject) return BadRequest();
            return Ok("Successfully updated!");
        }
        [HttpDelete("{subjectId}/Delete")]
        public async Task<IActionResult> DeleteASubjectAsync(int subjectId)
        {
            var deleteSubject = await _subjectRepository.DeleteASubjectAsync(subjectId);
            if(!deleteSubject) return BadRequest();
            return Ok("Successfully deleted!");
        }
    }
}
