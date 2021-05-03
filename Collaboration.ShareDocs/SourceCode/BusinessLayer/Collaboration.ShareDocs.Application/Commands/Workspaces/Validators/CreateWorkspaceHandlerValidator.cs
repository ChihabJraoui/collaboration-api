using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Collaboration.ShareDocs.Application.Commands.Workspaces.Validators
{
    public class CreateWorkspaceHandlerValidator : AbstractValidator<CreateWorkspaceCommand>
    {
        public CreateWorkspaceHandlerValidator()
        {
            this.RuleFor(x => x.Name).NotEmpty().MinimumLength(3)
                .WithMessage("Name length rrrrrrrrrrrrrrrrrrr be at least three character");

        }
    }
}
