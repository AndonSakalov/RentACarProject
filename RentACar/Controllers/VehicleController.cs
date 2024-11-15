using Microsoft.AspNetCore.Mvc;
using RentACar.Services.Data.Interfaces;
using RentACar.Web.ViewModels;

namespace RentACar.Controllers
{
    public class VehicleController : Controller // this controller will be for users that are with special access in order to add or remove a vehicle from the system.
    {
        private readonly IVehicleService service;
        public VehicleController(IVehicleService service)
        {
            this.service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AddVehicleViewModel();

            model.Makes = (await service.GetAllMakes()).ToList();

            return View(model);
        }

        //[HttpPost]
        //public IActionResult Add(AddVehicleViewModel model)
        //{

        //}
    }
}
