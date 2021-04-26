using Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Workspace.Queries.GetAllWorkspaces
{
    public class GetAllWorkspacesQueryHandler : IRequestHandler<GetAllWorkspaceQuery, WorkspacesDTOLists>
    {
        private readonly IWorkspaceRepository _workspaceRepository;

        public GetAllWorkspacesQueryHandler(IWorkspaceRepository workspaceRepository)
        {
            _workspaceRepository = workspaceRepository;
        }
       
        public async Task<WorkspacesDTOLists> Handle(GetAllWorkspaceQuery request, CancellationToken cancellationToken)
        {
            var workspaceLists = await _workspaceRepository.GetAllAsync(request, cancellationToken);
            return workspaceLists;
        }
    }
}
