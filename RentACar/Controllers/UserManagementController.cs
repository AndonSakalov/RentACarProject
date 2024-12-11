using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentACar.Services.Data.Interfaces;
using RentACar.Web.ViewModels;

namespace RentACar.Controllers
{
	[Authorize(Roles = "Admin")]
	public class UserManagementController : Controller
	{
		private readonly IUserManagementService service;
		public UserManagementController(IUserManagementService service)
		{
			this.service = service;
		}

		[HttpGet]
		public IActionResult SearchUsers()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SearchUsers(SearchUserViewModel model)
		{
			model = await service.SearchUsersAsync(model);

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> PromoteToStaff(SearchUserViewModel model)
		{
			await service.PromoteToStaffMemberAsync(model);

			return View("SearchUsers");
		}
	}
}
