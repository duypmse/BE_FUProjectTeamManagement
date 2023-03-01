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
using TeamManagement.RequestBodyModel;

namespace TeamManagement.Controllers
{
    //[Authorize(Roles = "teacher")]
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
        [HttpGet("Active")]
        public async Task<IActionResult> GetAllActiveCourseAsync()
        {
            var list = await _courseRepository.GetAllActiveCoursesAsync();
            if (list == null) return NoContent();
            return Ok(list);
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

        [HttpGet("{courseId}/StudentNonTeam")]
        public async Task<IActionResult> GetListStudentByCourseId(int courseId)
        {
            var listStudent = await _courseRepository.GetListStudentNonTeamByCourseIdAsync(courseId);
            if (!listStudent.Any())
            {
                return NoContent();
            }
            return Ok(listStudent); 
        }
        [HttpGet("Course-have-Teacher")]
        public async Task<IActionResult> GetlistCourseHaveTeacherAsync()
        {
            var list = await _courseRepository.GetlistCourseHaveTeacherAsync();
            if (!list.Any())
            {
                return NoContent();
            }
            return Ok(list);
        }
        //[HttpGet("Not-taught")]
        //public async Task<IActionResult> GetCoursesNotTaughtAsync()
        //{
        //    var listCourse = await _courseRepository.GetCoursesNotTaughtAsync();
        //    if (listCourse == null) return NoContent();
        //    return Ok(listCourse);
        //}
        [HttpPost]
        public async Task<IActionResult> CreateNewCourse(TeacherCourseModel TCModel)
        {
            var existingCourse = await _courseRepository.GetCourseByNameAsync(TCModel.CourseName);
            if (existingCourse != null)
            {
                return BadRequest();
            }
            else
            {
                var newCourse = await _courseRepository.CreateCoursesAsync(TCModel);
                if(!newCourse)
                {
                    return BadRequest();
                }
                return Ok("Successfully created!");
            }
        }
        [HttpPost("Join-course")]
        public async Task<IActionResult> StudentJoinCourse(int courseId, string keyEnroll, int studentId)
        {
            var joining = await _courseRepository.StudentJoinCourse(courseId, keyEnroll, studentId);
            if (!joining)
            {
                return BadRequest();
            }
            return Ok("Successfully joined");
        }
        [HttpPut("{courseId}/ChangeStatus")]
        public async Task<IActionResult> ChangeCourseStatusAsync(int courseId)
        {
            var changeStatus = await _courseRepository.ChangeCourseStatusAsync(courseId);
            if(!changeStatus) return BadRequest();
            return Ok("Successfully change status!");
        }
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateCourseAsync(int teacherId, TeacherCourseModel TCModel)
        {
            var updateCourse = await _courseRepository.UpdateCourseAsync(teacherId, TCModel);
            if(!updateCourse) return BadRequest();
            return Ok("Successfully updated!");
        }
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteACourse(int id)
        //{
        //    try
        //    {
        //        await _courseRepository.DeleteCoursesAsync(id);
        //        return Ok();
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }
        //}

    }
}
