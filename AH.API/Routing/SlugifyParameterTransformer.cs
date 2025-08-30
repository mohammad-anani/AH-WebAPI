using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Text.RegularExpressions;

namespace AH.API.Routing
{
    // Transforms route tokens like [controller] into slug-case (lowercase-with-dashes)
    public sealed class SlugifyParameterTransformer : IOutboundParameterTransformer
    {
        public string? TransformOutbound(object? value)
        {
            if (value == null)
                return null;

            var str = value.ToString();
            if (string.IsNullOrEmpty(str))
                return str;

            // Insert dashes between lowercase-to-uppercase boundaries, then lower.
            // Example: TestAppointment => test-appointment
            var slug = Regex.Replace(str!, "([a-z])([A-Z])", "$1-$2").ToLowerInvariant();
            return slug;
        }
    }
}
