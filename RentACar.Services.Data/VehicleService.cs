using Microsoft.EntityFrameworkCore;
using RentACar.Data.Models;
using RentACar.Data.Repository.Interfaces;
using RentACar.Services.Data.Interfaces;

namespace RentACar.Services.Data
{
    public class VehicleService : BaseService, IVehicleService
    {
        private IRepository<Vehicle, Guid> vehicleRepository;

        public VehicleService(IRepository<Vehicle, Guid> repository)
        {
            this.vehicleRepository = repository;
        }
        public async Task<IEnumerable<Make>> GetAllMakes()
        {
            var makes = await vehicleRepository.GetAllAttached()
                .Select(v => v.Make)
                .ToListAsync();

            return makes;
        }
    }
}
