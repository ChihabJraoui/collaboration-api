using Collaboration.ShareDocs.Persistence.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Collaboration.ShareDocs.Application.Common.Exceptions;
using Collaboration.ShareDocs.Application.Common.Interfaces;

namespace Collaboration.ShareDocs.Application.Project.Commands.DeleteProject
{

    public class DeleteProjectCommand : IRequest<DeleteProjectDto>
    {
        public Guid ProjectId { get; set; } 

        public class Handler : IRequestHandler<DeleteProjectCommand, DeleteProjectDto>
        {
            private readonly IProjectRepository _projectRepository;
            private readonly ICurrentUserService _currentUserService;

            public Handler(IProjectRepository projectRepository, ICurrentUserService currentUserService)
            {
                _projectRepository = projectRepository;
                _currentUserService = currentUserService;
            }

            public Task<DeleteProjectDto> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
            {
                if (request.ProjectId == Guid.Empty)
                {
                    throw new BusinessRuleException($"this {request.ProjectId} is empty");
                }
                //var result = _projectRepository.DeleteAsync(request.ProjectId, _currentUserService,cancellationToken);

                //return result;
                return null;
            }
        }
    }
}
