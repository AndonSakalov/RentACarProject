using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentACar.Data.Models;
using RentACar.Services.Data.Interfaces;
using RentACar.Web.ViewModels.Rental;

namespace RentACar.Controllers
{
	[Authorize]
	public class RentalController : Controller
	{
		private readonly IRentalService rentalService;
		private readonly UserManager<ApplicationUser> userManager;
		public RentalController
			(IRentalService service,
			UserManager<ApplicationUser> userManager)
		{
			this.rentalService = service;
			this.userManager = userManager;
		}
		[HttpGet]
		public async Task<IActionResult> Index(string branchId, string vehicleId, string pickupDate, string returnDate, string vehicleTypeName, decimal price)
		{
			(bool isValid, DealViewModel? model) = await rentalService.ValidateInputAsync(branchId, vehicleId, pickupDate, returnDate, vehicleTypeName, price);

			if (isValid == false)
			{
				TempData["Message"] = "Vehicle not found!";
				TempData["MessageType"] = "Error";

				return RedirectToAction("Index", "Home");
			}

			return this.View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Reserve(string branchId, string vehicleId, string pickupDate, string returnDate, decimal price, string vehicleTypeName)
		{
			(bool isValid, DealViewModel? model) = await rentalService.ValidateInputAsync(branchId, vehicleId, pickupDate, returnDate, vehicleTypeName, price);

			if (isValid == false)
			{
				TempData["Message"] = "Vehicle not found!";
				TempData["MessageType"] = "Error";

				return RedirectToAction("Index", "Home");
			}

			return this.View(model);

		}

		[HttpPost]
		public async Task<IActionResult> Reserve(DealViewModel model)
		{
			string userId = userManager.GetUserId(User)!;
			bool isSuccessful = await rentalService.ReserveVehicleAsync(model, userId);

			if (!isSuccessful)
			{
				TempData["Message"] = "Something went wrong while reserving this vehicle. Please try again later.";
				TempData["MessageType"] = "Error";

				return RedirectToAction("Index", "Home");
			}

			TempData["Message"] = $"You have successfully reserved the vehicle."; //TODO:give more info about the reservation
			TempData["MessageType"] = "Success";

			return RedirectToAction("Index", "Home");
		}

		[Authorize(Roles = "Staff, Admin")]
		[HttpGet]
		public async Task<IActionResult> ReservationsForCurrentBranch(string branchId)
		{
			var reservations = await rentalService.GetReservationsAsync(branchId);

			if (reservations.isIdValid == false)
			{
				TempData["Message"] = "Something went wrong while retrieving reservations. Please contact system administration.";
				TempData["MessageType"] = "Error";

				return RedirectToAction("Index", "Home");
			}

			return this.View(reservations.dict);
		}

		[Authorize(Roles = "Staff, Admin")]
		[HttpPost]
		public async Task<IActionResult> Rent(ConfirmReservationViewModel model)
		{
			bool isSuccessful = await rentalService.SetReservationAsRental(model);

			if (!isSuccessful)
			{
				TempData["Message"] = "Something went wrong while transforming this reservation to rental. Please contact system administration.";
				TempData["MessageType"] = "Error";

				return RedirectToAction("Index", "Home");
			}
			TempData["Message"] = $"You have successfully transferred the reservation to ongoing rental."; //TODO:give more info about the reservation
			TempData["MessageType"] = "Success";

			return RedirectToAction(nameof(ReservationsForCurrentBranch), new { model.BranchId });
		}
	}
}
