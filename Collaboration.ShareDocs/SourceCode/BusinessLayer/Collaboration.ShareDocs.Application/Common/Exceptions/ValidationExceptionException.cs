using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace Collaboration.ShareDocs.Application.Common.Exceptions
{
    public class ValidationExceptionException : Exception
    {
        public ValidationExceptionException()
            : base("One or more validation failures have occurred.")
        {
            Errors = new List<Error>();
        }

        public ValidationExceptionException(List<ValidationFailure> failures)
            : this()
        {

            Errors.AddRange(failures.Select(e => new Error(e.PropertyName, e.ErrorMessage)));
        }
        public List<Error> Errors { get; set; }

    }
}
