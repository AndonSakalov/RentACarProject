using RentACar.Data.Models;
using RentACar.Data.Repository.Interfaces;
using RentACar.Services.Data.Interfaces;
using RentACar.Web.ViewModels;

namespace RentACar.Services.Data
{
	public class MakeService : BaseService, IMakeService
	{
		private readonly IRepository<Make, Guid> makeRepository;

		public MakeService(IRepository<Make, Guid> makeRepository)
		{
			this.makeRepository = makeRepository;
		}
		public async Task<IEnumerable<AddVehicleMakeViewModel>> GetAllMakesAsync()
		{
			var allMakes = (await makeRepository.GetAllAsync())
				.ToList()
				.Select(m => new AddVehicleMakeViewModel()
				{
					Id = m.Id,
					Name = m.Name
				});

			return allMakes;
		}
	}
}
