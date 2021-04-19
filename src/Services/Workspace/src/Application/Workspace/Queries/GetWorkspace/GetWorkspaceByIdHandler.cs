using Application.Common.Exceptions;
using Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Workspace.Queries.GetWorkspace
{
    public class GetWorkspaceByIdHandler : IRequestHandler<GetWorkspaceByIdQuery, Core.Entities.Workspace>
    {
        private readonly IWorkspaceRepository _workspaceRepository;

        public GetWorkspaceByIdHandler(IWorkspaceRepository workspaceRepository)
        {
            _workspaceRepository = workspaceRepository;
        }
        public async Task<Core.Entities.Workspace> Handle(GetWorkspaceByIdQuery request, CancellationToken cancellationToken)
        {
                var workspace = await _workspaceRepository.GetAsync(request);
                return workspace;
        }
    }
}
