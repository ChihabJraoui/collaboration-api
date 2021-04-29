using AutoMapper;
using Collaboration.ShareDocs.Application.Commands.Workspaces.Dto;
using Collaboration.ShareDocs.Application.Common.Exceptions;
using Collaboration.ShareDocs.Persistence.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Application.Commands.Workspaces
{
    public class UpdateWorkspaceCommand : IRequest<WorkspaceDto>
    {
        public Guid WorkspaceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public bool BookMark { get; set; }
        public bool IsPrivate { get; set; }

        public class Handler : IRequestHandler<UpdateWorkspaceCommand, WorkspaceDto>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ICurrentUserService _currentUserService;
            private readonly IMethodesRepository _methodesRepository;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork,
                           ICurrentUserService currentUserService,
                           IMethodesRepository methodesRepository,
                           IMapper mapper)
            {
                this._unitOfWork = unitOfWork;
                this._currentUserService = currentUserService;
                this._methodesRepository = methodesRepository;
                _mapper = mapper;
            }

            public async Task<WorkspaceDto> Handle(UpdateWorkspaceCommand request, CancellationToken cancellationToken)
            {
                // R01 Workspace label is unique
                if (!await _methodesRepository.UniqueName(request.Name, cancellationToken))
                {
                    throw new BusinessRuleException($" The specified Name '{request.Name}' already exists.");
                }
                var workspace = await _unitOfWork.WorkspaceRepository.GetAsync(request.WorkspaceId, cancellationToken);
                if (workspace == null)
                {
                    throw new BusinessRuleException($" There is no workspace with this '{request.WorkspaceId}'");
                }
                await _unitOfWork.WorkspaceRepository.UpdateAsync(workspace, cancellationToken);


                return null;
            }
        }
    }
}
