using RentACar.Web.ViewModels;

namespace RentACar.Services.Data.Interfaces
{
	public interface IUserManagementService
	{
		Task<SearchUserViewModel> SearchUsers(SearchUserViewModel model);
		Task<bool> PromoteToStaffMemberAsync(SearchUserViewModel model);
	}
}
