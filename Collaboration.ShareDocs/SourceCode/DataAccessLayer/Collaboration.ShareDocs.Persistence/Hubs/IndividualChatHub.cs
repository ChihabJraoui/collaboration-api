using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Persistence.Hubs
{
    public class IndividualChatHub : Hub
    {

        public async Task SendMessage(IndividualChatHub message)
        => await Clients.All.SendAsync("Recive Messsage", message);
    }
}
