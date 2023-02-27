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
    }
}
