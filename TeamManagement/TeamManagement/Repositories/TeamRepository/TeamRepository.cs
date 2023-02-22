using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.Models;

namespace TeamManagement.Repositories.TeamRepository
{
    public class TeamRepository : ITeamRepository
    {
        private readonly FUProjectTeamManagementContext _context;
        private readonly IMapper _mapper;

        public TeamRepository(FUProjectTeamManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TeamDTO>> GetAllTeamAsync()
        {
            var listTeam = await _context.Teams.ToListAsync();
            return _mapper.Map<List<TeamDTO>>(listTeam);
        }

        public async Task<List<StudentDTO>> GetListStudentByTeamIdAsync(int teamId)
        {
            var listStudent = await _context.Participants.Where(t => t.TeamId == teamId).Select(s => s.Stu).ToListAsync();
            return _mapper.Map<List<StudentDTO>>(listStudent); 
        }

        public async Task AddStudentToTeamAsync(int teamId, int studentId)
        {
            var student = await _context.Participants.Where(s => s.StuId == studentId && s.CourseId != null).Select(t => t.Stu).FirstOrDefaultAsync();
            var team = await _context.Teams.Where(t => t.TeamId == teamId).FirstOrDefaultAsync();
            if(student == null)
            {
                throw new Exception("Student not found or not join course yet");
            }
            if(team == null)
            {
                throw new Exception("Team not found");
            }
            team.TeamCount++;
            var participant = new Participant()
            {
                Team = team,
                Stu = student,
                Status = 1
            };
            await _context.AddAsync(participant);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CreateATeamAsync(TeamDTO teamDto)
        {
            var existingTeamName = _context.Teams.Where(t => t.TeamName== teamDto.TeamName).FirstOrDefault();
            if(existingTeamName == null)
            {
                var newTeam = _mapper.Map<Team>(teamDto);
                newTeam.TeamCount = 0;
                newTeam.Status = 1;
                await _context.AddAsync(newTeam);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
