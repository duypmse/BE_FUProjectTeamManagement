using System.Collections.Generic;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.Repository.RequestBodyModel.TopicModel;
using TeamManagement.RequestBodyModel;

namespace TeamManagement.Repositories.TopicRepository
{
    public interface ITopicRepository
    {
        Task<List<TopicDTO>> GetAllTopicAsync();     
        Task<TopicDTO> GetTopicByIdAsync(int topicId);
        Task<bool> UpdateATopicAsync(UpdateTopic topicU);
        Task<bool> CreateATopicInSubAsync(CreateTopicInSub newTopic);
        Task<bool> RemoveATopicAsyc(int topicId);
        Task<bool> CloneATopicAsync(int topicId);
    }
}
