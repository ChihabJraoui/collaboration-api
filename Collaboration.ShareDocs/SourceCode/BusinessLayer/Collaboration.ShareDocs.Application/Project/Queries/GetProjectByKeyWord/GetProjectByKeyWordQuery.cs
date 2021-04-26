using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Project.Queries.GetProjectByKeyWord
{
    public class GetProjectByKeyWordQuery:IRequest<ProjectsDTOLists>
    {
        public string KeyWord { get; set; }
    }
}
