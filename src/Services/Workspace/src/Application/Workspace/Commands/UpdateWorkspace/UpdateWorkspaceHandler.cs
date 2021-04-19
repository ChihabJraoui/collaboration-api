using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Repositories;
using Application.Workspace.Queries.GetWorkspace;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Workspace.Commands.UpdateWorkspace
{
    public class UpdateWorkspaceHandler : IRequestHandler<UpdateWorkspaceCommand,Core.Entities.Workspace>
    {
        private readonly IWorkspaceRepository _workspaceRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMethodesRepository _methodesRopository;

        public UpdateWorkspaceHandler(IWorkspaceRepository workspaceRepository,IMethodesRepository methodesRepository,ICurrentUserService currentUserService)
        {
            _workspaceRepository = workspaceRepository;
            _currentUserService = currentUserService;
            _methodesRopository = methodesRepository;
        }
        public async Task<Core.Entities.Workspace> Handle(UpdateWorkspaceCommand request, CancellationToken cancellationToken)
        {
            var query = new GetWorkspaceByIdQuery
            {
                WorkspaceRequestId=request.WorkspaceId
            };
            if (request.WorkspaceId.Equals(Guid.Empty))
            {
                throw new BusinessRuleException($"this workspace Id {request.WorkspaceId} is empty");
            }
            else if (!await _methodesRopository.UniqueName(request.Name, cancellationToken))
            {
                throw new BusinessRuleException($"Name of workspace {request.Name} is already exist");
            }
            else
            {
                var isExsit = await this._workspaceRepository.GetAsync(query);
                if (isExsit != null)
                {
                    var result = await this._workspaceRepository.UpdataAsync(request, _currentUserService, cancellationToken);
                    return result;
                }
                else
                {
                    throw new BusinessRuleException($"The workspace id is doesn't exist in your database");
                }

            }
            
        }
    }
}
