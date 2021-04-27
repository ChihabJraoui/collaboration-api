using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Collaboration.ShareDocs.Application.Common.Exceptions;
using Collaboration.ShareDocs.Persistence.Interfaces;

namespace Collaboration.ShareDocs.Application.Workspace.Queries.GetWorkspaceByKeyWord
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
            //var workspaces = await _workspaceRepository.GetByKeyWord(request.KeyWord);
            //if (workspaces.Count == 0)
            //{
            //    throw new BusinessRuleException("Not Found ");
            //}
            //return workspaces;
            return null;
        }
    }
}
