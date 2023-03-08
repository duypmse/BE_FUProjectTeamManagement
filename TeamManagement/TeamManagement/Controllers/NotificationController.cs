using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TeamManagement.Repository.Repositories.NotificationRepository;
using TeamManagement.Repository.RequestBodyModel.NotificationModel;

namespace TeamManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationRepository _notification;
        public NotificationController(INotificationRepository notification)
        {
            _notification = notification;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnNotificationAsync(CreateNotification createNotification)
        {
            var newNoti = await _notification.CreateAnNotificationAsync(createNotification);
            return (!newNoti) ? BadRequest() : Ok("Successfully created");    
        }
    }
}
