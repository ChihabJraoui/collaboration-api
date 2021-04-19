using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Workspace.Queries.GetWorkspaceByKeyWord
{
    public class GetWorkspaceByKeyWord:IRequest<WorkspaceDtoLists>
    {
        public string KeyWord { get; set; }
    }
}
