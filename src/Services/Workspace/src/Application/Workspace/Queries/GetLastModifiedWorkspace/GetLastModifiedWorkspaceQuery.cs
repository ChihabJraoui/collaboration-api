using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Workspace.Queries.GetLastModifiedWorkspace
{
    public class GetLastModifiedWorkspaceQuery:IRequest<WorkspaceLastModifiedDto>
    {
    }
}
