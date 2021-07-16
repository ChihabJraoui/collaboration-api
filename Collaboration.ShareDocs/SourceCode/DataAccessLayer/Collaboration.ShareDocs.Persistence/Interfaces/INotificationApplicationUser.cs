using Collaboration.ShareDocs.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Persistence.Interfaces
{
    public interface INotificationApplicationUser
    {
        Task<List<NotificationApplicationUser>> GetUserNotifications(string userId, CancellationToken cancellationToken);
        bool ReadNotification(NotificationApplicationUser notification);
        Task AssignNotificationToTheUser(Notification notification, string followedId, CancellationToken cancellationToken);
        Task AssignNotificationToTheUsers(Notification notification, ICollection<ApplicationUser> followingUsers, CancellationToken cancellationToken);
        Task<NotificationApplicationUser> GetNotification(Guid notificationId, string userId);
    }
}
