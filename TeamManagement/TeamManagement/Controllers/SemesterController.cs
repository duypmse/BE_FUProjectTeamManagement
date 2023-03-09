using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.Repositories.SemesterRepository;

namespace TeamManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SemesterController : ControllerBase
    {
        private readonly ISemesterRepository _semesterRepository;
        private readonly IDistributedCache _distributedCache;

        public SemesterController(ISemesterRepository semesterRepository, IDistributedCache distributedCache)
        {
            _semesterRepository = semesterRepository;
            _distributedCache = distributedCache;
        }
        [HttpGet]
        public async Task<IActionResult> GetActionResultAsync()
        {
            var listSemester = await _semesterRepository.GetAllSemesterAsync();
            if (!listSemester.Any())
            {
                return NoContent();
            }
            return Ok(listSemester);
        }
        [HttpGet("{semesterId}")]
        public async Task<IActionResult> GetSemesterByIdAsync(int semesterId)
        {
            SemesterDTO semester = null;
            string semStr = _distributedCache.GetString("semObj_" + semesterId.ToString());

            if(!string.IsNullOrEmpty(semStr))
            {
                semester = JsonConvert.DeserializeObject<SemesterDTO>(semStr);
            } else
            {
                semester = await _semesterRepository.GetSemesterByIdAsync(semesterId);
                if(semester!=null){
                    await _distributedCache.SetStringAsync("semObj_" + semesterId.ToString(), JsonConvert.SerializeObject(semester));
                }
            }

            if (semester == null) return NotFound();
            return Ok(semester);
        }
        [HttpGet("semesterName")]
        public async Task<IActionResult> GetSemesterByNameAsync(string semesterName)
        {
            var semester = await _semesterRepository.GetSemesterByNameAsync(semesterName);
            if (semester == null)
            {
                return NotFound();
            }
            return Ok(semester);
        }
        [HttpPost]
        public async Task<IActionResult> CreateSemesterAsync(SemesterDTO semesterDto)
        {
            var newSemester = await _semesterRepository.CreateSemesterAsync(semesterDto);
            if (!newSemester)
            {
                return BadRequest();
            }
            return Ok("Successfully created");
        }
    }
}
