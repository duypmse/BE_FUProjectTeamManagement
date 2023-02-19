using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.Models;
using TeamManagement.Repositories.CourseReposiory;

namespace TeamManagement.Controllers
{
    [Authorize(Roles = "admin, teacher")]
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
            try
            {
                return Ok(await _courseRepository.GetAllCoursesAsync());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddNewCourse(CourseDTO course)
        {
            var existingCourse = await _courseRepository.GetCourseByNameAsync(course.CourseName);
            if (existingCourse != null)
            {
                return BadRequest("This Course is existed");
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
