using Threax.AspNetCore.ExceptionFilter;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Microsoft.Extensions.DependencyInjection
{
    public class ExceptionFilterOptions
    {
        public bool DetailedErrors { get; set; } = false;
    }

    public static class ExceptionFilterExtensions
    {
        public static IServiceCollection AddExceptionErrorFilters(this IServiceCollection services, ExceptionFilterOptions exFilterOptions)
        {
            services.AddSingleton<ExceptionToActionResultFilterAttribute>(s =>
            {
                return new ExceptionToActionResultFilterAttribute(exFilterOptions.DetailedErrors, s.GetRequiredService<ILogger<ExceptionToActionResultFilterAttribute>>());
            });

            return services;
        }

        public static MvcOptions UseExceptionErrorFilters(this MvcOptions options)
        {
            options.Filters.Add(new ServiceFilterAttribute(typeof(ExceptionToActionResultFilterAttribute)));
            return options;
        }
    }
}
