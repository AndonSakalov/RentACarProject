using Microsoft.AspNetCore.Mvc;
using RentACar.Services.Data.Interfaces;
using RentACar.Web.ViewModels;

namespace RentACar.Controllers
{
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

			//TODO:Finish creating vehicle and adding it to the db.
			return this.View(vehicleModel);
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
