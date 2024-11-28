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
				return NotFound(); //wrong data
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

		[HttpGet]
		public async Task<IActionResult> Delete(string branchId, string vehicleId)
		{
			(EditVehicleListViewModel? vehicle, bool isFound) = await vehicleService.GetVehicleToDeleteByIdAsync(vehicleId);
			if (isFound == false)
			{
				return RedirectToAction(nameof(Vehicles), branchId, vehicleId);
			}

			return this.View(vehicle);
		}


		[HttpPost]
		public async Task<IActionResult> Delete(EditVehicleListViewModel deleteModel)
		{
			bool isDeleted = await vehicleService.DeleteVehicleAsync(deleteModel);
			if (!isDeleted)
			{
				return NotFound(); // not successful
			}

			return RedirectToAction(nameof(Vehicles), new { branchId = deleteModel.BranchId.ToString() });
		}

		[HttpGet]
		public async Task<IActionResult> Edit(string branchId, string vehicleId)
		{

			(EditVehicleViewModel? vehicleToEdit, bool isFound) = (await vehicleService.GetVehicleToEditByIdAsync(vehicleId));
			if (isFound == false)
			{
				return NotFound();
			}

			vehicleToEdit = await SeedEditVehicleViewModelAsync(vehicleToEdit!);

			return View(vehicleToEdit);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(EditVehicleViewModel editModel)
		{
			editModel = await SeedEditVehicleViewModelAsync(editModel);

			if (!ModelState.IsValid)
			{
				return this.View(editModel);
			}
			bool isEditedAndSaved = await vehicleService.EditAndSaveChangesAsync(editModel);
			if (!isEditedAndSaved)
			{
				TempData["Message"] = "Something went wrong while creating the vehicle and adding it to the system. Please contact system administrator.";
				TempData["MessageType"] = "Error";

				return RedirectToAction("Index", "Home");
			}

			TempData["Message"] = $"You have successfully edited the vehicle."; //TODO:give more info about the change
			TempData["MessageType"] = "Success";

			return RedirectToAction("Index", "Home");
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

		private async Task<EditVehicleViewModel> SeedEditVehicleViewModelAsync(EditVehicleViewModel model)
		{
			model.Engines = (await engineService.GetAllEnginesAsync()).ToList();
			model.Transmissions = (await transmissionService.GetAllTransmissionsAsync()).ToList();
			model.Branches = (await branchService.GetAllBranchesAsync()).ToList();

			return model;
		}
	}
}
