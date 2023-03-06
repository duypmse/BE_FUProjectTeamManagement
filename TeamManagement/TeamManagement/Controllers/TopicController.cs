using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.Repositories.TopicRepository;

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
        public async Task<IActionResult> CreateATopicAsync(int teamId, TopicDTO topicDTO)
        {
            var newTopic = await _topicRepository.CreateATopicAsync( teamId, topicDTO);
            if (!newTopic)
            {
                return BadRequest();
            }
            return Ok("Successfully created!");
        }
        
    }
}
