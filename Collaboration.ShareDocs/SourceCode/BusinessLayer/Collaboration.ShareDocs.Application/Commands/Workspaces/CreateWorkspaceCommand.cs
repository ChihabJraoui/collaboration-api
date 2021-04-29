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
                        Image = request.Image
                    };

                    var workspace = await _unitOfWork.WorkspaceRepository.CreateAsync(newWorkspace, cancellationToken);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);

                    var response = _mapper.Map<CreateWorkspaceDto>(workspace);
                    return response;
                }
            }
        }
    }
}
