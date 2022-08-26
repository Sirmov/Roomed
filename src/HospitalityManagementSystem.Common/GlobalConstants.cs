namespace HospitalityManagementSystem.Common
{
    public static class GlobalConstants
    {
        // Validation Constants

        //Reservation
        public const int ReservationAdultsMaxCount = 5;
        public const int ReservationTeenagersMaxCount = 5;
        public const int ReservationChildrenMaxCount = 5;

        //Profile
        public const int ProfileFirstNameMaxLength = 50;
        public const int ProfileLastNameMaxLength = 50;
        public const int ProfileMiddleNameMaxLenght = 50;
        public const int ProfileNationalityMaxLength = 20;
        public const int ProfileNationalityCodeMinLength = 2;
        public const int ProfileNationalityCodeMaxLength = 2;
        public const int ProfileAddressMinLength = 10;
        public const int ProfileAddressMaxLength = 250;

        //Reservation Note
        public const int ReservationNoteBodyMaxLength = 500;

        //Profile Note
        public const int ProfileNoteBodyMaxLength = 500;

        //Identity Document
        public const int IdentityDocumentNationalityMaxLength = 20;
        public const int IdentityDocumentIssuedByMaxLength = 50;
        public const int IdentityDocumentNumberMaxLength = 50;
        public const int IdentityDocumentNameMaxLength = 80;
        public const int IdentityDocumentPersonalNumberMaxLength = 30;
        public const int IdentityDocumentCountryMaxLength = 50;
        public const int IdentityDocumentPlaceOfBirthMaxLength = 50;

        //Room
        public const int RoomNumberMaxLength = 6;
    }
}
