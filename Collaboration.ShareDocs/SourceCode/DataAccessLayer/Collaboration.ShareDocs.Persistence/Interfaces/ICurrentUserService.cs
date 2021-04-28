using System;
using System.Collections.Generic;
using System.Text;

namespace Collaboration.ShareDocs.Persistence.Interfaces
{
    public interface ICurrentUserService
    {
        string UserId { get; }
    }
}
