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
    public class NotificationRepository: GenericRepository<Notification>,INotificationRepository
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public NotificationRepository(AppDbContext context,
           
            IHubContext<NotificationHub> hubContext,
            UserManager<ApplicationUser> userManager) : base(context)

        {
            _context = context;
            _hubContext = hubContext;
            _userManager = userManager;
        }

        public async Task Create(Notification notification, Guid currentUserid,CancellationToken cancellationToken)
        {
            await base.InsertAsync(notification, cancellationToken);
            //TODO: save changes with unit of work
           
           
            //todo: add hub
            await _hubContext.Clients.All.SendAsync("displayNotification", cancellationToken);

        }

        


    }
}
