using RentACar.Web.ViewModels;

namespace RentACar.Services.Data.Interfaces
{
	public interface IEngineService
	{
		Task<IEnumerable<AddVehicleEngineViewModel>> GetAllEnginesAsync();
	}
}
