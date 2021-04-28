using MediatR;

namespace Collaboration.ShareDocs.Application.Workspace.Queries.GetWorkspaceByKeyWord
{
    public class GetWorkspaceByKeyWord:IRequest<WorkspaceDtoLists>
    {
        public string KeyWord { get; set; }
    }
}
