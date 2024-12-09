using RentACar.Web.ViewModels.UserAccountInfo;

namespace RentACar.Services.Data.Interfaces
{
	public interface IUserAccountInfoService
	{
		Task<(bool isSuccessful, UserAccountInfoViewModel? model)> GetUserInfoAsync(string? id, int pageIndex, int pageSize);
	}
}
