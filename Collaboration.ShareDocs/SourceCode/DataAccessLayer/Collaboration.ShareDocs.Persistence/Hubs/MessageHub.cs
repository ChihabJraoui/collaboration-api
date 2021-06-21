using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Persistence.Hubs
{
    public class MessageHub:Hub
    {
        public Task SendMessageToAll(string message)
        {
            return Clients.All.SendAsync("ReciveMessage", message);
        }
    }
}
