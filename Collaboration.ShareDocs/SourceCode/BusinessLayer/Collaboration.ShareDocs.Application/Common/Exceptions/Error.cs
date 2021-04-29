
namespace Collaboration.ShareDocs.Application.Common.Exceptions
{
    public class Error
    {
        public Error()
        {
        }

        public Error(string name, string message)
        {
            this.Name = name;
            this.Message = message;
        }

        public string Name { get; set; }

        public string Message { get; set; }
    }
}
