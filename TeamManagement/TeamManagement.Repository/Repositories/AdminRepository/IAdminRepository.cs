using System.Collections.Generic;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.Models;

namespace TeamManagement.Repositories.AdminRepository
{
    public interface IAdminRepository
    {
        Task<List<AdminDTO>> GetListAdminAsync();
        Task<Admin> getAdminByEmail(string email);
    }
}
