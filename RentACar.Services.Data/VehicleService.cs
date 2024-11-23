using RentACar.Data.Models;
using RentACar.Data.Repository.Interfaces;
using RentACar.Services.Data.Interfaces;
using RentACar.Web.ViewModels;

namespace RentACar.Services.Data
{
    public class VehicleService : BaseService, IVehicleService
    {
        private IRepository<Vehicle, Guid> vehicleRepository;
        private IRepository<Make, Guid> makeRepository;
        private IRepository<VehicleType, Guid> vehicleTypeRepository;
        private IRepository<Transmission, Guid> transmissionRepository;
        private IRepository<Branch, Guid> branchRepository;
        private IRepository<Engine, Guid> engineRepository;

        public VehicleService(
            IRepository<Vehicle, Guid> repository,
            IRepository<Make, Guid> makeRepository,
            IRepository<VehicleType, Guid> vehicleTypeRepository,
            IRepository<Transmission, Guid> transmissionRepository,
            IRepository<Branch, Guid> branchRepository,
            IRepository<Engine, Guid> engineRepository)
        {
            this.vehicleRepository = repository;
            this.makeRepository = makeRepository;
            this.vehicleTypeRepository = vehicleTypeRepository;
            this.transmissionRepository = transmissionRepository;
            this.branchRepository = branchRepository;
            this.engineRepository = engineRepository;
        }

        public async Task<bool> CreateAndAddVehicle(AddVehicleViewModel model)
        {
            try
            {
                var make = await makeRepository.GetByIdAsync(model.MakeId);
                var vehicleType = await vehicleTypeRepository.GetByIdAsync(model.VehicleTypeId);
                var transmission = await transmissionRepository.GetByIdAsync(model.TransmissionId);
                var branch = await branchRepository.GetByIdAsync(model.BranchId);
                var engine = await engineRepository.GetByIdAsync(model.EngineId);

                if (make == null || vehicleType == null || transmission == null || branch == null || engine == null)
                {
                    throw new Exception("HTML tampering!");
                }

                Vehicle vehicleToCreate = new Vehicle()
                {
                    MakeId = model.MakeId,
                    Color = model.Color,
                    Model = model.Model,
                    VehicleTypeId = model.VehicleTypeId,
                    TransmissionId = model.TransmissionId,
                    SeatsCount = model.SeatsCount,
                    DoorsCount = model.DoorsCount,
                    Year = model.Year, //int
                    Mileage = model.Mileage,
                    RegistrationNumber = model.RegistrationNumber,
                    ImageUrl = model.ImageUrl,
                    VINNumber = model.VINNumber,
                    AddedOn = DateTime.Now,
                    BranchId = model.BranchId,
                    EngineId = model.EngineId,
                    PricePerDay = model.PricePerDay,
                };

                await vehicleRepository.AddAsync(vehicleToCreate);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
