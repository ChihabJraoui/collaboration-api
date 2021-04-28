using AutoMapper;
using Collaboration.ShareDocs.Application.Commands.Workspaces.Dto;
using Collaboration.ShareDocs.Application.Common.Exceptions;
using Collaboration.ShareDocs.Persistence.Entities;
using Collaboration.ShareDocs.Persistence.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Application.Commands.Workspaces
{
    public class CreateWorkspaceCommand : IRequest<CreateWorkspaceDto>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }


        public class Handler : IRequestHandler<CreateWorkspaceCommand, CreateWorkspaceDto>
        {
            private readonly IWorkspaceRepository _workspaceRepository;
            private readonly ICurrentUserService _currentUserService;
            private readonly IMethodesRepository _methodesRepository;
            private readonly IMapper _mapper;

            public Handler(IWorkspaceRepository workspaceRepository,
                                          ICurrentUserService currentUserService,
                                          IMethodesRepository methodesRepository,
                                          IMapper mapper)
            {
                this._workspaceRepository = workspaceRepository;
                this._currentUserService = currentUserService;
                this._methodesRepository = methodesRepository;
                _mapper = mapper;
            }
            public async Task<CreateWorkspaceDto> Handle(CreateWorkspaceCommand request, CancellationToken cancellationToken)
            {

                // R01 Workspace label is unique
                if (!await _methodesRepository.UniqueName(request.Name, cancellationToken))
                {
                    throw new BusinessRuleException($" The specified Name '{request.Name}' already exists.");
                }
                else
                {
                    var newWorkspace = new Workspace
                    {
                        Name = request.Name,
                        Description = request.Description,
                        Image = request.Image,
                        CreatedBy = _currentUserService.UserId,
                    };

                    var workspace = await this._workspaceRepository.CreateAsync(newWorkspace, cancellationToken);
                    var response = _mapper.Map<CreateWorkspaceDto>(workspace);

                    return response;
                }
            }
        }
    }
}
