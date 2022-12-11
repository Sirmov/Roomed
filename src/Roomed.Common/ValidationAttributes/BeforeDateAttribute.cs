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

    /// <summary>
    /// This validation attribute validates that the date of this property is before another one.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public sealed class BeforeDateAttribute : ValidationAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BeforeDateAttribute"/> class.
        /// </summary>
        /// <param name="otherProperty">The name of the other property.</param>
        public BeforeDateAttribute(string otherProperty)
        {
            this.OtherProperty = otherProperty;
        }

        /// <summary>
        /// Gets or sets the name of the other property.
        /// </summary>
        public string OtherProperty { get; set; }

        /// <inheritdoc/>
        public override string FormatErrorMessage(string name)
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                ErrorMessageString,
                name);
        }

        /// <summary>
        /// This method gets the values of this property and the other is it exists.
        /// Checks if both are of the correct type and validates that the date of this property is before the other one.
        /// </summary>
        /// <param name="value">The value of this property.</param>
        /// <param name="validationContext">The context in which the validation check is performed.</param>
        /// <returns>Returns an instance of the <see cref="ValidationResult"/> class.</returns>
        /// <exception cref="NullReferenceException">Throws when <see cref="ValidationResult.Success"/> points to <see langword="null"/>.</exception>
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
                        return ValidationResult.Success ?? throw new NullReferenceException();
                    }

                    return new ValidationResult("This date is not before the other date.");
                }
                catch (InvalidCastException)
                {
                    return new ValidationResult("Invalid cast. One of the properties is not in a correct format.");
                    throw;
                }
            }
        }
    }
}
