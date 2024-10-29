namespace RentACar.Common
{
    public class EntityValidationConstants
    {
        public static class Vehicle
        {
            public const int ColorMinLength = 3;
            public const int ColorMaxLength = 30;

            public const int ModelMaxLength = 100;
            public const int ModelMinLength = 5;

            public const int SeatsMinCount = 2;
            public const int SeatsMaxCount = 8;

            public const int DoorsMinCount = 2;
            public const int DoorsMaxCount = 4;

            public const int MileageMinNumber = 0;
            public const int MileageMaxNumber = 1000000;

            public const int RegistrationMinLength = 8;
            public const int RegistrationMaxLength = 12;

            public const int ImageUrlMinLength = 8;
            public const int ImageUrlMaxLength = 2083;

            public const int VINNumberMinLength = 12;
            public const int VINNumberMaxLength = 17;

            public const string NoImageUrl = "/img/no-image.jpg";

            public const int PricePerDayMinValue = 10;
            public const int PricePerDayMaxValue = 1500;
        }

        public static class Make
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 20;

            public const int CountryMinLength = 3;
            public const int CountryMaxLength = 100;
        }

        public static class VehicleType
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 20;

            public const int DescriptionMaxLength = 500;
        }

        public static class Branch
        {
            public const int NameMinLength = 10;
            public const int NameMaxLength = 100;

            public const int AddressMinLength = 10;
            public const int AddressMaxLength = 200;

            public const int CityMinLength = 3;
            public const int CityMaxLength = 85;

            public const int CountryMinLength = 3;
            public const int CountryMaxLength = 100;

            public const int PhoneNumberMinLength = 10;
            public const int PhoneNumberMaxLength = 12;

            public const string RentalDateFormat = "yyyy-MM-ddTHH:mm";
        }

        public static class Engine
        {
            public const int HPMinRange = 60;
            public const int HPMaxRange = 2000;

            public const int MinTorque = 100;
            public const int MaxTorque = 1500;

            public const int FuelEfficiencyMin = 1;
            public const int FuelEfficiencyMax = 50;

            public const int DisplacementMinRange = 1;
            public const double DisplacementMaxRange = 10;

            public const int CylindersMinCount = 4;
            public const int CylindersMaxCount = 16;

            public const int DescriptionMaxLength = 500;

        }

        public static class Transmission
        {
            public const int MinGearsCount = 1;
            public const int MaxGearsCount = 12;
        }
    }
}
