using Collaboration.ShareDocs.Persistence.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Collaboration.ShareDocs.Application.Common.Exceptions;

namespace Collaboration.ShareDocs.Application.Project.Queries.GetProjectByKeyWord
{
    public class GetProjectByKeyWordHandler : IRequestHandler<GetProjectByKeyWordQuery, ProjectsDtoLists>
    {
        private readonly IProjectRepository _pojectRepository;

        public GetProjectByKeyWordHandler(IProjectRepository pojectRepository)
        {
            _pojectRepository = pojectRepository;
        }
        public async Task<ProjectsDtoLists> Handle(GetProjectByKeyWordQuery request, CancellationToken cancellationToken)
        {
            //TODO Add some RE for Business Rules 
            var projects = await _pojectRepository.GetByKeyWordAsync(request.KeyWord);
            if(projects.Count==0)
            {
                throw new BusinessRuleException("Not Found ");
            }
            //return projects;
            return null;
        }
    }
}
