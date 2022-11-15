﻿namespace Roomed.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// Extension of the base identity user class.
    /// </summary>
    public class ApplicationUser : IdentityUser<Guid>
    {
    }
}
