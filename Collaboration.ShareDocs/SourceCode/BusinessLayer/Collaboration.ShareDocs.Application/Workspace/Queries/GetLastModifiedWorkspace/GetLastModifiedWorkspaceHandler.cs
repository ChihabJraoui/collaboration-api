using Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Workspace.Queries.GetLastModifiedWorkspace
{

    public class GetLastModifiedWorkspaceHandler : IRequestHandler<GetLastModifiedWorkspaceQuery, WorkspaceLastModifiedDto>
    {
        private readonly IWorkspaceRepository _workspaceRepository;

        public GetLastModifiedWorkspaceHandler(IWorkspaceRepository workspaceRepository)
        {
            this._workspaceRepository = workspaceRepository;
        }
        public Task<WorkspaceLastModifiedDto> Handle(GetLastModifiedWorkspaceQuery request, CancellationToken cancellationToken)
        {
            return this._workspaceRepository.GetLastModifiedAsync(request, cancellationToken);
        }
    }
}
