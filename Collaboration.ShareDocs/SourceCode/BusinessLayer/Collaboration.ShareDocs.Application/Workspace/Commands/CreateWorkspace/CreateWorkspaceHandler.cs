﻿using Application.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Collaboration.ShareDocs.Application.Common.Exceptions;
using Collaboration.ShareDocs.Application.Common.Interfaces;
using Collaboration.ShareDocs.Persistence.Interfaces;

namespace Collaboration.ShareDocs.Application.Workspace.Commands.CreateWorkspace
{
    public class CreateWorkspaceHandler : IRequestHandler<CreateWorkspaceCommand, Guid>
    {
        private readonly IWorkspaceRepository _workspaceRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMethodesRepository _methodesRepository;

        public CreateWorkspaceHandler(IWorkspaceRepository workspaceRepository,
                                      ICurrentUserService currentUserService,
                                      IMethodesRepository methodesRepository)
        {
            this._workspaceRepository = workspaceRepository;
            this._currentUserService = currentUserService;
            this._methodesRepository = methodesRepository;
        }
        public async Task<Guid> Handle(CreateWorkspaceCommand request, CancellationToken cancellationToken)
        {

            // R01 Workspace label is unique
            if (!await _methodesRepository.UniqueName(request.Name, cancellationToken))
            {
                throw new BusinessRuleException($" The specified Name '{request.Name}' already exists.");
            }
            else
            {

                return Guid.NewGuid();
            }
        }
    }
}
