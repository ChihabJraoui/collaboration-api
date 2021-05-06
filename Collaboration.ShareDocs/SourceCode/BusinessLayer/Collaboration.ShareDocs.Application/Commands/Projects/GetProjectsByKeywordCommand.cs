﻿using AutoMapper;
using Collaboration.ShareDocs.Application.Common.Response;
using Collaboration.ShareDocs.Persistence.Interfaces;
using Collaboration.ShareDocs.Resources;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Application.Commands.Projects
{
    public class GetProjectsByKeywordCommand:IRequest<ApiResponseDetails>
    {
        public string Keyword { get; set; }

        public class Handler : IRequestHandler<GetProjectsByKeywordCommand, ApiResponseDetails>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                this._unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<ApiResponseDetails> Handle(GetProjectsByKeywordCommand request, CancellationToken cancellationToken)
            {
                if (request.Keyword == null)
                {
                    var message = string.Format(Resource.Error_NotValid, request);
                    return ApiCustomResponse.NotValid(request.Keyword, message, "");
                }
                var response = await _unitOfWork.ProjectRepository.GetByKeyWordAsync(request.Keyword, cancellationToken);
                if (response.Count == 0)
                {
                    var message = string.Format(Resource.Error_NotFound, request);
                    return ApiCustomResponse.NotFound(message);
                }
                return ApiCustomResponse.ReturnedObject(response);
            }
        }
    }
}