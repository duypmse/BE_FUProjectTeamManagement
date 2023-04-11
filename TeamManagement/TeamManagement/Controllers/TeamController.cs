using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.Repositories.TeamRepository;
using TeamManagement.Repository.RequestBodyModel.TeamTopicModel;
using TeamManagement.RequestBodyModel;

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
        [HttpGet("{teamId}/Details")]
        public async Task<IActionResult> GetTeamDetailByIdAsync(int teamId)
        {
            var teamDetail = await _teamRepository.GetTeamDetailByIdAsync(teamId);
            return (teamDetail == null) ? NoContent() : Ok(teamDetail);
        }
        [HttpGet("{teamId}/Get-a-topic")]
        public async Task<IActionResult> GetATopicByTeamIdAsync(int teamId)
        {
            var topic = await _teamRepository.GetATopicByTeamIdAsync(teamId);
            if(topic == null) return NoContent();
            return Ok(topic);
        }
        [HttpPost("Add-Topic")]
        public async Task<IActionResult> AddTopicToTeam(AddTopicToTeam addTT)
        {
            var addNew = await _teamRepository.AddTopicToTeamAsync(addTT);
            return !addNew ? BadRequest() : Ok("Successfully added!");
        }
        [HttpPost]
        public async Task<IActionResult> CreateATeamAsync(int courseId, TeamDTO teamDto)
        {
            var newTeam = await _teamRepository.CreateATeamToCourseAsync(courseId, teamDto);
            if (!newTeam)
            {
                return BadRequest("Name's team existing");
            }
            return Ok("Successfully created");
        }

        [HttpPut("{teamId}/Add-Students")]
        public async Task<IActionResult> AddStudentToTeam(int teamId, StudentModel studentModel)
        {
            var addNew = await _teamRepository.AddStudentToTeamAsync(teamId, studentModel.studentIds);
            if (!addNew)
            {
                return BadRequest("Student not found or not join course yet");
            }
            return Ok("Add successful");
        }
        [HttpPut("{teamId}/Remove/{studentId}")]
        public async Task<IActionResult> RemoveAStudentInTeam(int teamId, int studentId)
        {
            var remove = await _teamRepository.RemoveAStudentInTeamAsync(studentId, teamId);
            if (!remove)
            {
                return BadRequest();
            }
            return Ok("Successfully removed!");
        }
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateATeamAsync(TeamDTO teamDTO)
        {
            var upTeam = await _teamRepository.UpdateATeamAsync(teamDTO);
            if (!upTeam) return BadRequest();
            return Ok("Successfully updated!");
        }
        [HttpDelete("{teamId}")]
        public async Task<IActionResult> RemoveATeam(int teamId)
        {
            var removeTeam = await _teamRepository.RemoveATeamAsync(teamId);
            if (!removeTeam)
            {
                return BadRequest();
            }
            return Ok("Successfully removed!");
        }
    }
}
