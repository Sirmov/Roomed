// |-----------------------------------------------------------------------------------------------------|
// <copyright file="BeforeDateAttribute.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Common.ValidationAttributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Reflection;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public sealed class BeforeDateAttribute : ValidationAttribute
    {
        public BeforeDateAttribute(string otherProperty)
        {
            this.OtherProperty = otherProperty;
        }

        public string OtherProperty { get; set; }

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

                    if (date < otherDate)
                    {
                        return ValidationResult.Success;
                    }

                    return new ValidationResult("This date is not before the other date.");
                }
                catch (InvalidCastException ex)
                {
                    return new ValidationResult("Invalid cast. One of the properties is not in a correct format.");
                    throw;
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
