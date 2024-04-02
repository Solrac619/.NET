
using System.Net;

namespace ApplicationCore.Exceptions
{

    public class BadRequesException : CustomException
    {
        public BadRequesException(string message)
            : base(message, null, HttpStatusCode.BadRequest)
        {
        }
    }
}
