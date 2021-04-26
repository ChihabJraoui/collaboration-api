using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Folder.Commands.DeleteFolder
{
    public class DeleteFolderCommandValidator: AbstractValidator<DeleteFolderCommand>
    {
        public DeleteFolderCommandValidator()
        {
            this.RuleFor(x => x.FolderId).NotEmpty()
               .WithMessage("Id is required");
        }
    }
}
