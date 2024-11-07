using Microsoft.AspNetCore.Mvc;
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
        //TODO: Make getById methods to not work only with guids because we might want to search in mapping table which is composite key.
        public async Task<IActionResult> Index(SearchBranchViewModel model)
        {
            ViewBag.Message = model.City.ToString();

            var outputModel = await service.GetAllOrderedByLocationAsync(model);

            return View(outputModel);
        }

        [HttpGet]
        public async Task<IActionResult> VehicleType(string id)
        {
            var vehicleTypes = await service.GetAllVehicleTypesAsync(id);
            if (vehicleTypes == null)
            {
                RedirectToAction(nameof(Index));
            }

            return View(vehicleTypes);
        }
    }
}
