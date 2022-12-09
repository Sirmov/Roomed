namespace Roomed.Common
{
    /// <summary>
    /// This is a static class containing string constants of all controller names and actions.
    /// Its purpose is to prevent the use of magic strings.
    /// </summary>
    public static class AreasControllersActionsConstants
    {
        /// <summary>
        /// This class holds the names of all areas.
        /// </summary>
        public static class Areas
        {
            public const string Administration = "Administration";
        }

        /// <summary>
        /// This class holds the names of all controllers.
        /// </summary>
        public static class Controllers
        {
            public const string Home = "Home";
            public const string User = "User";
            public const string Reservations = "Reservations";
            public const string IdentityDocuments = "IdentityDocuments";
            public const string Profiles = "Profiles";
            public const string Users = "Users";
        }

        /// <summary>
        /// This class holds the names of all actions.
        /// </summary>
        public static class Actions
        {
            // Common actions
            public const string Index = "Index";
            public const string Create = "Create";
            public const string Details = "Details";
            public const string Edit = "Edit";
            public const string Delete = "Delete";

            // User controller actions
            public const string Login = "Login";
            public const string Register = "Register";
            public const string Logout = "Logout";
            public const string ForgotPassword = "ForgotPassword";

            // Home controller actions
            public const string NotImplemented = "NotImplemented";

            // Reservations controller actions
            public const string ChooseRoom = "ChooseRoom";
        }
    }
}
