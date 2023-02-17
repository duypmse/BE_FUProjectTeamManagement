using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TeamManagement.Models;
using TeamManagement.Repository;

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
        public async Task<IActionResult> GetAllCourse() {
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
        public async Task<IActionResult> AddNewCourse(Course course)
        {
            var newCourse = _courseRepository.AddCoursesAsync(course);
            return Ok(newCourse);
        }
    }
}
