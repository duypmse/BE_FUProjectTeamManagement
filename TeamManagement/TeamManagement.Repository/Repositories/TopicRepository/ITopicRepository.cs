using System.Collections.Generic;
using System.Threading.Tasks;
using TeamManagement.DTO;

namespace TeamManagement.Repositories.TopicRepository
{
    public interface ITopicRepository
    {
        Task<List<TopicDTO>> GetAllTopicAsync();
        Task<bool> CreateATopicAsync(TopicDTO topicDTO);
    }
}
