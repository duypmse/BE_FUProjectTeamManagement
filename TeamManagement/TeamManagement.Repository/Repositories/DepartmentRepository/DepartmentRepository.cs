using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.Models;

namespace TeamManagement.Repositories.DepartmentRepository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly FUProjectTeamManagementContext _context;
        private readonly IMapper _mapper;

        public DepartmentRepository(FUProjectTeamManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<DepartmentDTO>> GetAllDepartmentAsync()
        {
            var listDepartment = await _context.Departments.ToListAsync();
            return _mapper.Map<List<DepartmentDTO>>(listDepartment);
        }
        public async Task<bool> CreateADepartmentAsync(DepartmentDTO departmentDTO)
        {
            var newDepartment = _mapper.Map<Department>(departmentDTO);
            var existingDeparment = await _context.Departments.Where(d => d.DeptName == departmentDTO.DeptName).FirstOrDefaultAsync();
            if(newDepartment != null && existingDeparment == null)
            {
                newDepartment.Status = 1;
                await _context.AddAsync(newDepartment);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
