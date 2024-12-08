using RentACar.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace RentACar.Web.ViewModels.Rental
{
	public class EndRentalViewModel
	{
		[Required]
		public Guid VehicleId { get; set; }

		[Required]
		public Guid RentalId { get; set; }

		[Required]
		public Guid CustomerId { get; set; }

		[Required]
		public decimal AmountRequired { get; set; }

		[Required]
		public Guid BranchId { get; set; }

		[Required]
		public string CustomerEmail { get; set; } = null!;

		[Required]
		public string VehicleName { get; set; } = null!;

		[Required]
		public Guid PaymentId { get; set; }

		[Required]
		[PaymentAmountValidation(nameof(AmountRequired))]
		public decimal PaymentAmount { get; set; }

		[Required]
		public PaymentMethod PaymentMethod { get; set; }

		[Required]
		public PaymentStatus PaymentStatus { get; set; }

		[Required]
		public int KilometersTraveled { get; set; }
	}

	public class PaymentAmountValidationAttribute : ValidationAttribute
	{
		private readonly string _amountRequiredPropertyName;

		public PaymentAmountValidationAttribute(string amountRequiredPropertyName)
		{
			_amountRequiredPropertyName = amountRequiredPropertyName;
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var amountRequiredProperty = validationContext.ObjectType.GetProperty(_amountRequiredPropertyName);
			if (amountRequiredProperty == null)
			{
				return new ValidationResult($"Unknown property: {_amountRequiredPropertyName}");
			}

			var amountRequired = amountRequiredProperty.GetValue(validationContext.ObjectInstance, null);
			if (value != null && amountRequired != null && (decimal)value == (decimal)amountRequired)
			{
				return ValidationResult.Success;
			}

			return new ValidationResult("Payment amount must be equal to the required amount.");
		}
	}
}
