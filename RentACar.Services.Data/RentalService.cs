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
		private readonly IRepository<Reservation, Guid> reservationRepository;
		private readonly IRepository<ApplicationUser, Guid> applicationUserRepository;
		public RentalService(
			IRepository<Rental, Guid> rentalRepository,
			IRepository<Vehicle, Guid> vehicleRepository,
			IRepository<Branch, Guid> branchRepository,
			IRepository<Reservation, Guid> reservationRepository,
			IRepository<ApplicationUser, Guid> applicationUserRepository)
		{
			this.rentalRepository = rentalRepository;
			this.vehicleRepository = vehicleRepository;
			this.branchRepository = branchRepository;
			this.reservationRepository = reservationRepository;
			this.applicationUserRepository = applicationUserRepository;
		}

		public async Task<(bool isIdValid, ReservationCompositeViewModel? dict)> GetReservationsAsync(string branchId)
		{
			Guid validBranchId = Guid.Empty;
			bool isGuidValid = IsGuidValid(branchId, ref validBranchId);
			if (!isGuidValid)
			{
				return (false, null);
			}

			Branch? branch = branchRepository.GetAllAttached()
				.Where(b => b.Id == validBranchId)
				.Include(b => b.Vehicles)
				.ThenInclude(v => v.Make)
				.Include(b => b.Vehicles)
				.ThenInclude(v => v.Reservations)
				.ThenInclude(r => r.User)
				.FirstOrDefault();

			if (branch == null)
			{
				return (false, null);
			}

			Dictionary<DateTime, StaffReservationViewModel> reservations = branch.Vehicles
				.SelectMany(v => v.Reservations.Select(r => new
				{
					Reservation = r,
					Vehicle = v
				}))
				.Where(r => r.Reservation.isActive == true)
				.ToDictionary(
					rv => rv.Reservation.PickUpDate,
					rv => new StaffReservationViewModel
					{
						BranchId = branch.Id,
						BranchName = branch.Name,
						VehicleId = rv.Vehicle.Id,
						CustomerId = rv.Reservation.UserId,
						CustomerName = rv.Reservation.User.UserName!,
						CustomerEmail = rv.Reservation.User.Email!,
						PickUpDate = rv.Reservation.PickUpDate,
						ReturnDate = rv.Reservation.ReturnDate,
						Price = rv.Reservation.Price,
						VehicleName = $"{rv.Vehicle.Make.Name} {rv.Vehicle.Model}",
						PhoneNumber = rv.Reservation.User.PhoneNumber,
						ReservationId = rv.Reservation.Id,
					}
				);

			var uniqueDates = reservations.Keys
				.Select(d => d.Date)
				.Distinct()
				.OrderBy(d => d)
				.ToList();

			ReservationCompositeViewModel model = new ReservationCompositeViewModel()
			{
				Reservations = reservations,
				UniqueDates = uniqueDates
			};

			return (true, model);
		}

		public async Task<bool> ReserveVehicleAsync(DealViewModel model, string userId)
		{
			Guid validUserId = Guid.Empty;
			bool isGuidValid = IsGuidValid(userId, ref validUserId);
			if (!isGuidValid)
			{
				return false;
			}

			Reservation reservation = new Reservation()
			{
				UserId = validUserId,
				VehicleId = model.Vehicle.Id,
				PickUpDate = model.PickupDate,
				ReturnDate = model.ReturnDate,
				Price = model.Price
			};

			await reservationRepository.AddAsync(reservation);

			return await reservationRepository.UpdateAsync(reservation);
		}

		public async Task<bool> SetReservationAsRental(ConfirmReservationViewModel model)
		{
			try
			{
				Rental rental = new Rental()
				{
					CustomerId = model.CustomerId,
					StartDate = model.PickupDate,
					EndDate = model.ReturnDate,
					TotalPrice = model.Price,
					IsActive = true,
					Payment = new Payment()
					{
					}
				};

				await rentalRepository.AddAsync(rental);

				Vehicle vehicle = await vehicleRepository.GetByIdAsync(model.VehicleId);
				vehicle.RentalId = rental.Id;
				await vehicleRepository.UpdateAsync(vehicle);


				Reservation reservation = await reservationRepository.GetByIdAsync(model.ReservationId);
				reservation.isActive = false;
				await reservationRepository.UpdateAsync(reservation);

				var user = await applicationUserRepository.GetByIdAsync(model.CustomerId);
				user.Rentals.Add(rental);
				user.Reservations.Add(reservation);
				await applicationUserRepository.UpdateAsync(user);


				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public async Task<(bool isValid, DealViewModel? model)> ValidateInputAsync(string branchId, string vehicleId, string pickupDate, string returnDate, string vehicleType, decimal price)
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

			DealViewModel model = new DealViewModel()
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
