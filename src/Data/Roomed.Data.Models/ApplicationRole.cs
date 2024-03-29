﻿// |-----------------------------------------------------------------------------------------------------|
// <copyright file="ApplicationRole.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// Extension of the base identity role class.
    /// </summary>
    public class ApplicationRole : IdentityRole<Guid>
    {
    }
}
