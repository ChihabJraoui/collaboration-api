using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Repositories;
using Collaboration.ShareDocs.Application.Common.Exceptions;
using Collaboration.ShareDocs.Application.Common.Interfaces;
using Collaboration.ShareDocs.Application.Project.Queries.GetProjectById;
using Collaboration.ShareDocs.Persistence.Interfaces;
using MediatR;

namespace Collaboration.ShareDocs.Application.Project.Commands.UpdateProject
{
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMethodesRepository _methodesRepository;

        public  UpdateProjectCommandHandler(IProjectRepository projectRepository,IMethodesRepository methodesRepository, ICurrentUserService currentUserService)
        {
            _projectRepository = projectRepository;
            _currentUserService = currentUserService;
            _methodesRepository = methodesRepository;
        }
        public async Task<Unit> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            if (request.ProjectId == Guid.Empty)
            {
                throw new BusinessRuleException("The project doesn't exist");
            }
            var getEntity = new GetProjectByIdQuery
            {
                Id = request.ProjectId,
            };
            //var project =await _projectRepository.GetAsync(getEntity);
            //var exist = await _methodesRepository.UniqueName(request.Label, cancellationToken);
            //if (!exist)
            //{
            //    throw new BusinessRuleException("is alredy exist");

            //}
            //await _projectRepository.UpdateAsync(request,project, _currentUserService);

            return Unit.Value;
        }
    }
}
