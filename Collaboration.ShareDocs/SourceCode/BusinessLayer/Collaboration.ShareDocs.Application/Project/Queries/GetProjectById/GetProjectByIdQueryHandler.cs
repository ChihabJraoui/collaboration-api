using Application.Common.Exceptions;
using Application.Repositories;
using Collaboration.ShareDocs.Persistence.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Project.Queries.GetProjectById
{
    public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, Collaboration.ShareDocs.Persistence.Entities.Project>
    {
        private readonly IProjectRepository _projectRepository;

        public GetProjectByIdQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }


        public async Task<Collaboration.ShareDocs.Persistence.Entities.Project> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
            {
                throw new BusinessRuleException("Project id is requered");
            }
            
            return await this._projectRepository.GetAsync(request);
        }
    }
}
