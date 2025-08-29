using System;
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
}