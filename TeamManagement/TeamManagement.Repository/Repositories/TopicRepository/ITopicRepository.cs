using System.Collections.Generic;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.RequestBodyModel;

namespace TeamManagement.Repositories.TopicRepository
{
    public interface ITopicRepository
    {
        Task<List<TopicDTO>> GetAllTopicAsync();
        Task<TopicDTO> GetTopicByIdAsync(int topicId);
        Task<bool> CreateATopicAsync(int teamId, TopicDTO topicDTO);
    }
}
