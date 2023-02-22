using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.Repositories.TeamRepository;

namespace TeamManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamRepository _teamRepository;

        public TeamController(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetListTeam()
        {
            var listTeam = await _teamRepository.GetAllTeamAsync();
            return Ok(listTeam);
        }
        [HttpGet("{teamId}/Student")]
        public async Task<IActionResult> GetListStudentByTeamId(int teamId)
        {
            var listStudent = await _teamRepository.GetListStudentByTeamIdAsync(teamId);
            if (!listStudent.Any())
            {
                return NoContent();
            }
            return Ok(listStudent);
        }
        [HttpPut]
        public async Task<IActionResult> AddStudentToTeam(int teamId, int studentId)
        {
            await _teamRepository.AddStudentToTeamAsync(teamId, studentId);
            return Ok("Add successful");
        }
    }
}
