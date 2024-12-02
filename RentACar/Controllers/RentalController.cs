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
			(bool isValid, DealViewModel? model) = await rentalService.ValidateInput(branchId, vehicleId, pickupDate, returnDate, vehicleTypeName, price);

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
			(bool isValid, DealViewModel? model) = await rentalService.ValidateInput(branchId, vehicleId, pickupDate, returnDate, vehicleTypeName, price);

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
			bool isSuccessful = await rentalService.ReserveVehicle(model, userId);

			if (!isSuccessful)
			{
				TempData["Message"] = "Something went wrong while reserving this vehicle. Please try again later.";
				TempData["MessageType"] = "Error";

				return RedirectToAction("Index", "Home");
			}

			TempData["Message"] = $"You have successfully reserved the vehicle."; //TODO:give more info about the change
			TempData["MessageType"] = "Success";

			return RedirectToAction("Index", "Home");
		}
	}
}
