using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Threax.AspNetCore.ExceptionToJson
{
    /// <summary>
    /// This exception is used to handle validation errors.
    /// </summary>
    public class ValidationException : Exception
    {
        public ValidationException(String message)
            : base(message)
        {

        }
    }
}
