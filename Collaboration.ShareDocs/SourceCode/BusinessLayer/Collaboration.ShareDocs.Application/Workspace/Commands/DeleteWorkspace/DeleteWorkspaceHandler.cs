using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Workspace.Commands.DeleteWorkspace;
using Collaboration.ShareDocs.Application.Common.Exceptions;
using Collaboration.ShareDocs.Application.Common.Interfaces;
using Collaboration.ShareDocs.Persistence.Interfaces;
using MediatR;

namespace Collaboration.ShareDocs.Application.Workspace.Commands.DeleteWorkspace
{
    public class DeleteWorkspaceHandler : IRequestHandler<DeleteWorkspaceCommad,DeleteWorkspaceReturnDto>
    {
        private readonly IWorkspaceRepository _workspaceRepository;
        private readonly ICurrentUserService _currentUserService;

        public DeleteWorkspaceHandler(IWorkspaceRepository workspaceRepository,ICurrentUserService currentUserService)
        {
            _workspaceRepository = workspaceRepository;
            _currentUserService = currentUserService;
        }
        public async Task<DeleteWorkspaceReturnDto> Handle(DeleteWorkspaceCommad request, CancellationToken cancellationToken)
        {
            
            if(request.WorkspaceId == Guid.Empty)
            {
                throw new BusinessRuleException($"Workspace {request.WorkspaceId} doesn't exist");
            }

            //var dto = await _workspaceRepository.DeleteAsync(request.WorkspaceId, _currentUserService, cancellationToken);

            //return dto;
            return null;
        }
    }
}
