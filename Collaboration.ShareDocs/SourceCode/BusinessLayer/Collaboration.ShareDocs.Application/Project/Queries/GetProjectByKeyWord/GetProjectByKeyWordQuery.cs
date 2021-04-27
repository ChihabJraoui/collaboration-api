using MediatR;

namespace  Collaboration.ShareDocs.Application.Project.Queries.GetProjectByKeyWord
{
    public class GetProjectByKeyWordQuery:IRequest<ProjectsDtoLists>
    {
        public string KeyWord { get; set; }
    }
}
