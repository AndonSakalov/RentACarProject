using RentACar.Services.Data.Interfaces;

namespace RentACar.Services.Data
{
	public class BaseService : IBaseService
	{
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

		public (decimal price, int rentalLength) CalculateRentalPrice(DateTime pickupDate, DateTime returnDate, decimal pricePerDay)
		{
			int rentalLength = (returnDate.Date - pickupDate.Date).Days + 1;

			decimal price = pricePerDay * rentalLength;

			return (price, rentalLength);
		}
	}
}
