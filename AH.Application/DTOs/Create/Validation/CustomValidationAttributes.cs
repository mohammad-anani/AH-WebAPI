using AH.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Validation
{
    public class FutureDateWithinYearAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is DateTime dateTime)
            {
                var now = DateTime.Now;
                var oneYearFromNow = now.AddYears(1);
                return dateTime > now && dateTime <= oneYearFromNow;
            }
            if (value is DateOnly dateOnly)
            {
                var now = DateOnly.FromDateTime(DateTime.Now);
                var oneYearFromNow = now.AddYears(1);
                return dateOnly > now && dateOnly <= oneYearFromNow;
            }
            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} must be in the future and within one year from now.";
        }
    }

    public class PastDateWithin120YearsAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is DateTime dateTime)
            {
                var now = DateTime.Now;
                var minimumDate = now.AddYears(-120);
                return dateTime >= minimumDate && dateTime <= now;
            }
            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} must be between 120 years ago and now.";
        }
    }

    public class HireDateValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is DateTime dateTime)
            {
                var year2000 = new DateTime(2000, 1, 1);
                var now = DateTime.Now;
                return dateTime >= year2000 && dateTime <= now;
            }
            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} must be between year 2000 and now.";
        }
    }

    public class MedicationDateRangeAttribute : ValidationAttribute
    {
        public string EndDateProperty { get; set; }

        public MedicationDateRangeAttribute(string endDateProperty)
        {
            EndDateProperty = endDateProperty;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime startDate)
            {
                var endDateProperty = validationContext.ObjectType.GetProperty(EndDateProperty);
                if (endDateProperty != null)
                {
                    var endDateValue = endDateProperty.GetValue(validationContext.ObjectInstance);
                    if (endDateValue is DateTime endDate)
                    {
                        var now = DateTime.Now;
                        var fiveYearsFromNow = now.AddYears(5);

                        if (startDate >= now && endDate > startDate && endDate <= fiveYearsFromNow)
                        {
                            return ValidationResult.Success;
                        }
                    }
                }
            }
            return new ValidationResult("Medication start must be now or later, end must be after start, and within 5 years from now.");
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class WorkingDaysStringAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not string input || string.IsNullOrWhiteSpace(input))
            {
                return new ValidationResult("Working days must be a comma-separated string of weekdays.");
            }

            // Split by comma, trim spaces, ignore empty items
            var parts = input.Split(',', StringSplitOptions.RemoveEmptyEntries)
                             .Select(p => p.Trim())
                             .ToList();

            if (parts.Count == 0)
                return new ValidationResult("You must specify at least 1 working day.");

            if (parts.Count > 7)
                return new ValidationResult("You cannot specify more than 7 working days.");

            // Normalize using dictionary & check validity
            var normalized = new List<string>();
            foreach (var part in parts)
            {
                if (!Employee.ValidDays.TryGetValue(part, out var normalizedDay))
                {
                    return new ValidationResult($"Invalid working day: '{part}'.");
                }
                normalized.Add(normalizedDay);
            }

            // Check uniqueness (case-insensitive, but we already normalized so just Distinct)
            if (normalized.Distinct().Count() != normalized.Count)
                return new ValidationResult("Duplicate working days are not allowed.");

            // Everything passed
            return ValidationResult.Success;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} must be a comma-separated list of weekdays (Mon or Monday, case-insensitive).";
        }
    }
}