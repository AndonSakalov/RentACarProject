using Microsoft.AspNetCore.Mvc;
using RentACar.Data;
using RentACar.Services.Data.Interfaces;
using RentACar.Web.ViewModels;

namespace RentACar.Controllers
{
    public class BranchController : Controller
    {
        private readonly RentACarDbContext context;
        private readonly IBranchService service;
        public BranchController(RentACarDbContext context, IBranchService service)
        {
            this.context = context;
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
