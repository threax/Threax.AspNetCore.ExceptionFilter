using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Threax.AspNetCore.ExceptionFilter
{
    /// <summary>
    /// A class that returns an error result with a message. Subclassed to be more interesting.
    /// </summary>
    public class ErrorResult
    {
        public ErrorResult(String message)
        {
            this.Message = message;
        }

        /// <summary>
        /// The overall error message
        /// </summary>
        public String Message { get; set; }
    }
}
