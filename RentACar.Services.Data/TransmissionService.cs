using RentACar.Data.Models;
using RentACar.Data.Repository.Interfaces;
using RentACar.Services.Data.Interfaces;
using RentACar.Web.ViewModels;

namespace RentACar.Services.Data
{
	public class TransmissionService : BaseService, ITransmissionService
	{
		private readonly IRepository<Transmission, Guid> transmissionRepository;

		public TransmissionService(IRepository<Transmission, Guid> transmissionRepository)
		{
			this.transmissionRepository = transmissionRepository;
		}

		public async Task<IEnumerable<AddVehicleTransmissionViewModel>> GetAllTransmissionsAsync()
		{
			var allTransmissions = (await transmissionRepository.GetAllAsync())
				.ToList()
				.Select(t => new AddVehicleTransmissionViewModel()
				{
					Id = t.Id,
					Type = t.Type.ToString(),
					GearsCount = t.GearsCount
				});

			return allTransmissions;
		}
	}
}
