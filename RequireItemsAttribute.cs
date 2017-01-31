using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Threax.AspNetCore.ExceptionToJson
{
    /// <summary>
    /// This attribute marks that an item must contain items. Supports
    /// anything that extends ICollection.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class RequireItemsAttribute : RequiredAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public RequireItemsAttribute()
        {
            
        }

        /// <summary>
        /// True if the property is an ICollection that has items, otherwise false.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            var dictionary = value as ICollection;
            if(dictionary != null)
            {
                return dictionary.Count > 0;
            }
            return false;
        }
    }
}
