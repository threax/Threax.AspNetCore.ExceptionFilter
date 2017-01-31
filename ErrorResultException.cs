using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Threax.AspNetCore.ExceptionToJson
{
    public class ErrorResultException : Exception
    {
        public ErrorResultException(String message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
            :base(message)
        {
            this.StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; set; }
    }
}
