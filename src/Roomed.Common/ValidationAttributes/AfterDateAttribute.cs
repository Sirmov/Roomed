namespace Roomed.Common.ValidationAttributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Reflection;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public class AfterDateAttribute : ValidationAttribute
    {
        public string OtherProperty { get; set; }

        public AfterDateAttribute(string otherProperty)
        {
            this.OtherProperty = otherProperty;
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            var otherPropertyInfo = validationContext.ObjectType.GetRuntimeProperty(OtherProperty);

            if (otherPropertyInfo == null || otherPropertyInfo.GetIndexParameters().Length > 0)
            {
                return new ValidationResult("Other property cannot be found.");
            }

            object? otherPropertyValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);

            if (value == null || otherPropertyValue == null)
            {
                return new ValidationResult("Properties should not be null.");
            }
            else
            {
                try
                {
                    var date = (DateOnly)value;
                    var otherDate = (DateOnly)otherPropertyValue;

                    if (date > otherDate)
                    {
                        return ValidationResult.Success;
                    }

                    return new ValidationResult("This date is not after the other date.");
                }
                catch (InvalidCastException ex)
                {
                    return new ValidationResult("Invalid cast! One of the properties is not in a correct format.");
                }
            }
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                ErrorMessageString,
                name);
        }
    }
}
