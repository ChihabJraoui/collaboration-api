using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Collaboration.ShareDocs.Persistence.Interfaces;

namespace Collaboration.ShareDocs.Application.Workspace.Queries.GetWorkspace
{
    public class GetWorkspaceByIdHandler : IRequestHandler<GetWorkspaceByIdQuery, Persistence.Entities.Workspace>
    {
        private readonly IWorkspaceRepository _workspaceRepository;

        public GetWorkspaceByIdHandler(IWorkspaceRepository workspaceRepository)
        {
            _workspaceRepository = workspaceRepository;
        }
        public async Task<Persistence.Entities.Workspace> Handle(GetWorkspaceByIdQuery request, CancellationToken cancellationToken)
        {
                //var workspace = await _workspaceRepository.GetAsync(request);
                //return workspace;
                return null;
        }
    }
}
