using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.Repository.Models;
//using TeamManagement.Models;

namespace TeamManagement.Repositories.AdminRepository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly FUProjectTeamManagementContext _context;
        private readonly IMapper _mapper;
        public AdminRepository(FUProjectTeamManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<AdminDTO>> GetListAdminAsync()
        {
            var listAdmin = await _context.Admins.ToListAsync();
            return _mapper.Map<List<AdminDTO>>(listAdmin);
        }

        public async Task<Admin> getAdminByEmail(string email)
        {
            return await _context.Admins.Where(a => a.AdminEmail == email).FirstOrDefaultAsync();
        }
    }
}
