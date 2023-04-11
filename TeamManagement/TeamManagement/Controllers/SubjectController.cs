﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.Repositories.SubjectRepository;
using TeamManagement.Repository.RequestBodyModel.SubjectModel;

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
        [HttpGet("{subjectId}/Teacher/{teacherId}/List-course")]
        public async Task<IActionResult> GetListCourseById(int teacherId, int subjectId)
        {
            var list = await _subjectRepository.GetListCourseBySubId(teacherId, subjectId);
            return list == null ? NoContent() : Ok(list);
        }
        [HttpGet("{subjectId}/List-Topic")]
        public async Task<IActionResult> GetListTopicById(int subjectId)
        {
            var list = await _subjectRepository.GetListTopicBySubIdAsync(subjectId);
            if (!list.Any())
            {
                return NoContent();
            }
            return Ok(list);
        }
        [HttpGet("{subjectId}/List-Topic-NonTeamInCourse")]
        public async Task<IActionResult> GetTopicNonTeamInACourseByIdAsync(int subjectId)
        {
            var list = await _subjectRepository.GetListTopicNonTeamInACourse(subjectId);
            return list == null ? NoContent() : Ok(list);
        }
        [HttpPost]
        public async Task<IActionResult> CreateASubjectAsync(CreateSubject newSub)
        {
            var newSubj = await _subjectRepository.CreateASubjectAsync(newSub);
            if(!newSubj)
            {
                return BadRequest();
            }
            return Ok("Successfully created!");
        }
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateASubjectAsync(UpdateSubject updateSub)
        {
            var updateSubject = await _subjectRepository.UpdateASubjectAsync(updateSub);
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
