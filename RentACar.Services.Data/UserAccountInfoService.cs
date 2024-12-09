using Microsoft.EntityFrameworkCore;
using RentACar.Common;
using RentACar.Data.Models;
using RentACar.Data.Repository.Interfaces;
using RentACar.Services.Data.Interfaces;
using RentACar.Web.ViewModels.UserAccountInfo;

namespace RentACar.Services.Data
{
	public class UserAccountInfoService : BaseService, IUserAccountInfoService
	{
		private readonly IRepository<ApplicationUser, Guid> userRepository;
		private readonly IRepository<Rental, Guid> rentalsRepository;
		private readonly IRepository<Vehicle, Guid> vehicleRepository;
		public UserAccountInfoService(
			IRepository<ApplicationUser, Guid> userRepository,
			IRepository<Rental, Guid> rentalsRepository,
			IRepository<Vehicle, Guid> vehicleRepository)
		{
			this.userRepository = userRepository;
			this.rentalsRepository = rentalsRepository;
			this.vehicleRepository = vehicleRepository;
		}
		public async Task<(bool isSuccessful, UserAccountInfoViewModel? model)> GetUserInfoAsync(string? id, int pageIndex, int pageSize)
		{
			Guid validUserId = Guid.Empty;
			bool isGuidValid = IsGuidValid(id, ref validUserId);
			if (!isGuidValid)
			{
				return (false, null);
			}

			ApplicationUser? user = await userRepository.GetByIdAsync(validUserId);
			if (user == null)
			{
				return (false, null);
			}


			// Query rentals with related vehicle data
			var rentalsQuery = rentalsRepository.GetAllAttached()
				.Where(r => r.CustomerId == validUserId && r.VehicleId != Guid.Empty)
				.Include(r => r.Vehicle.Make)
				.OrderByDescending(r => r.EndDate)
				.Select(r => new RentalHistoryViewModel
				{
					Id = r.Id,
					StartDate = r.StartDate,
					EndDate = r.EndDate,
					RentalPrice = r.TotalPrice,
					VehicleName = $"{r.Vehicle.Make.Name} {r.Vehicle.Model}",
					VehicleId = r.VehicleId
				});

			// Create a paginated list from the query
			var paginatedRentals = await PaginatedList<RentalHistoryViewModel>.CreateAsync(rentalsQuery, pageIndex, pageSize);

			// Create and return the UserAccountInfoViewModel
			var model = new UserAccountInfoViewModel
			{
				Id = user.Id,
				Email = user.Email!,
				UserName = user.UserName!,
				PhoneNumber = user.PhoneNumber!,
				Rentals = paginatedRentals
			};

			foreach (var r in model.Rentals)
			{
				var vehicle = await vehicleRepository.GetAllAttached()
					.Where(v => v.Id == r.VehicleId)
					.Include(v => v.Make)
					.FirstOrDefaultAsync();

				if (vehicle == null)
				{
					return (false, null);
				}
				r.VehicleName = $"{vehicle.Make.Name} {vehicle.Model}";
			}

			return (true, model);
		}
	}
}
