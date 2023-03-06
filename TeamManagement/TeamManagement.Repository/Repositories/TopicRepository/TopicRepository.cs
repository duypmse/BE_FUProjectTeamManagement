using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.Repository.Models;
using TeamManagement.RequestBodyModel;

namespace TeamManagement.Repositories.TopicRepository
{
    public class TopicRepository : ITopicRepository
    {
        private readonly FUProjectTeamManagementContext _context;
        private readonly IMapper _mapper;

        public TopicRepository(FUProjectTeamManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TopicDTO>> GetAllTopicAsync()
        {
            var listTopic = await _context.Topics.ToListAsync();
            return _mapper.Map<List<TopicDTO>>(listTopic);
        }
        public async Task<bool> CreateATopicAsync(int teamId, TopicDTO topicDTO)
        {
            //var topic = _mapper.Map<Topic>(topicDTO);
            //if(topic == null) return false;
            var course = await _context.CourseTeams.Where(ct => ct.TeamId == teamId).Select(c => c.Course).FirstOrDefaultAsync();
            if (topicDTO.TopicId != 0)
            {
                var updateTopic = new Topic 
                {
                    TopicId = topicDTO.TopicId,
                    TopicName = topicDTO.TopicName,
                    CourseId = course.CourseId,
                    DeadlineDate = topicDTO.DeadlineDate,
                    Requirement = topicDTO.Requirement,
                    Status = 1
                };
                _context.Update(updateTopic);
                await _context.SaveChangesAsync();
                return true;
            }
            if(topicDTO.TopicId == 0)
            {
                var isExisting = await _context.TeamTopics.Where(tt => tt.TeamId == teamId).Select(to => to.Topic).AnyAsync();
                if (isExisting) return false;
                var newTopic = new Topic
                {
                    TopicName = topicDTO.TopicName,
                    CourseId = course.CourseId,
                    DeadlineDate = topicDTO.DeadlineDate,
                    Requirement = topicDTO.Requirement,
                    Status = 1,
                };
                await _context.Topics.AddAsync(newTopic);
                await _context.SaveChangesAsync();

                var newTeamTopic = new TeamTopic
                {
                    TeamId = teamId,
                    TopicId = newTopic.TopicId
                };

                await _context.AddAsync(newTeamTopic);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<TopicDTO> GetTopicByIdAsync(int topicId)
        {
            var topic = await _context.Topics.FindAsync(topicId);
            return _mapper.Map<TopicDTO>(topic);
        }
    }
}
