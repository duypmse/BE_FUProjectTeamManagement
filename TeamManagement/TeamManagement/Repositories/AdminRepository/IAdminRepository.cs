using System.Threading.Tasks;
using TeamManagement.Models;

namespace TeamManagement.Repositories.AdminRepository
{
    public interface IAdminRepository
    {
        Task<Admin> getAdminByEmail(string email);
    }
}
