using RentACar.Web.ViewModels;

namespace RentACar.Services.Data.Interfaces
{
	public interface ITransmissionService
	{
		Task<IEnumerable<AddVehicleTransmissionViewModel>> GetAllTransmissionsAsync();
	}
}
