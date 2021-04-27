using System.Threading;
using System.Threading.Tasks;
using Application.Repositories;
using Collaboration.ShareDocs.Application.Common.Interfaces;
using Collaboration.ShareDocs.Persistence.Interfaces;
using MediatR;

namespace Collaboration.ShareDocs.Application.Project.Commands.AddMemberToProject
{
    public class AddMemberToProjectHandler :IRequestHandler<AddMemberToProject , Unit>
    {
        private readonly IMethodesRepository _methodesRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IWorkspaceRepository _workspaceRepository;
        public AddMemberToProjectHandler(IMethodesRepository methodesRepository,
                                    IProjectRepository projectRepository,
                                    ICurrentUserService currentUserService,
                                    IWorkspaceRepository workspaceRepository)
        {
            _methodesRepository = methodesRepository;
            _projectRepository = projectRepository;
            _currentUserService = currentUserService;
            _workspaceRepository = workspaceRepository;
            

        }

        public async Task<Unit> Handle(AddMemberToProject request, CancellationToken cancellationToken)
        {
           // await _projectRepository.AddMembersToProject(request.MemberID, request.ProjectID);
            return Unit.Value;
        }
    }
}
