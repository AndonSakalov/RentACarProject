using RentACar.Data.Models;
using RentACar.Data.Repository.Interfaces;
using RentACar.Services.Data.Interfaces;
using RentACar.Web.ViewModels;

namespace RentACar.Services.Data
{
	public class VehicleTypeService : BaseService, IVehicleTypeService
	{
		private readonly IRepository<VehicleType, Guid> vehicleTypeRepository;
		public VehicleTypeService(IRepository<VehicleType, Guid> vehicleTypeRepository)
		{
			this.vehicleTypeRepository = vehicleTypeRepository;
		}
		public async Task<IEnumerable<AddVehicleVehicleTypeViewModel>> GetAllVehicleTypesAsync()
		{
			var allVehicleTypes = (await vehicleTypeRepository.GetAllAsync())
				.ToList()
				.Select(vt => new AddVehicleVehicleTypeViewModel()
				{
					Id = vt.Id,
					Name = vt.Name
				});

			return allVehicleTypes;
		}
	}
}
