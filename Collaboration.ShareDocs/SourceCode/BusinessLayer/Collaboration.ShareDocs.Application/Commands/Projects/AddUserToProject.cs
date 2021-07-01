using AutoMapper;
using Collaboration.ShareDocs.Application.Commands.Projects.Dto;
using Collaboration.ShareDocs.Application.Common.Response;
using Collaboration.ShareDocs.Persistence.Entities;
using Collaboration.ShareDocs.Persistence.Interfaces;
using Collaboration.ShareDocs.Resources;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Application.Commands.Projects
{
    public class AddUserToProject : IRequest<ApiResponseDetails>
    {
        public Guid[] UsersId { get; set; }
        public Guid ProjectId { get; set; }

        public class Handler : IRequestHandler<AddUserToProject, ApiResponseDetails>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ICurrentUserService _currentUserService;

            private readonly IMapper _mapper;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IUserProjectRepository _userProjectRepository;

            public Handler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService,IUserProjectRepository userProjectRepository,
                                         IMapper mapper, UserManager<ApplicationUser> userManager)
            {
                this._unitOfWork = unitOfWork;
                this._currentUserService = currentUserService;
                _mapper = mapper;
                _userManager = userManager;
                _userProjectRepository = userProjectRepository;
            }

            public async Task<ApiResponseDetails> Handle(AddUserToProject request, CancellationToken cancellationToken)
            {
                var users = new List<ApplicationUser>();
                var project = await _unitOfWork.ProjectRepository.GetAsync(request.ProjectId, cancellationToken);
                if (project == null)
                {
                    var message = string.Format(Resource.Error_NotFound, project,request.ProjectId);
                    return ApiCustomResponse.NotFound(message);
                }
                foreach (var userId in request.UsersId)
                {
                    var userDb = await this._userManager.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();

                    //var excestingUser = await _unitOfWork.UserProjectRepository.UserProject(userDb, project, cancellationToken);
                    //if (excestingUser != null)
                    //{
                        
                    //    var message = string.Format(Resource.Error_IsAleradyExist);
                        
                    //    return  ApiCustomResponse.ExestingError(message);
                        
                    //}
                
                    if (userDb == null)
                    {
                        var message = string.Format(Resource.Error_NotFound, userDb,userId);
                        return ApiCustomResponse.NotFound(message);
                    }
                   
                    await _unitOfWork.UserProjectRepository.AddMemberToProject(userDb, project, cancellationToken);

                    await _unitOfWork.SaveChangesAsync();
                  
                }


                return ApiCustomResponse.ReturnedObject();

            }
        }
    }
}
