// |-----------------------------------------------------------------------------------------------------|
// <copyright file="AreasControllersActionsConstants.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Common
{
    /// <summary>
    /// This is a static class containing string constants of all area, controller and action names.
    /// </summary>
    public static class AreasControllersActionsConstants
    {
        /// <summary>
        /// This class holds the names of all areas.
        /// </summary>
        public static class Areas
        {
            /// <summary>
            /// A string representing the administration area.
            /// </summary>
            public const string Administration = "Administration";
        }

        /// <summary>
        /// This class holds the names of all controllers.
        /// </summary>
        public static class Controllers
        {
            /// <summary>
            /// A string representing the home controller.
            /// </summary>
            public const string Home = "Home";

            /// <summary>
            /// A string representing the user controller.
            /// </summary>
            public const string User = "User";

            /// <summary>
            /// A string representing the reservations controller.
            /// </summary>
            public const string Reservations = "Reservations";

            /// <summary>
            /// A string representing the identity documents controller.
            /// </summary>
            public const string IdentityDocuments = "IdentityDocuments";

            /// <summary>
            /// A string representing the profiles controller.
            /// </summary>
            public const string Profiles = "Profiles";

            /// <summary>
            /// A string representing the users controller.
            /// </summary>
            public const string Users = "Users";
        }

        /// <summary>
        /// This class holds the names of all actions.
        /// </summary>
        public static class Actions
        {
            // Common actions

            /// <summary>
            /// A string representing the index action.
            /// </summary>
            public const string Index = "Index";

            /// <summary>
            /// A string representing the create action.
            /// </summary>
            public const string Create = "Create";

            /// <summary>
            /// A string representing the details action.
            /// </summary>
            public const string Details = "Details";

            /// <summary>
            /// A string representing the edit action.
            /// </summary>
            public const string Edit = "Edit";

            /// <summary>
            /// A string representing the delete action.
            /// </summary>
            public const string Delete = "Delete";

            // User controller actions

            /// <summary>
            /// A string representing the login action.
            /// </summary>
            public const string Login = "Login";

            /// <summary>
            /// A string representing the register action.
            /// </summary>
            public const string Register = "Register";

            /// <summary>
            /// A string representing the logout action.
            /// </summary>
            public const string Logout = "Logout";

            /// <summary>
            /// A string representing the forgot password action.
            /// </summary>
            public const string ForgotPassword = "ForgotPassword";

            // Home controller actions

            /// <summary>
            /// A string representing the not implemented action.
            /// </summary>
            public const string NotImplemented = "NotImplemented";

            // Reservations controller actions

            /// <summary>
            /// A string representing the choose room action.
            /// </summary>
            public const string ChooseRoom = "ChooseRoom";
        }
    }
}
