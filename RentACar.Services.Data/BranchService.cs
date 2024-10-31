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
                   Name = b.Name,
                   Address = b.Address,
                   Country = b.Country,
                   VehiclesCount = b.Vehicles.Count
               })
               .ToListAsync();

            return foundBranches;
        }
    }
}
