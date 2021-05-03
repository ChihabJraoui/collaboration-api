using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Collaboration.ShareDocs.Application.Commands.Workspaces.Validators
{
    public class UpdateWorkspaceHandlerValidator: AbstractValidator<UpdateWorkspaceCommand>
    {
        public UpdateWorkspaceHandlerValidator()
    {
        this.RuleFor(x => x.Name).NotEmpty().MinimumLength(3)
            .WithMessage("Name length should be at least three character");

    }
}
}
