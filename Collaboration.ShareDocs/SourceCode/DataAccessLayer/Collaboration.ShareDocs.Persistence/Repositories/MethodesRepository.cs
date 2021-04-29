using Collaboration.ShareDocs.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Persistence.Repositories
{
    public class MethodesRepository : IMethodesRepository
    {
        public Task<bool> UniqueName(string name, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
