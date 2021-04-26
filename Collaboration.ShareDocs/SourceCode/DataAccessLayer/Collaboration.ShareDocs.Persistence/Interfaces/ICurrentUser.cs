using System;
using System.Collections.Generic;
using System.Text;

namespace Collaboration.ShareDocs.Persistence.Interfaces
{
    public interface ICurrentUser
    {
        public string Username { get; set; }
    }
}
