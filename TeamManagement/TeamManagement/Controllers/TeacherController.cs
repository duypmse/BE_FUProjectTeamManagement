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

namespace TeamManagement.Controllers
{
    [Authorize(Roles = "admin, teacher")]
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
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeacherById(int id)
        {
            var teacher = await _teacherRepository.GetTeacherByIdAsync(id);
            return teacher == null ? NotFound() : Ok(teacher);
        }

        [AllowAnonymous]
        [HttpGet("Course/{teacherId}")]
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
        public async Task<IActionResult> AddTeacher(TeacherDTO teacher)
        {
            try
            {
                await _teacherRepository.AddTeacherAsync(teacher);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

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
