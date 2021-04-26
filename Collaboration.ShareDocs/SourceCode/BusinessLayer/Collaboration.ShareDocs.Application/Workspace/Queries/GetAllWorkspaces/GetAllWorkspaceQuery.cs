using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Workspace.Queries.GetAllWorkspaces
{
    public class GetAllWorkspaceQuery: IRequest<WorkspacesDTOLists>
    {
    }
}
