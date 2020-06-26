using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace Threax.AspNetCore.ExceptionFilter
{
    /// <summary>
    /// This class represents an error result. It is mostly used to provide a strongly
    /// typed error class that gets serialized to json. It contains an overall error message
    /// and specific error messages for each field.
    /// </summary>
    public class ModelStateErrorResult : ErrorResult
    {
        /// <summary>
        /// Constructor, takes the model state and an overall error message.
        /// </summary>
        /// <param name="modelState">The model state.</param>
        /// <param name="message">An error message that applies to the entire result.</param>
        /// <param name="namingStrategy">The naming strategy to use. Can be null, which makes no changes.</param>
        public ModelStateErrorResult(ModelStateDictionary modelState, String message, NamingStrategy namingStrategy)
            :base(message)
        {
            var keySb = new StringBuilder();
            var errorSb = new StringBuilder();
            this.Errors = new Dictionary<String, String>(modelState.ErrorCount);
            foreach (var item in modelState)
            {
                if (item.Value.ValidationState == ModelValidationState.Invalid)
                {
                    errorSb.Clear();
                    foreach (var error in item.Value.Errors)
                    {
                        errorSb.AppendLine(error.ErrorMessage);
                    }

                    var key = item.Key;
                    if(namingStrategy != null && key.Contains('.'))
                    {
                        keySb.Clear();
                        var keyParts = key.Split('.');
                        foreach(var keyPart in keyParts)
                        {
                            keySb.Append(namingStrategy.GetPropertyName(keyPart, false));
                            keySb.Append('.');
                        }
                        key = keySb.ToString(0, keySb.Length - 1);
                    }

                    this.Errors[key] = errorSb.ToString();
                }
            }
        }

        /// <summary>
        /// The collection of field specific error messages from the model state.
        /// </summary>
        public Dictionary<String, String> Errors { get; set; }
    }
}
