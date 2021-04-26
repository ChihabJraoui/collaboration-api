using Application.Common.Exceptions;
using Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Project.Queries.GetProjectByKeyWord
{
    public class GetProjectByKeyWordHandler : IRequestHandler<GetProjectByKeyWordQuery, ProjectsDTOLists>
    {
        private readonly IProjectRepository _pojectRepository;

        public GetProjectByKeyWordHandler(IProjectRepository pojectRepository)
        {
            _pojectRepository = pojectRepository;
        }
        public async Task<ProjectsDTOLists> Handle(GetProjectByKeyWordQuery request, CancellationToken cancellationToken)
        {
            //TODO Add some RE for Business Rules 
            var projects = await _pojectRepository.GetByKeyWordAsync(request);
            if(projects.Projects.Count==0)
            {
                throw new BusinessRuleException("Not Found ");
            }
            return projects;
        }
    }
}
