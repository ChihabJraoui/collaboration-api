using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Workspace.Commands.DeleteWorkspace
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

            var dto = await _workspaceRepository.DeleteAsync(request, _currentUserService, cancellationToken);

            return dto;
        }
    }
}
