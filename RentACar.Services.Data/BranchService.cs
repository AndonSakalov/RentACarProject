using Microsoft.EntityFrameworkCore;
using RentACar.Data.Models;
using RentACar.Data.Repository.Interfaces;
using RentACar.Services.Data.Interfaces;
using RentACar.Web.ViewModels;

namespace RentACar.Services.Data
{
    public class BranchService : BaseService, IBranchService
    {
        private IRepository<Branch, Guid> branchRepository;

        public BranchService(IRepository<Branch, Guid> repository)
        {
            this.branchRepository = repository;
        }
        public async Task<IEnumerable<BranchViewModel>> GetAllOrderedByLocationAsync(SearchBranchViewModel model)
        {
            var foundBranches = await branchRepository.GetAllAttached()
               .Where(b => b.City == model.City)
               .Select(b => new BranchViewModel()
               {
                   Id = b.Id.ToString(),
                   Name = b.Name,
                   Address = b.Address,
                   Country = b.Country,
                   VehiclesCount = b.Vehicles.Count
               })
               .ToListAsync();

            return foundBranches;
        }

        public async Task<IEnumerable<VehicleTypeViewModel>> GetAllVehicleTypesAsync(string id)
        {
            Guid parsedGuid = Guid.Empty;
            bool isGuidValid = IsGuidValid(id, ref parsedGuid);

            if (!isGuidValid)
            {
                return null;
            }

            var currentBranch = await branchRepository.GetAllAttached()
                .Include(b => b.Vehicles)
                .ThenInclude(v => v.VehicleType)
                .SingleOrDefaultAsync(b => b.Id == parsedGuid);

            if (currentBranch == null)
            {
                return null;
            }

            List<VehicleTypeViewModel> vehicleTypes = currentBranch.Vehicles
                 .Select(v => new VehicleTypeViewModel()
                 {
                     Name = v.VehicleType.Name,
                     Description = v.VehicleType.Description,
                     ImageUrl = v.VehicleType.ImageUrl
                 })
                 .GroupBy(vt => vt.Name)
                 .Select(g => g.First())
                 .ToList();

            return vehicleTypes;

        }
    }
}
