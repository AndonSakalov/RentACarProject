using Microsoft.AspNetCore.Mvc;
using RentACar.Services.Data.Interfaces;
using RentACar.Web.ViewModels.Rental;

namespace RentACar.Controllers
{
	public class RentalController : Controller
	{
		private readonly IRentalService rentalService;
		public RentalController(IRentalService service)
		{
			this.rentalService = service;
		}
		public async Task<IActionResult> Index(string branchId, string vehicleId, string pickupDate, string returnDate, string vehicleTypeName, decimal price)
		{
			(bool isValid, RentalViewModel? model) = await rentalService.ValidateInput(branchId, vehicleId, pickupDate, returnDate, vehicleTypeName, price);

			if (isValid == false)
			{
				TempData["Message"] = "Vehicle not found!";
				TempData["MessageType"] = "Error";

				return RedirectToAction("Index", "Home");
			}

			return this.View(model);
		}
	}
}
