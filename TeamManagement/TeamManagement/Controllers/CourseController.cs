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
        [HttpPost]
        public async Task<IActionResult> AddNewCourse(CourseDTO course)
        {
            var existingCourse = await _courseRepository.GetCourseByNameAsync(course.CourseName);
            if (existingCourse != null)
            {
                return BadRequest();
            }
            else
            {
                await _courseRepository.AddCoursesAsync(course);
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
