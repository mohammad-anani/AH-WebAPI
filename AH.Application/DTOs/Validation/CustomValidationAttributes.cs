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
        public bool Required { get; set; }

        public WorkingDaysStringAttribute(bool required = true)
        {
            Required = required;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // Handle null or empty
            if (value == null)
            {
                return Required
                    ? new ValidationResult("Working days are required.")
                    : ValidationResult.Success;
            }

            // Convert to list of strings
            List<string> parts;

            switch (value)
            {
                case string s:
                    parts = s.Split(',', StringSplitOptions.RemoveEmptyEntries)
                             .Select(p => p.Trim())
                             .ToList();
                    break;

                case List<string> list:
                    parts = list.Where(p => !string.IsNullOrWhiteSpace(p))
                                .Select(p => p.Trim())
                                .ToList();
                    break;

                default:
                    return new ValidationResult("Invalid type for working days. Must be a string or list of strings.");
            }

            // Check required
            if (!parts.Any())
            {
                return Required
                    ? new ValidationResult("You must specify at least 1 working day.")
                    : ValidationResult.Success;
            }

            if (parts.Count > 7)
                return new ValidationResult("You cannot specify more than 7 working days.");

            // Normalize & check validity
            var normalized = new List<string>();
            foreach (var part in parts)
            {
                if (!Employee.ValidDays.TryGetValue(part, out var normalizedDay))
                {
                    return new ValidationResult($"Invalid working day: '{part}'.");
                }
                normalized.Add(normalizedDay);
            }

            // Check uniqueness
            if (normalized.Distinct(StringComparer.OrdinalIgnoreCase).Count() != normalized.Count)
                return new ValidationResult("Duplicate working days are not allowed.");

            return ValidationResult.Success;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} must be a list of valid weekdays (Mon, Tue, ..., case-insensitive).";
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class OperationDoctorsAttribute : ValidationAttribute
    {
        private readonly int _maxDoctors;

        public OperationDoctorsAttribute(int maxDoctors = 5)
        {
            _maxDoctors = maxDoctors;
            ErrorMessage = $"The field must contain 1 to {_maxDoctors} doctors in the format DoctorID:Role;";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            string doctorsString = value.ToString()!;
            if (string.IsNullOrWhiteSpace(doctorsString))
                return new ValidationResult(ErrorMessage);

            var doctors = doctorsString.Split(';', StringSplitOptions.RemoveEmptyEntries);

            if (doctors.Length == 0 || doctors.Length > _maxDoctors)
                return new ValidationResult($"You must provide between 1 and {_maxDoctors} doctors.");

            foreach (var doctor in doctors)
            {
                var parts = doctor.Split(':', StringSplitOptions.None);

                if (parts.Length != 2)
                    return new ValidationResult($"Each doctor entry must be in the format DoctorID:Role. Invalid: '{doctor}'");

                if (string.IsNullOrWhiteSpace(parts[0]))
                    return new ValidationResult($"DoctorID is required. Invalid entry: '{doctor}'");

                // Optionally, validate that DoctorID is numeric
                if (!int.TryParse(parts[0], out int doctorId) || doctorId <= 0)
                    return new ValidationResult($"DoctorID must be a positive integer. Invalid entry: '{doctor}'");

                // Role is optional, no further validation needed
            }

            return ValidationResult.Success;
        }
    }
    }