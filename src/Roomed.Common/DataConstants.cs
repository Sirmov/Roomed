namespace Roomed.Common
{
    public static class DataConstants
    {
        public static class ApplicationUser
        {
            public const int UserNameMinLength = 5;
            public const int UserNameMaxLength = 30;

            public const int PasswordMinLength = 8;
            public const int PasswordMaxLength = 30;

            public const int EmailMinLength = 6;
            public const int EmailMaxLength = 35;
        }

        public static class Reservation
        {
            public const int ReservationAdultsMaxCount = 5;
            public const int ReservationTeenagersMaxCount = 5;
            public const int ReservationChildrenMaxCount = 5;
        }

        public static class Profile
        {
            public const int ProfileFirstNameMaxLength = 50;
            public const int ProfileLastNameMaxLength = 50;
            public const int ProfileMiddleNameMaxLenght = 50;
            public const int ProfileNationalityMaxLength = 20;
            public const int ProfileNationalityCodeMinLength = 2;
            public const int ProfileNationalityCodeMaxLength = 2;
            public const int ProfileAddressMinLength = 10;
            public const int ProfileAddressMaxLength = 250;
        }

        public static class IdentityDocument
        {
            public const int IdentityDocumentNationalityMaxLength = 20;
            public const int IdentityDocumentIssuedByMaxLength = 50;
            public const int IdentityDocumentNumberMaxLength = 50;
            public const int IdentityDocumentNameMaxLength = 80;
            public const int IdentityDocumentPersonalNumberMaxLength = 30;
            public const int IdentityDocumentCountryMaxLength = 50;
            public const int IdentityDocumentPlaceOfBirthMaxLength = 50;
        }

        public static class Room
        {
            public const int RoomNumberMaxLength = 6;
        }

        public static class ReservationNote
        {
            public const int ReservationNoteBodyMaxLength = 500;
        }

        public static class ProfileNote
        {
            public const int ProfileNoteBodyMaxLength = 500;
        }
    }
}
