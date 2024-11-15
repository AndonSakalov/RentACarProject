namespace RentACar.Services.Data.Interfaces
{
	public interface IBaseService
	{
		bool IsGuidValid(string? id, ref Guid parsedGuid);

		decimal CalculateRentalPrice(DateTime pickupDate, DateTime returnDate, decimal pricePerDay);
	}
}
