using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Workspace.Commands.CreateWorkspace
{
    public class CreateWorkspaceHandlerValidator:AbstractValidator<CreateWorkspaceCommand>
    {
        public CreateWorkspaceHandlerValidator()
        {
            this.RuleFor(x => x.Name).NotEmpty().MinimumLength(3)
                .WithMessage("Name length should be at least three character");

        }
    }
}
