using RentACar.Web.ViewModels;

namespace RentACar.Services.Data.Interfaces
{
	public interface IVehicleTypeService
	{
		Task<IEnumerable<AddVehicleVehicleTypeViewModel>> GetAllVehicleTypesAsync();
	}
}
