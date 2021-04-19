using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Workspace.Queries.GetLastWorkspace
{
    public class GetLastWorkspaceQuery:IRequest<LastWorkspaceDto>
    {
    }
}
