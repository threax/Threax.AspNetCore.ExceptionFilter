using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public ModelStateErrorResult(ModelStateDictionary modelState, String message)
            :base(message)
        {
            this.Errors = new Dictionary<String, String>(modelState.ErrorCount);
            foreach (var item in modelState)
            {
                if (item.Value.ValidationState == ModelValidationState.Invalid)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var error in item.Value.Errors)
                    {
                        sb.AppendLine(error.ErrorMessage);
                    }
                    this.Errors[item.Key] = sb.ToString();
                }
            }
        }

        /// <summary>
        /// The collection of field specific error messages from the model state.
        /// </summary>
        public Dictionary<String, String> Errors { get; set; }
    }
}
