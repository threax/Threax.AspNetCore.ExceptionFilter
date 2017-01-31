using Threax.AspNetCore.ExceptionFilter;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ExceptionFilterExtensions
    {
        public static MvcOptions UseExceptionErrorFilters(this MvcOptions options, bool detailedErrors = false)
        {
            options.Filters.Add(new ExceptionToActionResultFilterAttribute(detailedErrors));
            return options;
        }
    }
}
