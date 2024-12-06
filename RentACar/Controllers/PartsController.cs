using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentACar.Data.Models.Enums;
using RentACar.Services.Data.Interfaces;
using RentACar.Web.ViewModels.Parts;
using static RentACar.Common.EntityValidationConstants.Engine;
using static RentACar.Common.EntityValidationErrorMessages.Engine;
//using System.Web.Mvc;

namespace RentACar.Controllers
{
    [Authorize(Roles = "Staff, Admin")]
    public class PartsController : Controller
    {
        private readonly IEngineService engineService;
        private readonly IMakeService makeService;

        public PartsController(IEngineService engineService, IMakeService makeService)
        {
            this.engineService = engineService;
            this.makeService = makeService;
        }
        public IActionResult SelectAction()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateEngine()
        {
            var fuelTypesList = Enum.GetValues(typeof(FuelType))
                .Cast<FuelType>()
                .Select(ft => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = ((int)ft).ToString(),
                    Text = ft.ToString()
                })
                .ToList();

            ViewBag.FuelTypesList = fuelTypesList;

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEngine(CreateEngineViewModel model)
        {
            var fuelTypesList = Enum.GetValues(typeof(FuelType))
                .Cast<FuelType>()
                .Select(ft => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = ((int)ft).ToString(),
                    Text = ft.ToString()
                })
                .ToList();
            ViewBag.FuelTypesList = fuelTypesList;

            if (!model.IsElectric)
            {
                if (model.CylindersCount < CylindersMinCount)
                {
                    ModelState.AddModelError(nameof(model.CylindersCount), CylindersCountRangeMessage);
                }
                if (model.FuelEfficiency < FuelEfficiencyMin)
                {
                    ModelState.AddModelError(nameof(model.FuelEfficiency), FuelEfficiencyRangeMessage);
                }
                if (model.Displacement < DisplacementMinRange)
                {
                    ModelState.AddModelError(nameof(model.Displacement), DisplacementRangeMessage);
                }
            }

            if (model.IsElectric)
            {
                ModelState.Remove(nameof(model.CylindersCount));
                ModelState.Remove(nameof(model.Displacement));
                ModelState.Remove(nameof(model.FuelEfficiency));
                if (model.CylindersCount > 0)
                {
                    ModelState.AddModelError(nameof(model.CylindersCount), CylindersCountErrorMessageForElectric);
                }
                if (model.Displacement > 0)
                {
                    ModelState.AddModelError(nameof(model.Displacement), DisplacementErrorMessageForElectric);
                }
                if (model.FuelEfficiency > 0)
                {
                    ModelState.AddModelError(nameof(model.FuelEfficiency), FuelEfficiencyErrorMessageForElectric);
                }
            }

            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            bool isSuccessful = await engineService.CreateAndAddEngineAsync(model);
            if (!isSuccessful)
            {
                TempData["Message"] = "Something went wrong. Please contact system administrator.";
                TempData["MessageType"] = "Error";

                return RedirectToAction(nameof(SelectAction), "Parts");
            }

            TempData["Message"] = $"You have successfully created and added to the system engine with {model.HP} HP.";
            TempData["MessageType"] = "Success";

            return RedirectToAction(nameof(SelectAction), "Parts");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEngines()
        {
            var engines = await engineService.GetAllEnginesForDeletionAsync();

            return this.View(engines);
        }

        [HttpGet]
        public IActionResult DeleteEngine(string id, int hp, string fuelType, int cylindersCount, decimal displacement)
        {
            DeleteEngineViewModel model = new DeleteEngineViewModel()
            {
                Id = Guid.Parse(id),
                HP = hp,
                FuelType = fuelType,
                CylindersCount = cylindersCount,
                Displacement = displacement
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEngine(DeleteEngineViewModel model)
        {
            bool isSuccessful = await engineService.DeleteEngineAsync(model.Id);

            if (!isSuccessful)
            {
                TempData["Message"] = "Something went wrong. Please contact system administrator.";
                TempData["MessageType"] = "Error";

                return RedirectToAction(nameof(GetAllEngines), "Parts");
            }

            TempData["Message"] = $"You have successfully deleted {model.DisplayName} engine.";
            TempData["MessageType"] = "Success";

            return RedirectToAction(nameof(GetAllEngines), "Parts");
        }

        [HttpGet]
        public IActionResult CreateMake()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateMake(CreateMakeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }
            bool isSuccessful = await makeService.CreateMakeAsync(model);

            if (!isSuccessful)
            {
                TempData["Message"] = "Something went wrong. Please contact system administrator.";
                TempData["MessageType"] = "Error";
            }
            else
            {
                TempData["Message"] = $"You have successfully created and added to the system {model.Name} - {model.Country}.";
                TempData["MessageType"] = "Success";
            }

            return RedirectToAction(nameof(SelectAction), "Parts");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMakes()
        {
            var makes = await makeService.GetAllMakesForDeletionAsync();

            return this.View(makes);
        }

        [HttpGet]
        public IActionResult DeleteMake(string id, string name, string country)
        {
            DeleteMakeViewModel model = new DeleteMakeViewModel()
            {
                Id = Guid.Parse(id),
                Name = name,
                Country = country
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMake(DeleteMakeViewModel model)
        {
            bool isSuccessful = await makeService.DeleteMakeAsync(model.Id);
            if (!isSuccessful)
            {
                TempData["Message"] = "Something went wrong. Please contact system administrator.";
                TempData["MessageType"] = "Error";

                return RedirectToAction(nameof(GetAllMakes), "Parts");
            }

            TempData["Message"] = $"You have successfully deleted {model.Name}.";
            TempData["MessageType"] = "Success";

            return RedirectToAction(nameof(GetAllMakes), "Parts");
        }
    }
}
