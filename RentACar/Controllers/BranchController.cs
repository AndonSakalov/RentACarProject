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

        public async Task<IActionResult> Index(SearchBranchViewModel model)
        {
            ViewBag.Message = model.City.ToString();

            var outputModel = await service.GetAllOrderedByLocationAsync(model);

            return View(outputModel);
        }
    }
}
