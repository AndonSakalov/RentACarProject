using RentACar.Web.ViewModels;

namespace RentACar.Services.Data.Interfaces
{
	public interface IUserManagementService
	{
		Task<SearchUserViewModel> SearchUsersAsync(SearchUserViewModel model);
		Task<bool> PromoteToStaffMemberAsync(SearchUserViewModel model);
	}
}
