using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Threax.AspNetCore.ExceptionToJson
{
    /// <summary>
    /// This class is an error result with exception details.
    /// </summary>
    public class ExceptionResult : ErrorResult
    {
        public ExceptionResult(Exception ex)
            :base(ex.Message)
        {
            this.Exception = ex;
        }

        public Exception Exception { get; set; }
    }
}
