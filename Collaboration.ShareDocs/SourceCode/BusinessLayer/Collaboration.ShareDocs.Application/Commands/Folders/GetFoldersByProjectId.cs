using AutoMapper;
using Collaboration.ShareDocs.Application.Commands.Folders.Dto;
using Collaboration.ShareDocs.Application.Common.Response;
using Collaboration.ShareDocs.Persistence.Interfaces;
using Collaboration.ShareDocs.Resources;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Application.Commands.Folders
{
    public class GetFoldersByProjectId:IRequest<ApiResponseDetails>
    {
        public Guid ProjectId { get; set; }

        public class Handler : IRequestHandler<GetFoldersByProjectId, ApiResponseDetails>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                this._unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<ApiResponseDetails> Handle(GetFoldersByProjectId request, CancellationToken cancellationToken)
            {
                var project = await _unitOfWork.ProjectRepository.GetAsync(request.ProjectId, cancellationToken);
                if(project == null)
                {
                    var message = string.Format(Resource.Error_NotFound, request);
                    return ApiCustomResponse.NotFound(message);
                }
                var folders = await _unitOfWork.FolderRepository.GetByProjectIdAsync(request.ProjectId,cancellationToken);
                var response = _mapper.Map<List<FolderDto>>(folders);
                return ApiCustomResponse.ReturnedObject(response);

            }
        }
    }
}
