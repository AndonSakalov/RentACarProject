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

		public static class Engine
		{
			public const string HPIsRequiredMessage = "Horse power for the engine is required.";
			public const string HPRangeMessage = "The horse power can be between 60 and 2000.";

			public const string TorqueRangeMessage = "Torque can be between 100 and 1500.";

			public const string FuelEfficiencyIsRequiredMessage = "Fuel efficiency is required.";
			public const string FuelEfficiencyRangeMessage = "Fuel efficiency can be between 1 and 50.";

			public const string DisplacementIsRequiredMessage = "Displacement is required.";
			public const string DisplacementRangeMessage = "Displacement can be between 1 and 10.";

			public const string CylindersCountIsRequiredMessage = "Cylinders count is required.";
			public const string CylindersCountRangeMessage = "Cylinders count can be between 4 and 16.";

			public const string DescriptionMaxLengthMessage = "Description can be up to 500 symbols.";

			public const string DisplacementErrorMessageForElectric = "Electric engines can not have displacement.";
			public const string CylindersCountErrorMessageForElectric = "Electric engines can not have cylinders.";
			public const string FuelEfficiencyErrorMessageForElectric = "Electric engines can not have fuel efficiency.";
		}
		public static class Make
		{
			public const string NameIsRequiredMessage = "Make name is required for creation.";
			public const string NameMinLengthMessage = "Make name should be atleast 2 symbols.";
			public const string NameMaxLengthMessage = "Make name can be up to 20 symbols.";

			public const string CountryIsRequiredMessage = "Country name is required for creation.";
			public const string CountryMinLengthMessage = "Country name should be atleast 3 symbols.";
			public const string CountryMaxLengthMessage = "Country name can be up to 100 symbols.";
		}
	}
}
