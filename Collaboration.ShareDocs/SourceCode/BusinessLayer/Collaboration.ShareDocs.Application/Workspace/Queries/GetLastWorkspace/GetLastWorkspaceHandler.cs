using Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Workspace.Queries.GetLastWorkspace
{
    public class GetLastWorkspaceHandler : IRequestHandler<GetLastWorkspaceQuery, LastWorkspaceDto>
    {
        private readonly IWorkspaceRepository _workspaceRepository;

        public GetLastWorkspaceHandler(IWorkspaceRepository workspaceRepository)
        {
            this._workspaceRepository = workspaceRepository;

        }
        public async Task<LastWorkspaceDto> Handle(GetLastWorkspaceQuery request, CancellationToken cancellationToken)
        {
            return await this._workspaceRepository.GetLastAsync(request, cancellationToken);
           
        }
    }
}
