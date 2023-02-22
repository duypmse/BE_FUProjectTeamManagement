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

        public async Task<bool> AddStudentToTeamAsync(int teamId, int studentId)
        {
            var participant = await _context.Participants.Where(s => s.StuId == studentId && s.CourseId != null && s.TeamId == null).FirstOrDefaultAsync();
            var team = await _context.Teams.Where(t => t.TeamId == teamId).FirstOrDefaultAsync();
            var existingTeam = await _context.Participants.Where(e => e.StuId == studentId && e.TeamId == teamId).FirstOrDefaultAsync();
            if (existingTeam != null)
            {
                return false;
            }
            if(participant == null || team == null)
            {
                return false;
            } 
            team.TeamCount++;
            participant.Team = team;
            participant.Status = 1;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CreateATeamToCourseAsync(int courseId, TeamDTO teamDto)
        {
            var existingTeamName = _context.Teams.Where(t => t.TeamName== teamDto.TeamName).FirstOrDefault();
            var course = await _context.Courses.Where(c => c.CourseId == courseId).FirstOrDefaultAsync();
            if(existingTeamName == null)
            {
                var newTeam = _mapper.Map<Team>(teamDto);
                var newCourse = new CourseTeam()
                {
                    Course = course,
                    Team = newTeam
                };
                await _context.AddAsync(newCourse);
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
