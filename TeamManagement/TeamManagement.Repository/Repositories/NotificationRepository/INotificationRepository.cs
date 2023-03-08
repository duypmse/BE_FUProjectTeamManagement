using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManagement.Repository.RequestBodyModel.NotificationModel;

namespace TeamManagement.Repository.Repositories.NotificationRepository
{
    public interface INotificationRepository
    {
        Task<bool> CreateAnNotificationAsync(CreateNotification createNoti);
    }
}
