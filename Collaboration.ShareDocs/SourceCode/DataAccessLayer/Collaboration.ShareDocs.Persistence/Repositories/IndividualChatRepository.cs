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
    public class IndividualChatRepository : GenericRepository<IndividualChat>, IIndividualChatRepository
    {
        private readonly IHubContext<IndividualChatHub> _hubContext;

        public IndividualChatRepository(AppDbContext context,
            IHubContext<IndividualChatHub> hubContext,
            UserManager<ApplicationUser> userManager) : base(context)
        {
            _hubContext = hubContext;
        }
        public async Task Create(IndividualChat individualChat, CancellationToken cancellationToken)
        {
            await base.InsertAsync(individualChat, cancellationToken);
        }

        public async Task<List<IndividualChat>> GetChatAsync(Guid currentUserId, Guid userId)
        {
            var history= await dbSet.Where(u => (u.UserId == userId) && (u.From.Id == currentUserId)).OrderByDescending(w=>w.SentAt).ToListAsync();
            return history; 
        }
    }
}
