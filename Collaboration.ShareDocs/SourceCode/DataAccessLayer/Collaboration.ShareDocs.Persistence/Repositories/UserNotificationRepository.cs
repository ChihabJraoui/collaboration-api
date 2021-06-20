using Collaboration.ShareDocs.Persistence.Entities;
using Collaboration.ShareDocs.Persistence.Hubs;
using Collaboration.ShareDocs.Persistence.Interfaces;
using Microsoft.AspNetCore.Identity;
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
    public class UserNotificationRepository : GenericRepository<NotificationApplicationUser>, INotificationApplicationUser
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly AppDbContext _context;
       
        private readonly UserManager<ApplicationUser> _userManager;

        public UserNotificationRepository(AppDbContext context, IHubContext<NotificationHub> hubContext,
            UserManager<ApplicationUser> userManager) : base(context)

        {
            _hubContext = hubContext;
            _context = context;
            _userManager = userManager;
        }

        public async Task<List<NotificationApplicationUser>> GetUserNotifications(string userId, CancellationToken cancellationToken)
        {
            var response = await _context.UserNotifications.Where(u => u.ApplicationUserId.Equals(userId) && !u.IsRead)
                                                   .Include(n => n.Notification).ToListAsync();
            return response;
        }
        //public async Task<Notification> GetUserNotification(Ge userId, CancellationToken cancellationToken)
        //{
        //    var response = await _context.UserNotifications.Where(u => u.ApplicationUserId.Equals(userId) && !u.IsRead)
        //                                           .Include(n => n.Notification).ToListAsync();
        //    return response;
        //}
        public async Task<NotificationApplicationUser> GetNotification(Guid notificationId, string userId)
        {
            var notification = await _context.UserNotifications
                                      .FirstOrDefaultAsync(n => n.ApplicationUserId.Equals(userId) && !n.IsRead
                                      && n.NotificationId == notificationId);
            return notification;
        }

        public bool ReadNotification(NotificationApplicationUser notification)
        {

            notification.IsRead = true;
            base.Update(notification);


            _hubContext.Clients.All.SendAsync("displayNotification");

            return notification.IsRead;
        }
        public async Task AssignNotificationToTheUsers(Notification notification, List<Guid> followingUsers, CancellationToken cancellationToken)
        {
            //TODO:Aassign notification to the user 

            if (followingUsers != null)
            {
                foreach (var following in followingUsers)
                {
                    var userNotification = new NotificationApplicationUser();
                    userNotification.ApplicationUserId = following.ToString();
                    userNotification.NotificationId = notification.NotificationId;

                    await base.InsertAsync(userNotification,cancellationToken);
                   
                }
            }



        }

        public async Task AssignNotificationToTheUser(Notification notification, string followedId, CancellationToken cancellationToken)
        {
            var userNotification = new NotificationApplicationUser();
            userNotification.ApplicationUserId = followedId;
            userNotification.NotificationId = notification.NotificationId;

            await _context.UserNotifications.AddAsync(userNotification);
            //await _context.SaveChangesAsync();
        }

    }
}
