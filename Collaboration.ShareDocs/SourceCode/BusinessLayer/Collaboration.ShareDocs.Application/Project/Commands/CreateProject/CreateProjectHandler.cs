using Application.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Collaboration.ShareDocs.Application.Common.Exceptions;
using Collaboration.ShareDocs.Application.Common.Interfaces;
using Collaboration.ShareDocs.Application.Workspace.Queries.GetWorkspace;
using Collaboration.ShareDocs.Persistence.Interfaces;

namespace Collaboration.ShareDocs.Application.Project.Commands.CreateProject
{
    public class CreateProjectHandler : IRequestHandler<CreateProjectCommand, string>
    {
        private readonly IMethodesRepository _methodesRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IWorkspaceRepository _workspaceRepository;

        public CreateProjectHandler(IMethodesRepository methodesRepository,
                                    IProjectRepository projectRepository,
                                    ICurrentUserService currentUserService,
                                    IWorkspaceRepository workspaceRepository)
        {
            _methodesRepository = methodesRepository;
            _projectRepository = projectRepository;
            _currentUserService = currentUserService;
            _workspaceRepository = workspaceRepository;
        }
        public async Task<string> Handle(CreateProjectCommand command, CancellationToken cancellationToken)
        {
            var query = new GetWorkspaceByIdQuery { WorkspaceRequestId = command.WorkspaceId};
            var label = await _methodesRepository.UniqueName(command.Label, cancellationToken);
            //var workspaceExiste = await _workspaceRepository.GetAsync(query);
            //if (workspaceExiste == null)
            //{
            //    throw new BusinessRuleException("Workspace doesn't exist");
            //}
           
            //else if(!label)
            //{
            //    throw new BusinessRuleException("this new project is already exist");
                
            //}
            //else
            //{
            //    //var project = await _projectRepository.CreateAsync(command, workspaceExiste, _currentUserService);
            //    //return project;
            //}
            return null;

        }
    }
}
