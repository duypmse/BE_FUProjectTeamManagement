using System.Collections.Generic;
using System.Threading.Tasks;
using TeamManagement.DTO;

namespace TeamManagement.Repositories.DepartmentRepository
{
    public interface IDepartmentRepository
    {
        Task<List<DepartmentDTO>> GetAllDepartmentAsync();
        Task<DepartmentDTO> GetADepartmentByIdAsync(int departmentId);
        Task<bool> CreateADepartmentAsync(DepartmentDTO departmentDTO);
        Task<bool> UpdateADepartmentAsync(DepartmentDTO DepartmentDTO);
        Task<bool> DeleteADepartmentAsync(int  departmentId);
    }
}
