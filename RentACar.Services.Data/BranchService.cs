using Microsoft.EntityFrameworkCore;
using RentACar.Data.Models;
using RentACar.Data.Repository.Interfaces;
using RentACar.Services.Data.Interfaces;
using RentACar.Web.ViewModels;
using System.Globalization;
using static RentACar.Common.EntityValidationConstants.Branch;

namespace RentACar.Services.Data
{
    public class BranchService : BaseService, IBranchService
    {
        private readonly IRepository<Branch, Guid> branchRepository;

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
                   VehiclesCount = b.Vehicles.Count,
                   PickUpDate = model.PickUpDate,
                   ReturnDate = model.ReturnDate,
               })
               .ToListAsync();

            return foundBranches;
        }

        public async Task<IEnumerable<VehicleTypeViewModel>> GetAllVehicleTypesAsync(string id, string pickupDate, string returnDate)
        {
            bool isPickupDateValid = DateTime.TryParseExact(pickupDate, RentalDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime validPickupDate);

            bool isReturnDateValid = DateTime.TryParseExact(returnDate, RentalDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime validReturnDate);

            var datesResult = DateTime.Compare(validPickupDate, validReturnDate);

            if (datesResult > 0 || datesResult == 0 || !isPickupDateValid || !isReturnDateValid)
            {
                return null;
            }

            Guid parsedGuid = Guid.Empty;
            bool isGuidValid = IsGuidValid(id, ref parsedGuid);

            if (!isGuidValid)
            {
                return null;
            }

            var currentBranch = await branchRepository.GetAllAttached()
                .Include(b => b.Vehicles)
                .ThenInclude(v => v.VehicleType)
                .Include(b => b.Vehicles)
                .ThenInclude(v => v.Rental)
                .SingleOrDefaultAsync(b => b.Id == parsedGuid);

            if (currentBranch == null)
            {
                return null;
            }

            List<VehicleTypeViewModel> vehicleTypes = currentBranch.Vehicles
                .Where(v => v.RentalId == null || DateTime.Compare(v.Rental.EndDate, validPickupDate) < 0)
                 .Select(v => new VehicleTypeViewModel()
                 {
                     Name = v.VehicleType.Name,
                     Description = v.VehicleType.Description,
                     ImageUrl = v.VehicleType.ImageUrl, //hmm
                     PickUpDate = pickupDate,
                     ReturnDate = returnDate,
                     BranchId = id
                 })
                 .GroupBy(vt => vt.Name)
                 .Select(g => g.First())
                 .ToList();

            return vehicleTypes;

        }

        public async Task<IEnumerable<VehicleListViewModel>> GetAllVehiclesForCurrentBranchAsync(string id, string pickupDate, string returnDate, string vehicleTypeName)
        {
            bool isPickupDateValid = DateTime.TryParseExact(pickupDate, RentalDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime validPickupDate);

            bool isReturnDateValid = DateTime.TryParseExact(returnDate, RentalDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime validReturnDate);

            var datesResult = DateTime.Compare(validPickupDate, validReturnDate);

            if (datesResult > 0 || datesResult == 0 || !isPickupDateValid || !isReturnDateValid)
            {
                return null;
            }

            Guid parsedGuid = Guid.Empty;
            bool isGuidValid = IsGuidValid(id, ref parsedGuid);

            if (!isGuidValid)
            {
                return null;
            }

            var vehicles = await branchRepository.GetAllAttached()
                .Include(b => b.Vehicles)
                .ThenInclude(v => v.Make)
                .Include(b => b.Vehicles)
                .ThenInclude(v => v.Transmission)
                .Include(b => b.Vehicles)
                .ThenInclude(v => v.VehicleType)
                .Include(b => b.Vehicles)
                .ThenInclude(v => v.Branch)
                .SelectMany(b => b.Vehicles
                .Where(v => v.VehicleType.Name == vehicleTypeName && v.IsDeleted == false && v.BranchId == parsedGuid))
                .ToListAsync();

            List<VehicleListViewModel> outputModel = new List<VehicleListViewModel>();

            foreach (var v in vehicles)
            {
                VehicleListViewModel model = new VehicleListViewModel()
                {
                    Name = $"{v.Make.Name} {v.Model}",
                    SeatsCount = v.SeatsCount,
                    TransmissionType = v.Transmission.Type.ToString(),
                    Mileage = v.Mileage,
                    PriceForSelectedDays = CalculateRentalPrice(validPickupDate, validReturnDate, v.PricePerDay),
                    ImageUrl = v.ImageUrl!,
                    VehicleType = v.VehicleType.Name,
                    BranchName = v.Branch.Name,
                    RentalLengthInDays = Math.Abs(validReturnDate.Day - validPickupDate.Day) + 1
                };

                outputModel.Add(model);
            }

            return outputModel;
        }

        public async Task<IEnumerable<VehicleListViewModel>> GetAllVehiclesFilteredAsync(VehicleFilterViewModel model)
        {
            bool isPickupDateValid = DateTime.TryParseExact(model.PickupDate, RentalDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime validPickupDate);

            bool isReturnDateValid = DateTime.TryParseExact(model.ReturnDate, RentalDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime validReturnDate);

            var datesResult = DateTime.Compare(validPickupDate, validReturnDate);

            if (datesResult > 0 || datesResult == 0 || !isPickupDateValid || !isReturnDateValid)
            {
                return null;
            }

            Guid parsedGuid = Guid.Empty;
            bool isGuidValid = IsGuidValid(model.BranchId, ref parsedGuid);

            if (!isGuidValid)
            {
                return null;
            }

            var availableVehicles = await branchRepository.GetAllAttached()
                .Where(b => b.Id == parsedGuid)
                .Include(b => b.Vehicles)
                    .ThenInclude(v => v.VehicleType)
                .Include(b => b.Vehicles)
                    .ThenInclude(v => v.Make)
                .Include(b => b.Vehicles)
                    .ThenInclude(v => v.Transmission)
                .Include(b => b.Vehicles)
                    .ThenInclude(v => v.Branch)
                .SelectMany(b => b.Vehicles)
                .Where(v => v.VehicleType.Name == model.VehicleTypeName && v.IsDeleted == false)
                .Where(v => v.Rental == null || DateTime.Compare(validPickupDate, v.Rental.EndDate) > 0)
                .ToListAsync(); //TODO:Optimize

            if (model.PriceRanges.Any())
            {
                foreach (var p in model.PriceRanges)
                {
                    int[] prices;
                    if (p.Contains("+"))
                    {
                        prices = p.Split("+", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                        int price = prices[0];
                        availableVehicles = availableVehicles
                            .Where(v => v.PricePerDay >= price).ToList();
                    }
                    else
                    {
                        prices = p.Split("-", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                        int lowerPrice = prices[0];
                        int upperPrice = prices[1];

                        availableVehicles = availableVehicles
                            .Where(v => v.PricePerDay >= lowerPrice && v.PricePerDay <= upperPrice).ToList();
                    }
                }
            }
            if (model.DoorsCount != 0)
            {
                if (model.DoorsCount == 2)
                {
                    availableVehicles = availableVehicles
                        .Where(v => v.DoorsCount == 2).ToList();
                }
                else if (model.DoorsCount == 4)
                {
                    availableVehicles = availableVehicles
                        .Where(v => v.DoorsCount == 4).ToList();
                }
            }
            if (model.TransmissionType != null)
            {
                availableVehicles = availableVehicles
                    .Where(v => v.Transmission.Type.ToString() == model.TransmissionType).ToList();
            }

            List<VehicleListViewModel> outputModel = new List<VehicleListViewModel>();

            foreach (var v in availableVehicles)
            {
                VehicleListViewModel currentModel = new VehicleListViewModel()
                {
                    Name = $"{v.Make.Name} {v.Model}",
                    SeatsCount = v.SeatsCount,
                    TransmissionType = v.Transmission.Type.ToString(),
                    Mileage = v.Mileage,
                    PriceForSelectedDays = CalculateRentalPrice(validPickupDate, validReturnDate, v.PricePerDay),
                    ImageUrl = v.ImageUrl!,
                    VehicleType = v.VehicleType.Name,
                    BranchName = v.Branch.Name,
                    RentalLengthInDays = Math.Abs(validReturnDate.Day - validPickupDate.Day) + 1
                };

                outputModel.Add(currentModel);
            }

            return outputModel;
        }

        public async Task<IEnumerable<AddVehicleBranchViewModel>> GetAllBranchesAsync()
        {
            var allBranches = (await branchRepository.GetAllAsync())
                .ToList()
                .Select(b => new AddVehicleBranchViewModel()
                {
                    Id = b.Id,
                    Name = b.Name,
                    Address = b.Address,
                    City = b.City,
                    Country = b.Country
                });



            return allBranches;
        }
    }
}
