using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Reflection;


public class ValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                // Build a list of BindNever properties from all action arguments
                var bindNeverProps = context.ActionArguments.Values
                    .SelectMany(arg => arg?.GetType().GetProperties() ?? Array.Empty<PropertyInfo>())
                    .Where(prop => prop.GetCustomAttribute<BindNeverAttribute>() != null)
                    .Select(prop => prop.Name)
                    .ToHashSet(StringComparer.OrdinalIgnoreCase);

                // Filter ModelState to exclude keys that match BindNever properties
                var errors = context.ModelState
                    .Where(e =>
                        e.Value?.Errors.Count > 0 &&
                        !bindNeverProps.Contains(GetTopPropertyName(e.Key)))
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
                    );

                if (errors.Any())
                {
                    context.Result = new BadRequestObjectResult(new { Errors = errors });
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }

        private static string GetTopPropertyName(string key)
        {
            // ModelState keys can look like "MyDto.CreatedByAdminID" or just "CreatedByAdminID"
            var dotIndex = key.IndexOf('.');
            return dotIndex > 0 ? key.Substring(dotIndex + 1) : key;
        }
}