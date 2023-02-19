using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TeamManagement.Models;

namespace TeamManagement.Repositories.AdminRepository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly FUProjectTeamManagementContext _context;

        public AdminRepository(FUProjectTeamManagementContext context)
        {
            _context = context;
        }

        public async Task<Admin> getAdminByEmail(string email)
        {
            return await _context.Admins.Where(a => a.AdminEmail == email).FirstOrDefaultAsync();
        }
    }
}
