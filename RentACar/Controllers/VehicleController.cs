using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentACar.Services.Data.Interfaces;
using RentACar.Web.ViewModels;

namespace RentACar.Controllers
{
	[Authorize(Roles = "Staff, Admin")]
	public class VehicleController : Controller
	{
		private readonly IVehicleService vehicleService;
		private readonly IMakeService makeService;
		private readonly IEngineService engineService;
		private readonly IVehicleTypeService vehicleTypeService;
		private readonly ITransmissionService transmissionService;
		private readonly IBranchService branchService;
		public VehicleController(
			IVehicleService vehicleService,
			IMakeService makeService,
			IEngineService engineService,
			IVehicleTypeService vehicleTypeService,
			ITransmissionService transmissionService,
			IBranchService branchService)
		{
			this.vehicleService = vehicleService;
			this.makeService = makeService;
			this.engineService = engineService;
			this.vehicleTypeService = vehicleTypeService;
			this.transmissionService = transmissionService;
			this.branchService = branchService;
		}
		[HttpGet]
		public async Task<IActionResult> Add()
		{
			var model = new AddVehicleViewModel();

			model = await SeedAddVehicleViewModelAsync(model);
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Add(AddVehicleViewModel vehicleModel)
		{
			vehicleModel = await SeedAddVehicleViewModelAsync(vehicleModel);
			if (!ModelState.IsValid)
			{
				return this.View(vehicleModel);
			}

			bool isVehicleCreatedAndAdded = await vehicleService.CreateAndAddVehicleAsync(vehicleModel);

			if (isVehicleCreatedAndAdded)
			{
				var selectedMake = vehicleModel.Makes
					 .FirstOrDefault(m => m.Id == vehicleModel.MakeId);
				var selectedBranch = vehicleModel.Branches
					.FirstOrDefault(b => b.Id == vehicleModel.BranchId);

				if (selectedMake != null && selectedBranch != null)
				{
					TempData["Message"] = $"You have successfully added {selectedMake.Name} {vehicleModel.Model} to {selectedBranch.Name}.";
					TempData["MessageType"] = "Success";

					return RedirectToAction("Index", "Home");
				}

			}

			TempData["Message"] = "Something went wrong while creating the vehicle and adding it to the system. Please contact system administrator.";
			TempData["MessageType"] = "Error";

			return RedirectToAction("Index", "Home");
		}

		[HttpGet]
		public async Task<IActionResult> Vehicles(string branchId)
		{
			var vehicles = await vehicleService.GetAllVehiclesAsync(branchId);

			if (vehicles == null)
			{
				return null; //wrong data
			}
			if (vehicles.Count() == 0)
			{
				ViewData["Title"] = $"No vehicles matching your criteria.";
			}
			else
			{
				ViewData["Title"] = $"Vehicles in {vehicles.First().BranchName}";
			}

			ViewData["BranchId"] = branchId;

			return this.View(vehicles);
		}

		[HttpGet]
		public async Task<IActionResult> FilteredVehicles(string branchId, string vehicleType)
		{
			var filteredVehicles = await vehicleService.FilterVehiclesAsync(branchId, vehicleType);

			if (filteredVehicles == null)
			{
				return RedirectToAction(nameof(Vehicles)); //wrong data
			}
			if (filteredVehicles?.Count() == 0)
			{
				ViewData["Title"] = $"No vehicles matching your criteria.";
			}
			else
			{
				ViewData["Title"] = $"Vehicles in {filteredVehicles.First().BranchName}";
			}

			ViewData["BranchId"] = branchId;

			return View(nameof(Vehicles), filteredVehicles);
		}

		[HttpPost]
		public async Task<IActionResult> Delete(string id)
		{
			return null; //TODO:Implement soft delete
		}


		private async Task<AddVehicleViewModel> SeedAddVehicleViewModelAsync(AddVehicleViewModel model)
		{
			model.Makes = (await makeService.GetAllMakesAsync()).ToList();
			model.Engines = (await engineService.GetAllEnginesAsync()).ToList();
			model.VehicleTypes = (await vehicleTypeService.GetAllVehicleTypesAsync()).ToList();
			model.Transmissions = (await transmissionService.GetAllTransmissionsAsync()).ToList();
			model.Branches = (await branchService.GetAllBranchesAsync()).ToList();

			return model;
		}
	}
}
