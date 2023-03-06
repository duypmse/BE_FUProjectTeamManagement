using Google.Apis.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.Repositories.CourseReposiory;
using TeamManagement.Repositories.TeacherRepository;
using TeamManagement.Repository.Repositories.Notification;
using TeamManagement.RequestBodyModel;

namespace TeamManagement.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ITeacherRepository _teacherRepo;
        public CourseController(ICourseRepository courseRepository, ITeacherRepository teacherRepository)
        {
            _courseRepository = courseRepository;
            _teacherRepo = teacherRepository;
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
            var newCourse = await _courseRepository.CreateCoursesAsync(TCModel);
            var teacher = await _teacherRepo.GetTeacherByIdAsync(TCModel.TeacherId);
            if (!newCourse)  return BadRequest();
            Dictionary<string, string> data = new Dictionary<string, string>()
            {
                { "courseId", TCModel.CourseId.ToString() }
            };
            await PushNotification.SendMessage(teacher.TeacherEmail, "New course" 
                ,$"Course {TCModel.CourseName} has been added for you", data);
            return Ok("Successfully created!");
        }
        
        [HttpPut("{courseId}/ChangeStatus")]
        public async Task<IActionResult> ChangeCourseStatusAsync(int courseId)
        {
            var changeStatus = await _courseRepository.ChangeCourseStatusAsync(courseId);
            if (!changeStatus) return BadRequest();
            return Ok("Successfully change status!");
        }
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateCourseAsync(int courseId, TeacherCourseModel TCModel)
        {
            var updateCourse = await _courseRepository.UpdateCourseAsync(courseId, TCModel);
            if (!updateCourse) return BadRequest();
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
