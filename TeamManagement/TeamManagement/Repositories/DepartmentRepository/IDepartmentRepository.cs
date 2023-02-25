using System.Collections.Generic;
using System.Threading.Tasks;
using TeamManagement.DTO;

namespace TeamManagement.Repositories.DepartmentRepository
{
    public interface IDepartmentRepository
    {
        Task<List<DepartmentDTO>> GetAllDepartmentAsync();
        Task<bool> CreateADepartmentAsync(DepartmentDTO departmentDTO);
    }
}
