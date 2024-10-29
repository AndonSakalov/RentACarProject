using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Web.ViewModels;

namespace RentACar.Controllers
{
    public class BranchController : Controller
    {
        private readonly RentACarDbContext context;
        public BranchController(RentACarDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index(SearchBranchViewModel model)
        {
            ViewBag.Message = model.City.ToString();

            var foundBranches = await context.Branches
                .Where(b => b.City == model.City)
                .Select(b => new BranchViewModel()
                {
                    Name = b.Name,
                    Address = b.Address,
                    Country = b.Country,
                    VehiclesCount = b.Vehicles.Count
                })
                .ToListAsync();

            return View(foundBranches);
        }
    }
}
