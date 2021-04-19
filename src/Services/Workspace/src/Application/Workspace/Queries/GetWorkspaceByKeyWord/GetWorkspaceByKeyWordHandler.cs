using Application.Common.Exceptions;
using Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Workspace.Queries.GetWorkspaceByKeyWord
{
    public class GetWorkspaceByKeyWordHandler : IRequestHandler<GetWorkspaceByKeyWord, WorkspaceDtoLists>
    {
        private readonly IWorkspaceRepository _workspaceRepository;

        public GetWorkspaceByKeyWordHandler(IWorkspaceRepository workspaceRepository)
        {
            _workspaceRepository = workspaceRepository;
        }
        public async Task<WorkspaceDtoLists> Handle(GetWorkspaceByKeyWord request, CancellationToken cancellationToken)
        {
            var workspaces = await _workspaceRepository.GetByKeyWord(request);
            if (workspaces.Workspaces.Count == 0)
            {
                throw new BusinessRuleException("Not Found ");
            }
            return workspaces;
        }
    }
}
