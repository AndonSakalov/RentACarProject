using RentACar.Web.ViewModels;

namespace RentACar.Services.Data.Interfaces
{
	public interface IMakeService
	{
		Task<IEnumerable<AddVehicleMakeViewModel>> GetAllMakesAsync();
	}
}
