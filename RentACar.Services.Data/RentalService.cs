using Microsoft.EntityFrameworkCore;
using RentACar.Data.Models;
using RentACar.Data.Repository.Interfaces;
using RentACar.Services.Data.Interfaces;
using RentACar.Web.ViewModels.Rental;
using System.Globalization;
using static RentACar.Common.EntityValidationConstants.Branch;

namespace RentACar.Services.Data
{
	public class RentalService : BaseService, IRentalService
	{
		private readonly IRepository<Rental, Guid> rentalRepository;
		private readonly IRepository<Vehicle, Guid> vehicleRepository;
		private readonly IRepository<Branch, Guid> branchRepository;

		public RentalService(
			IRepository<Rental, Guid> rentalRepository,
			IRepository<Vehicle, Guid> vehicleRepository,
			IRepository<Branch, Guid> branchRepository)
		{
			this.rentalRepository = rentalRepository;
			this.vehicleRepository = vehicleRepository;
			this.branchRepository = branchRepository;
		}
		public async Task<(bool isValid, RentalViewModel? model)> ValidateInput(string branchId, string vehicleId, string pickupDate, string returnDate, string vehicleType, decimal price)
		{
			Guid validBranchId = Guid.Empty;
			Guid validVehicleId = Guid.Empty;

			bool isBranchIdValid = IsGuidValid(branchId, ref validBranchId);
			bool isVehicleIdValid = IsGuidValid(vehicleId, ref validVehicleId);

			bool isPickupDateValid = DateTime.TryParseExact(pickupDate, RentalDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime validPickupDate);

			bool isReturnDateValid = DateTime.TryParseExact(returnDate, RentalDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime validReturnDate);

			var datesResult = DateTime.Compare(validPickupDate, validReturnDate);

			if (datesResult > 0 || datesResult == 0 || !isPickupDateValid || !isReturnDateValid || !isBranchIdValid || !isVehicleIdValid)
			{
				return (false, null);
			}

			Branch? branch = await branchRepository.GetByIdAsync(validBranchId);
			Vehicle? vehicle = vehicleRepository.GetAllAttached()
				.Where(v => v.Id == validVehicleId)
				.Include(v => v.Transmission)
				.Include(v => v.Make)
				.SingleOrDefault();

			if (branch == null || vehicle == null)
			{
				return (false, null);
			}

			RentalViewModel model = new RentalViewModel()
			{
				Branch =
				{
					Id = branch.Id,
					Name = branch.Name,
					City = branch.City,
					Country = branch.Country,
					Address = branch.Address
				},
				Vehicle =
				{
					Id =vehicle.Id,
					ImageUrl = vehicle.ImageUrl!,
					Name = $"{vehicle.Make.Name} {vehicle.Model}",
					SeatsCount = vehicle.SeatsCount,
					TransmissionType = vehicle.Transmission.Type.ToString()
				},
				PickupDate = validPickupDate,
				ReturnDate = validReturnDate,
				Price = price,
				VehicleType = vehicleType
			};

			return (true, model);
		}
	}
}
