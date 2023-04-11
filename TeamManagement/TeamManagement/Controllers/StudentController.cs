using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.Repositories.StudentRepository;
using TeamManagement.Repository.RequestBodyModel.CourseModel;
using TeamManagement.Repository.RequestBodyModel.StudentModel;

namespace TeamManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _student;

        public StudentController(IStudentRepository student)
        {
            _student = student;
        }

        [HttpGet] 
        public async Task<IActionResult> GetAllStudent() {
            try
            {
                return Ok(await _student.GetAllStudent());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{studentId}")]
        public async Task<IActionResult> GetAStudentByid(int studentId)
        {
            var stu = await _student.GetAStudentById(studentId);
            if(stu  == null) return NoContent();
            return Ok(stu);
        }
        [HttpGet("{studentId}/Team/{teamId}/Detail")]
        public async Task<IActionResult> GetAStudentDetailByid(int teamId, int studentId)
        {
            var stu = await _student.GetAStudentDetailById(teamId, studentId);
            return stu == null ? NoContent() : Ok(stu);

        }
        [Authorize(Roles = "student")]
        [HttpGet("{studentId}/Active-for-student")]
        public async Task<IActionResult> GetAllActiveCourseForStudentAsync(int studentId)
        {
            var list = await _student.GetAllActiveCoursesForStudentAsync(studentId);
            if (list == null) return NoContent();
            return Ok(list);
        }

        [HttpGet("{studentId}/list-course")]
        public async Task<IActionResult> GetListCourseByStudentAsync(int studentId)
        {
            var list = await _student.GetListCourseByStudentAsync(studentId);
            if(list == null) return NoContent();
            return Ok(list);
        }
        [HttpGet("{studentId}/list-team")]
        public async Task<IActionResult> GetListTeamByStudentAsync(int studentId)
        {
            var list = await _student.GetListTeamByStudentAsync(studentId);
            if(list == null) return NoContent();
            return Ok(list);    
        }
        [HttpGet("{studentId}/Course/{courseId}/Noti")]
        public async Task<IActionResult> GetListNotiByStudent(int courseId, int studentId)
        {
            var list = await _student.GetListNotiByStudentAsync(courseId, studentId);
            if(list == null) return NoContent(); return Ok(list);
        }
        [HttpGet("{studentId}/Team/{teamId}/Get-Note")]
        public async Task<IActionResult> GetStudentNoteAsync(int studentId, int teamId)
        {
            var note = await _student.GetStudentNoteAsync(studentId, teamId);
            if(note.Equals("")) return BadRequest();
            return Ok(note);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAStudent([FromBody] StudentDTO studentDTO)
        {
            var student = await _student.CreateAStudentAsync(studentDTO);
            if (!student)
            {
                return BadRequest();
            }
            return Ok("Successfully created");
        }

        [HttpPost("Join-course")]
        public async Task<IActionResult> StudentJoinCourse(JoinCourseModel joinCourseModel)
        {
            var joining = await _student.StudentJoinCourseAsync(joinCourseModel);
            if (!joining)
            {
                return BadRequest();
            }
            return Ok("Successfully joined");
        }

        [HttpPost("Out-course")]
        public async Task<IActionResult> OutCourseAsync(JoinCourseModel joinCourseModel)
        {
            var outCourse = await _student.StudentOutCourseAsync(joinCourseModel);
            if (!outCourse) return BadRequest();
            return Ok("Successfully out");
        }
        [HttpPost("Add-Note")]
        public async Task<IActionResult> AddNoteToStudent(AddNote addNote)
        {
            var add = await _student.AddNoteToStudentAsync(addNote);
            return add ? Ok("Successfully added note") : BadRequest();
        }

        [HttpPost("Add-Score")]
        public async Task<IActionResult> AddScoreToStudent(AddScore addScore)
        {
            var add = await _student.AddScoreToStudentAsync(addScore);
            return add ? Ok("Successfully added score") : BadRequest();
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAStudentAsync(StudentDTO studentDTO)
        {
            var updateStu = await _student.UpdateAStudentAsync(studentDTO);
            if (!updateStu) return BadRequest();
            return Ok("Successfully updated!");
        }

        [HttpDelete("{studentId}/Delete")]
        public async Task<IActionResult> DeleteAStudentAsync(int studentId)
        {
            var deleteStu = await _student.DeleteAStudentAsync(studentId);
            if (!deleteStu) return BadRequest();
            return Ok("Successfully deleted!");
        }

    }
}
