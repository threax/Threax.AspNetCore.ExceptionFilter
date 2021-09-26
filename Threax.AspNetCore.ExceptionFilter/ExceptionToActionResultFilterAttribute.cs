﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Threax.AspNetCore.ExceptionFilter
{
    /// <summary>
    /// This filter checks for the exceptions thrown by the attributes defined in this system
    /// and converts them to the appropriate json result.
    /// </summary>
    public class ExceptionToActionResultFilterAttribute : ExceptionFilterAttribute
    {
        private bool detailedInternalServerError;
        private ILogger<ExceptionToActionResultFilterAttribute> logger;
        private readonly NamingStrategy namingStrategy;

        /// <summary>
        /// Constructor. Takes a bool to show detailed Internal Server Error (500) exceptions or not.
        /// If these are detailed the exception will be serialized to json, if not just 500 is returned.
        /// You should only use detailed exceptions for development otherwise you could leak internal
        /// implementation details.
        /// </summary>
        /// <param name="detailedInternalServerError"></param>
        /// <param name="logger"></param>
        /// <param name="namingStrategy">The naming strategy to use. Can be null.</param>
        public ExceptionToActionResultFilterAttribute(bool detailedInternalServerError, ILogger<ExceptionToActionResultFilterAttribute> logger, NamingStrategy namingStrategy)
        {
            this.detailedInternalServerError = detailedInternalServerError;
            this.logger = logger;
            this.namingStrategy = namingStrategy;
        }

        public override void OnException(ExceptionContext context)
        {
            logger.LogError(context.Exception, $"Exception {context.Exception.GetType().Name} occured in exception filter.\nMessage: {context.Exception.Message}");

            //Validation exception becomes a Bad Request (400) and gets a ModelState simplified and serialized to json.
            var validationException = context.Exception as ValidationException;
            if (validationException != null)
            {
                context.Result = new ObjectResult(new ModelStateErrorResult(context.ModelState, validationException.Message, namingStrategy))
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };

                return;
            }

            //ExceptionErrorResult becomes a ErrorResult with the given status code
            var exceptionErrorResult = context.Exception as ErrorResultException;
            if(exceptionErrorResult != null)
            {
                context.Result = new ObjectResult(new ErrorResult(exceptionErrorResult.Message))
                {
                    StatusCode = (int)exceptionErrorResult.StatusCode
                };

                return;
            }

            //File not found becomes a Not Found (404).
            var fileNotFoundException = context.Exception as FileNotFoundException;
            if(fileNotFoundException != null)
            {
                context.Result = new StatusCodeResult((int)HttpStatusCode.NotFound);

                return;
            }

            if (detailedInternalServerError)
            {
                context.Result = new ObjectResult(new ExceptionResult(context.Exception))
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
            else
            {
                context.Result = new ObjectResult(new ErrorResult("Internal Server Error"))
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
