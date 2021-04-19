using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Workspace.Queries.GetWorkspace
{
    public class GetWorkspaceByIdQuery:IRequest<Core.Entities.Workspace>
    {
        public Guid WorkspaceRequestId { get; set; }

    }
}
 