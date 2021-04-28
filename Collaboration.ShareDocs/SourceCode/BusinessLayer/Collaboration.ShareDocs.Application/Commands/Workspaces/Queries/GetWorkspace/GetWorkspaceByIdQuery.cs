using System;
using MediatR;

namespace Collaboration.ShareDocs.Application.Workspace.Queries.GetWorkspace
{
    public class GetWorkspaceByIdQuery:IRequest<Persistence.Entities.Workspace>
    {
        public Guid WorkspaceRequestId { get; set; }

    }
}
 