using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentACar.Data.Models;
using RentACar.Services.Data.Interfaces;

namespace RentACar.Controllers
{
    [Authorize]
    public class UserAccountController : Controller
    {
        private readonly IUserAccountInfoService userAccountInfoService;
        private readonly UserManager<ApplicationUser> userManager;
        public UserAccountController(IUserAccountInfoService userAccountInfoService, UserManager<ApplicationUser> userManager)
        {
            this.userAccountInfoService = userAccountInfoService;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 3)
        {
            var userId = userManager.GetUserId(User);
            var accountInfo = await userAccountInfoService.GetUserInfoAsync(userId, pageIndex, pageSize);
            if (!accountInfo.isSuccessful)
            {
                TempData["Message"] = "Something went wrong. Please contact customer support.";
                TempData["MessageType"] = "Error";

                return RedirectToAction("Index", "Home");
            }

            return View(accountInfo.model);
        }
    }
}
