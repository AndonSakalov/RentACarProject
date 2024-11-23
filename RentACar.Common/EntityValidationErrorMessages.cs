namespace RentACar.Common
{
    public class EntityValidationErrorMessages
    {
        public static class Branch
        {
            public const string CityRequiredMessage = "City name is required.";
            public const string CityMinLengthMessage = "City name should be atleast 3 symbols.";
            public const string CityMaxLengthMessage = "City name can be up to 85 symbols.";

            public const string PickUpDateIsLaterThanReturnDateMessage = "Pick-up date can not be later than return date.";
            public const string PickUpDateAndReturnDateAreTheSameMessage = "Pick-up date and return date can not be the same.";
            public const string PickUpDateIsBeforeCurrentDate = "Pick-up date can not be before now.";
        }

        public static class Vehicle
        {
            public const string ColorIsRequired = "Vehicle color is required.";

        }
    }
}
