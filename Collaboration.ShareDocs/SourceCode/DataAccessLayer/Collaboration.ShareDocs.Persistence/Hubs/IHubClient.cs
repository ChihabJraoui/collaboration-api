using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Persistence.Hubs
{
    public interface IHubClient
    {
        Task BroadcastMessage();
    }
}
