using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.Models;

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
        public async Task<bool> CreateATopicAsync(TopicDTO topicDTO)
        {
            var topic = _mapper.Map<Topic>(topicDTO);
            if(topic == null)
            {
                return false;
            }
            topic.Status = 1;
            await _context.Topics.AddAsync(topic);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
