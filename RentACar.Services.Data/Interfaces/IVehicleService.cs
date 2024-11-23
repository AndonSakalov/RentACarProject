using RentACar.Web.ViewModels;

namespace RentACar.Services.Data.Interfaces
{
	public interface IVehicleService
	{
		Task<bool> CreateAndAddVehicle(AddVehicleViewModel model);
	}
}
