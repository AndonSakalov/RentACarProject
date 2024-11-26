using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RentACar.Services.Data.Interfaces;
using RentACar.Web.ViewModels;

namespace RentACar.Controllers
{

	public class BranchController : Controller
	{
		private readonly IBranchService service;
		public BranchController(IBranchService service)
		{
			this.service = service;
		}
		public async Task<IActionResult> Index(SearchBranchViewModel model)
		{
			var outputModel = await service.GetAllOrderedByLocationAsync(model);

			if (outputModel.IsNullOrEmpty())
			{
				ViewBag.Message = $"No branches found in {model.City.ToString()}";
			}
			else
			{
				ViewBag.Message = $"Branches in {model.City.ToString()}"; ;
			}

			return View(outputModel);
		}

		[HttpGet]
		public async Task<IActionResult> VehicleType(string id, string pickupDate, string returnDate)
		{
			var vehicleTypes = await service.GetAllVehicleTypesAsync(id, pickupDate, returnDate);
			if (vehicleTypes == null)
			{
				RedirectToAction(nameof(Index));
			}

			return View(vehicleTypes);
		}

		[HttpGet]
		public async Task<IActionResult> Vehicles(string id, string pickupDate, string returnDate, string vehicleTypeName)
		{

			IEnumerable<VehicleListViewModel> outputModel = await service.GetAllVehiclesForCurrentBranchAsync(id, pickupDate, returnDate, vehicleTypeName);

			if (outputModel == null)
			{
				return RedirectToAction(nameof(Index));//wrong data
			}
			if (outputModel!.Count() == 0)
			{
				ViewData["Title"] = $"No vehicles matching your criteria.";
			}
			else
			{
				ViewData["Title"] = $"{outputModel!.First().VehicleType} vehicles in {outputModel!.First().BranchName}";

			}

			ViewData["BranchId"] = id;
			ViewData["PickUpDate"] = pickupDate;
			ViewData["ReturnDate"] = returnDate;
			ViewData["VehicleTypeName"] = vehicleTypeName;

			return this.View(outputModel);
		}

		[HttpGet]
		public async Task<IActionResult> FilteredVehicles(VehicleFilterViewModel filters)
		{

			IEnumerable<VehicleListViewModel> outputModel = await service.GetAllVehiclesFilteredAsync(filters);

			if (outputModel == null)
			{
				return RedirectToAction(nameof(Index)); //wrong data
			}

			if (outputModel!.Count() == 0)
			{
				ViewData["Title"] = $"No vehicles matching your criteria.";
			}
			else
			{
				ViewData["Title"] = $"{outputModel!.First().VehicleType} vehicles in {outputModel!.First().BranchName}";
			}

			ViewData["BranchId"] = filters.BranchId;
			ViewData["PickUpDate"] = filters.PickupDate;
			ViewData["ReturnDate"] = filters.ReturnDate;
			ViewData["VehicleTypeName"] = filters.VehicleTypeName;


			return View("Vehicles", outputModel);
		}

		[HttpGet]
		public async Task<IActionResult> StaffSearchBranches(StaffSearchViewModel model)
		{
			model.FilteredBranches = (await service.StaffSearchBranchesAsync(model)).ToList();

			return this.View(model);
		}


	}
}
