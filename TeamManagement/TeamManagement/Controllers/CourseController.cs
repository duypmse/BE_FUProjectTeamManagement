using Google.Apis.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.Models;
using TeamManagement.Repositories.CourseReposiory;

namespace TeamManagement.Controllers
{
    [Authorize(Roles = "teacher")]
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;

        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCourse()
        {
            return Ok(await _courseRepository.GetAllCoursesAsync());
        }
        [HttpGet("{courseId}/Team")]
        public async Task<IActionResult> GetListTeamByCourseId(int courseId)
        {
            var listTeam = await _courseRepository.GetListTeamByCourseIdAsync(courseId);
            if (!listTeam.Any())
            {
                return NoContent();
            }
            return Ok(listTeam);
        }

        [HttpGet("{courseId}/Student")]
        public async Task<IActionResult> GetListStudentByCourseId(int courseId)
        {
            var listStudent = await _courseRepository.GetListStudentByCourseIdAsync(courseId);
            if (!listStudent.Any())
            {
                return NoContent();
            }
            return Ok(listStudent); 
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewCourse(CourseDTO course)
        {
            var existingCourse = await _courseRepository.GetCourseByNameAsync(course.CourseName);
            if (existingCourse != null)
            {
                return BadRequest();
            }
            else
            {
                await _courseRepository.CreateCoursesAsync(course);
                return Ok();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteACourse(int id)
        {
            try
            {
                await _courseRepository.DeleteCoursesAsync(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
