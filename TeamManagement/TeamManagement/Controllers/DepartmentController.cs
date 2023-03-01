using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.Repositories.DepartmentRepository;

namespace TeamManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDepartmentAsync()
        {
            var listDepartment = await _departmentRepository.GetAllDepartmentAsync();
            return Ok(listDepartment);
        }
        [HttpGet("{depId}")]
        public async Task<IActionResult> GetDepartmentByIdAsync(int depId)
        {
            var dep = await _departmentRepository.GetADepartmentByIdAsync(depId);
            if (dep == null) return NoContent();
            return Ok(dep);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateADepartment(DepartmentDTO departmentDTO)
        {
            var newDep = await _departmentRepository.CreateADepartmentAsync(departmentDTO);
            if (!newDep)
            {
                return BadRequest();
            }
            return Ok("Successfully created!");
        }
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateADepartmentAsync(DepartmentDTO departmentDTO)
        {
            var updateDep = await _departmentRepository.UpdateADepartmentAsync(departmentDTO);
            if(!updateDep) return BadRequest();
            return Ok("Successfully updated!");
        }
        [HttpDelete("{depId}")]
        public async Task<IActionResult> DeleteADepartmentAsync(int depId)
        {
            var deleteDep = await _departmentRepository.DeleteADepartmentAsync(depId);
            if(!deleteDep) return BadRequest();
            return Ok("Successfully deleted!");
        }
    }
}
