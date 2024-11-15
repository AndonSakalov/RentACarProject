using RentACar.Services.Data.Interfaces;

namespace RentACar.Services.Data
{
	public class BaseService : IBaseService
	{
		public decimal CalculateRentalPrice(DateTime pickupDate, DateTime returnDate, decimal pricePerDay)
		{
			int rentalLength = returnDate.Day - pickupDate.Day + 1;

			return pricePerDay * rentalLength;
		}

		public bool IsGuidValid(string? id, ref Guid parsedGuid)
		{
			if (String.IsNullOrWhiteSpace(id))
			{
				return false;
			}

			bool isGuidValid = Guid.TryParse(id, out parsedGuid);
			if (!isGuidValid)
			{
				return false;
			}

			return true;
		}
	}
}
