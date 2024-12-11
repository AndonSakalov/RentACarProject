using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentACar.Data.Models;
using RentACar.Data.Repository.Interfaces;
using RentACar.Services.Data.Interfaces;
using RentACar.Web.ViewModels;


namespace RentACar.Services.Data
{
	public class UserManagementService : BaseService, IUserManagementService
	{
		private readonly IRepository<ApplicationUser, Guid> userManagementRepository;
		private readonly UserManager<ApplicationUser> userManager;
		public UserManagementService(IRepository<ApplicationUser, Guid> repository, UserManager<ApplicationUser> userManager)
		{
			this.userManagementRepository = repository;
			this.userManager = userManager;
		}

		public async Task<bool> PromoteToStaffMemberAsync(SearchUserViewModel model)
		{
			var user = await userManagementRepository.GetByIdAsync(model.UserId);
			if (user != null)
			{
				var result = await userManager.AddToRoleAsync(user, "Staff");
				if (result.Succeeded)
				{
					return true;
				}
				return false;
			}
			return false;
		}

		public async Task<SearchUserViewModel> SearchUsers(SearchUserViewModel model)
		{
			var foundUser = await this.userManagementRepository.GetAllAttached()
				.FirstOrDefaultAsync(u => u.Email == model.Email);

			if (foundUser != null)
			{
				model.Username = foundUser.UserName;
				model.IsFound = true;
				model.UserId = foundUser.Id;
			}
			else
			{
				model.IsFound = false;
			}

			return model;
		}

	}
}
