using Collaboration.ShareDocs.Persistence.Entities;
using Collaboration.ShareDocs.Persistence.Hubs;
using Collaboration.ShareDocs.Persistence.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
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
    }
}
