using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Project.Commands.DeleteProject
{
    public class DeleteProjectHandler : IRequestHandler<DeleteProjectCommand, DeleteProjectDto>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ICurrentUserService _currentUserService;

        public DeleteProjectHandler( IProjectRepository projectRepository,ICurrentUserService currentUserService)
        {
            _projectRepository = projectRepository;
            _currentUserService = currentUserService;
        }
        public Task<DeleteProjectDto> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            if(request.ProjectId == Guid.Empty)
            {
                throw new BusinessRuleException($"this {request.ProjectId} is empty");
            }
            var result = _projectRepository.DeleteAsync(request, _currentUserService);
            return result;

            
        }
    }
}
