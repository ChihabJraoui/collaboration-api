using Collaboration.ShareDocs.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Persistence.Interfaces
{
    public interface INotificationRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        Task<List<NotificationApplicationUser>> GetUserNotifications(string userId, CancellationToken cancellationToken);
        Task Create(Notification notification, Guid id, CancellationToken cancellationToken);
        bool ReadNotification(Guid notificationId, string userId);
    }
}
