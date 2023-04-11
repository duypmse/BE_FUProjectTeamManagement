using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.Repositories.TopicRepository;
using TeamManagement.Repository.RequestBodyModel.TopicModel;

namespace TeamManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly ITopicRepository _topicRepository;
        public TopicController(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTopicAsync()
        {
            var listTopic = await _topicRepository.GetAllTopicAsync();
            return Ok(listTopic);
        }

        [HttpGet("{topicId}")]
        public async Task<IActionResult> GetTopicByIdAsyc(int topicId)
        {
            var topic = await _topicRepository.GetTopicByIdAsync(topicId);
            return (topic == null) ? BadRequest() : Ok(topic);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTopicAsync(CreateTopicInSub newTopic)
        {
            var newT = await _topicRepository.CreateATopicInSubAsync(newTopic);
            return newT ? Ok("Successfully created") : BadRequest();
        }
        [HttpPost("{topicId}/Clone")]
        public async Task<IActionResult> CloneTopicAsync(int topicId)
        {
            var newT = await _topicRepository.CloneATopicAsync(topicId);
            return newT ? Ok("Successfully Cloned") : BadRequest();
        }
        [HttpPut("/Update")]
        public async Task<IActionResult> UpdateATopicInTeamAsync(UpdateTopic topicU)
        {
            var updateTopic = await _topicRepository.UpdateATopicAsync(topicU);
            if (!updateTopic)
            {
                return BadRequest();
            }
            return Ok("Successfully created!");
        }
        [HttpDelete("{topicId}")]
        public async Task<IActionResult> RemoveATopicAsync(int topicId)
        {
            var removeTopic = await _topicRepository.RemoveATopicAsyc(topicId);
            return removeTopic ? Ok("Successfully removed") : BadRequest(); 
        }
    }
}
