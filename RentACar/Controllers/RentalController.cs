using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentACar.Data.Models;
using RentACar.Data.Models.Enums;
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

			TempData["Message"] = $"You have successfully reserved the vehicle.";
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
			(bool isSuccessful, bool isVehicleFree) = await rentalService.SetReservationAsRental(model);

			if (!isSuccessful)
			{
				if (!isVehicleFree)
				{
					TempData["Message"] = $"The vehicle is currently in a rental. You can not promote this reservation to rental.";
					TempData["MessageType"] = "Error";

					return RedirectToAction(nameof(ReservationsForCurrentBranch), new { model.BranchId });
				}

				TempData["Message"] = "Something went wrong while transforming this reservation to rental. Please contact system administration.";
				TempData["MessageType"] = "Error";

				return RedirectToAction("Index", "Home");
			}

			TempData["Message"] = $"You have successfully transferred the reservation to ongoing rental.";
			TempData["MessageType"] = "Success";

			return RedirectToAction(nameof(ReservationsForCurrentBranch), new { model.BranchId });
		}

		public async Task<IActionResult> GetAllRentals(string branchId)
		{
			(bool isSuccessful, RentalsCompositeViewModel? rentals) = await rentalService.GetAllRentalsForBranchAsync(branchId);

			if (!isSuccessful)
			{
				TempData["Message"] = "Something went wrong while fetching rentals. Please contact system administration.";
				TempData["MessageType"] = "Error";

				return RedirectToAction("Index", "Home");
			}

			return this.View(rentals);
		}

		[HttpGet]
		public async Task<IActionResult> EndRental(string id, string vehicleId)
		{
			(bool isSuccessful, EndRentalViewModel? model) result = await rentalService.GetVehicleRentalToRemoveAsync(id, vehicleId);

			if (result.isSuccessful == false)
			{
				TempData["Message"] = "Something went wrong with fetching the rental. Please contact system administration.";
				TempData["MessageType"] = "Error";

				return RedirectToAction("Index", "Home");
			}

			var paymentMethods = Enum.GetValues(typeof(PaymentMethod))
			   .Cast<PaymentMethod>()
			   .Select(pm => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
			   {
				   Value = ((int)pm).ToString(),
				   Text = pm.ToString()
			   })
			   .ToList();

			var paymentStatuses = Enum.GetValues(typeof(PaymentStatus))
		   .Cast<PaymentStatus>()
		   .Select(ps => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
		   {
			   Value = ((int)ps).ToString(),
			   Text = ps.ToString()
		   })
		   .ToList();

			ViewBag.PaymentStatusesList = paymentStatuses;
			ViewBag.PaymentMethodsList = paymentMethods;

			return this.View(result.model);
		}

		[HttpPost]
		public async Task<IActionResult> EndRental(EndRentalViewModel model)
		{
			var paymentMethods = Enum.GetValues(typeof(PaymentMethod))
			   .Cast<PaymentMethod>()
			   .Select(pm => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
			   {
				   Value = ((int)pm).ToString(),
				   Text = pm.ToString()
			   })
			   .ToList();

			var paymentStatuses = Enum.GetValues(typeof(PaymentStatus))
		   .Cast<PaymentStatus>()
		   .Select(ps => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
		   {
			   Value = ((int)ps).ToString(),
			   Text = ps.ToString()
		   })
		   .ToList();

			ViewBag.PaymentStatusesList = paymentStatuses;
			ViewBag.PaymentMethodsList = paymentMethods;

			if (!ModelState.IsValid)
			{
				return this.View(model);
			}

			bool isSuccessful = await rentalService.EndRentalAsync(model);

			if (!isSuccessful)
			{
				TempData["Message"] = "Something went wrong with ending the rental. Please contact system administration.";
				TempData["MessageType"] = "Error";

				return RedirectToAction("GetAllRentals", "Rental", new { model.BranchId });
			}

			TempData["Message"] = $"You have successfully ended rental of customer with email: {model.CustomerEmail} and vehicle: {model.VehicleName}.";
			TempData["MessageType"] = "Success";

			return RedirectToAction("GetAllRentals", "Rental", new { model.BranchId });
		}
	}
}
