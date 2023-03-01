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

        public async Task<DepartmentDTO> GetADepartmentByIdAsync(int departmentId)
        {
            var department = await _context.Departments.FindAsync(departmentId);
            return _mapper.Map<DepartmentDTO>(department);  
        }

        public async Task<bool> UpdateADepartmentAsync(DepartmentDTO DepartmentDTO)
        {
            if (DepartmentDTO != null) 
            {
                var updateDep = _mapper.Map<Department>(DepartmentDTO);
                _context.Update(updateDep);
                await _context.SaveChangesAsync();  
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteADepartmentAsync(int departmentId)
        {
            var dep = await _context.Departments.FindAsync(departmentId);
            if(dep != null)
            {
                var isInSubject = await _context.Subjects.Where(s => s.DeptId == dep.DeptId).ToListAsync();
                if (isInSubject.Any())
                {
                    foreach(var i in isInSubject)
                    {
                        i.DeptId = null;
                    }
                    await _context.SaveChangesAsync();
                }
                _context.Remove(dep);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
