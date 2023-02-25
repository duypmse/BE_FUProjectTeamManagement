using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.Models;
using TeamManagement.Repositories.TeacherRepository;
using TeamManagement.RequestBodyModel;

namespace TeamManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherRepository _teacherRepository;

        public TeacherController(ITeacherRepository teacherRepository, IMapper mapper)
        {
            _teacherRepository = teacherRepository;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllTeacher()
        {
            try
            {
                return Ok(await _teacherRepository.GetAllTeacherAsync());
            }
            catch
            {
                return NoContent();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeacherById(int id)
        {
            var teacher = await _teacherRepository.GetTeacherByIdAsync(id);
            return teacher == null ? NoContent() : Ok(teacher);
        }
        [HttpGet("{teacherName}")]
        public async Task<IActionResult> GetTeacherByName(string teacherName)
        {
            var teacher = await _teacherRepository.GetTeacherByNameAsync(teacherName);
            if (teacher == null) return NoContent();
            return Ok(teacher);
        }
        [HttpGet("{teacherId}/Course")]
        public async Task<ActionResult> GetListCourseByTeacherId(int teacherId)
        {
            var courses = await _teacherRepository.GetListCourseByTeacherIdAsync(teacherId);
            if (!courses.Any())
            {
                return NoContent();
            }
            return Ok(courses);
        }

        [HttpPost]
        public async Task<IActionResult> AddTeacher(TeacherDTO teacherDto)
        {
            var teacher = await _teacherRepository.CreateTeacherAsync(teacherDto);
            if (!teacher)
            {
                return BadRequest();
            }
            return Ok("Successfully created");
        }
        //[HttpPost("{teacherId}/Add-course")]
        //public async Task<IActionResult> AddCoursesToTeacherAsync(int teacherId, CourseModel course)
        //{
        //    var add = await _teacherRepository.AddCoursesToTeacherAsync(teacherId, course.CourseIds);
        //    if (!add) return BadRequest();
        //    return Ok("Successfully added");   
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            try
            {
                await _teacherRepository.DeleteTeacherAsync(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
