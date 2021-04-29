using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Collaboration.ShareDocs.Application.Workspace.Queries.GetWorkspaceByKeyWord
{
    
    public class GetWorkspaceByKeyWordValidator:AbstractValidator<GetWorkspaceByKeyWord>
    {
        public GetWorkspaceByKeyWordValidator()
        {
            this.RuleFor(x => x.KeyWord).NotEmpty().MinimumLength(2).WithMessage("Is required");

        }
    }
}
