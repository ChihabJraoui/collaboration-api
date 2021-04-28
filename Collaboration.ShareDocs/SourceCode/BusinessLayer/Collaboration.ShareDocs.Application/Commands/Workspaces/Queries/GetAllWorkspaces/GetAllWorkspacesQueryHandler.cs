using Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Collaboration.ShareDocs.Persistence.Interfaces;

namespace Collaboration.ShareDocs.Application.Workspace.Queries.GetAllWorkspaces
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
            //var workspaceLists = await _workspaceRepository.GetAllAsync(request, cancellationToken);
            //return workspaceLists;
            return null;
        }
    }
}
