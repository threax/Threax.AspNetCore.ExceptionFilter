using Threax.AspNetCore.ExceptionFilter;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;

namespace Microsoft.Extensions.DependencyInjection
{
    public class ExceptionFilterOptions
    {
        /// <summary>
        /// Set to true to return detailed errors.
        /// </summary>
        public bool DetailedErrors { get; set; } = false;

        /// <summary>
        /// Set to the naming strategy you want to use. Default is null or no strategy, which makes no changes.
        /// </summary>
        public NamingStrategy NamingStrategy { get; set; }
    }

    public static class ExceptionFilterExtensions
    {
        public static IServiceCollection AddExceptionErrorFilters(this IServiceCollection services, ExceptionFilterOptions exFilterOptions)
        {
            services.AddSingleton<ExceptionToActionResultFilterAttribute>(s =>
            {
                return new ExceptionToActionResultFilterAttribute(exFilterOptions.DetailedErrors, s.GetRequiredService<ILogger<ExceptionToActionResultFilterAttribute>>(), exFilterOptions.NamingStrategy);
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
