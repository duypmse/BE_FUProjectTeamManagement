using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.Repository.Models;
using TeamManagement.Repository.RequestBodyModel.TopicModel;
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
        public async Task<bool> UpdateATopicAsync(UpdateTopic topicU)
        {
            var topic = await _context.Topics.Where(t => t.TopicId == topicU.TopicId).FirstOrDefaultAsync();
            if (topic != null)
            {
                topic.TopicName = topicU.TopicName;
                topic.DeadlineDate = topicU.DeadlineDate;
                topic.Requirement = topicU.Requirement;
                _context.Topics.Update(topic);
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
        public async Task<bool> CreateATopicInSubAsync(CreateTopicInSub newTopic)
        {
            var isExistName = await _context.Topics.Where(t => t.TopicName == newTopic.TopicName).AnyAsync();
            if (newTopic.SubId == 0 && isExistName == true) return false;
            var newT = new Topic
            {
                TopicName = newTopic.TopicName,
                CourseId = null,
                SubId = newTopic.SubId,
                DeadlineDate = newTopic.DeadlineDate,
                Requirement = newTopic.Requirement,
                Status = 1
            };
            _context.Add(newT);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveATopicAsyc(int topicId)
        {
            var topic = await _context.Topics.FindAsync(topicId);
            if (topic == null) return false;
            var topicTeam = await _context.TeamTopics.Where(tt => tt.TopicId == topic.TopicId).FirstOrDefaultAsync();
            if (topicTeam != null)
            {
                _context.TeamTopics.Remove(topicTeam);
                await _context.SaveChangesAsync();
            }
            _context.Topics.Remove(topic);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CloneATopicAsync(int topicId)
        {
            var cloneText = "Copy of ";
            var topic = await _context.Topics.Where(t => t.TopicId == topicId).FirstOrDefaultAsync();
            if(topic == null) return false;

            var newTopic = new Topic
            {
                TopicName = cloneText + topic.TopicName,
                CourseId = null,
                SubId = topic.SubId,
                DeadlineDate = topic.DeadlineDate,
                Requirement = topic.Requirement,
                Status = 1
            };

            var isExistingName = await _context.Topics.AnyAsync(t => t.TopicName == newTopic.TopicName);
            if(isExistingName == true) return false;
            _context.Topics.Add(newTopic);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
