using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Exceptions
{
    public class NotFoundException :Exception
    {
        //pour initialiser une nouvelle instances de la classe exception
        public NotFoundException()
            :base()
        {

        }
        //with a specific error message
        public NotFoundException(string message)
            :base(message)
        {

        }
        public NotFoundException(string message, Exception innerException)
            :base(message,innerException)
        {

        }

        public NotFoundException(string name, object key)
            :base($"Entity \"{name}\"({key}) was not found.")
        {

        }
    }
}
