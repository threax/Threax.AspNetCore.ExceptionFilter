using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Threax.AspNetCore.ExceptionFilter
{
    /// <summary>
    /// This class is an error result with exception details.
    /// </summary>
    public class ExceptionResult : ErrorResult
    {
        private Exception ex;

        public ExceptionResult(Exception ex)
            :base(ex.Message)
        {
            this.ex = ex;
        }

        public String Type
        {
            get
            {
                return ex.GetType().FullName;
            }
        }

        public String StackTrace
        {
            get
            {
                return ex.StackTrace;
            }
        }
    }
}
