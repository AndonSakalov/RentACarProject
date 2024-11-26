using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcContrib.Filters;
using RentACar.Web.ViewModels;
using System.Globalization;
using static RentACar.Common.EntityValidationConstants.Branch;
using static RentACar.Common.EntityValidationErrorMessages.Branch;

namespace RentACar.Controllers
{
    [PassParametersDuringRedirect]
    [Authorize]
    public class SearchController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Index(SearchBranchViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            bool isPickupDateValid = DateTime.TryParseExact(model.PickUpDate, RentalDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime validPickupDate);

            bool isReturnDateValid = DateTime.TryParseExact(model.ReturnDate, RentalDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime validReturnDate);

            var result = DateTime.Compare(validPickupDate, validReturnDate);

            if (result > 0)
            {
                ModelState.AddModelError(nameof(model.PickUpDate), PickUpDateIsLaterThanReturnDateMessage);
                return this.View(model);
            }
            else if (result == 0)
            {
                ModelState.AddModelError(nameof(model.PickUpDate), PickUpDateAndReturnDateAreTheSameMessage);
                return this.View(model);
            }

            var compareToDateTimeNow = DateTime.Compare(validPickupDate, DateTime.Now);

            if (compareToDateTimeNow < 0)
            {
                ModelState.AddModelError(nameof(model.PickUpDate), PickUpDateIsBeforeCurrentDate);
                return this.View(model);
            }

            return RedirectToAction(nameof(Index), "Branch", model);
        }

        [HttpGet]
        [Authorize(Roles = "Staff, Admin")]
        public IActionResult StaffSearch()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = "Staff, Admin")]
        public IActionResult StaffSearch(StaffSearchViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            return RedirectToAction("StaffSearchBranches", "Branch", model);
        }

    }
}
