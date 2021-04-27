using System;
using System.Threading;
using System.Threading.Tasks;
using Collaboration.ShareDocs.Application.Common.Exceptions;
using Collaboration.ShareDocs.Persistence.Interfaces;
using MediatR;

namespace Collaboration.ShareDocs.Application.Project.Queries.GetProjectById
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
            
            //return await this._projectRepository.GetAsync(request);
            return null;
        }
    }
}
