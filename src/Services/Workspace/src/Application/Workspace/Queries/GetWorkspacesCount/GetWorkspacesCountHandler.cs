using Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Workspace.Queries.GetWorkspacesCount
{
    public class GetWorkspacesCountHandler : IRequestHandler<GetWorkspacesCountQuery, int>
    {
        private readonly IWorkspaceRepository _workspaceRepository;

        public GetWorkspacesCountHandler(IWorkspaceRepository workspaceRepository)
        {
            this._workspaceRepository = workspaceRepository;
        }
        public async Task<int> Handle(GetWorkspacesCountQuery request, CancellationToken cancellationToken)
        {
            return await _workspaceRepository.GetCount(request);
        }
    }
}
