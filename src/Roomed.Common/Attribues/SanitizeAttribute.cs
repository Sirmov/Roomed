// |-----------------------------------------------------------------------------------------------------|
// <copyright file="SanitizeAttribute.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Common.Attribues
{
    /// <summary>
    /// This attribute is used to mark the properties that should be html sanitized.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class SanitizeAttribute : Attribute
    {
    }
}
