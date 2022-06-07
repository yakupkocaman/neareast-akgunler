using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Akgunler.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Akgunler.Extensions
{
    public static class ModelStateHelper
    {
        public static IActionResult ErrorResult(this ModelStateDictionary modelState, string message)
        {
            if(!string.IsNullOrEmpty(message))
            {
                modelState.AddModelError("", message);
            }

            var result = new List<Error>();
            var erroneousFields = modelState.Where(ms => ms.Value.Errors.Any())
                                            .Select(x => new { x.Key, x.Value.Errors });

            foreach (var erroneousField in erroneousFields)
            {
                var fieldKey = erroneousField.Key;
                var fieldErrors = erroneousField.Errors.Select(error => new Error(fieldKey, error.ErrorMessage));
                result.AddRange(fieldErrors);
            }

            return new BadRequestObjectResult(result);

        }
        public static IEnumerable<Error> Errors(this ModelStateDictionary modelState)
        {
            //if (!modelState.IsValid)
            //{
            //    return modelState.ToDictionary(kvp => kvp.Key,
            //        kvp => kvp.Value.Errors
            //                        .Select(e => e.ErrorMessage).ToArray())
            //                        .Where(m => m.Value.Count() > 0);

            //}

            var result = new List<Error>();
            var erroneousFields = modelState.Where(ms => ms.Value.Errors.Any())
                                            .Select(x => new { x.Key, x.Value.Errors });

            foreach (var erroneousField in erroneousFields)
            {
                var fieldKey = erroneousField.Key;
                var fieldErrors = erroneousField.Errors.Select(error => new Error(fieldKey, error.ErrorMessage));
                result.AddRange(fieldErrors);
            }

            return result;
        }
    }
}
