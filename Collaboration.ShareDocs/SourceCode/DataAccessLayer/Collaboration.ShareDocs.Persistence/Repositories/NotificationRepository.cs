using Collaboration.ShareDocs.Persistence.Entities;
using Collaboration.ShareDocs.Persistence.Hubs;
using Collaboration.ShareDocs.Persistence.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Persistence.Repositories
{
    public class NotificationRepository:INotificationRepository
    {
        private readonly AppDbContext _context;
        private readonly IFollowRepository _followRepository;
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationRepository(AppDbContext context,IFollowRepository followRepository, IHubContext<NotificationHub> hubContext)
        {
            _context = context;
            _followRepository = followRepository;
            _hubContext = hubContext;
        }

        public async Task Create(Notification notification, Guid currentUserid,CancellationToken cancellationToken)
        {
            await _context.Notifications.AddAsync(notification);
            //TODO: save changes with unit of work
           //await _context.SaveChangesAsync();
            //TODO:Aassign notification to the user 
            var followingUsers = await _followRepository.GetFollowing(currentUserid, cancellationToken);
            foreach (var following in followingUsers)
            {
                var userNotification = new NotificationApplicationUser();
                userNotification.ApplicationUserId = following.ToString();
                userNotification.NotificationId = notification.NotificationId;

                await _context.UserNotifications.AddAsync(userNotification);
                _context.SaveChanges();
            }
            //todo: add hub
            await _hubContext.Clients.All.SendAsync("displayNotification", cancellationToken);

        }

        public async Task<List<NotificationApplicationUser>> GetUserNotifications(string userId, CancellationToken cancellationToken)
        {
            var response= await _context.UserNotifications.Where(u => u.ApplicationUserId.Equals(userId) && !u.IsRead )
                                                   .Include(n => n.Notification).ToListAsync();
            return response;
        }

        public  bool ReadNotification(Guid notificationId, string userId)
        {
            var notification =  _context.UserNotifications
                                       .FirstOrDefault(n => n.ApplicationUserId.Equals(userId)
                                       && n.NotificationId == notificationId);
            notification.IsRead = true;
             _context.UserNotifications.Update(notification);
             _context.SaveChangesAsync();
            return notification.IsRead;
        }

       
        
              
        
    }
}
