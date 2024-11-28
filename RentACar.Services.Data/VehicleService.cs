using Microsoft.EntityFrameworkCore;
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

		public async Task<bool> EditAndSaveChangesAsync(EditVehicleViewModel model)
		{
			bool isSuccessful = false;
			var transmission = await transmissionRepository.GetByIdAsync(model.TransmissionId);
			var branch = await branchRepository.GetByIdAsync(model.BranchId);
			var engine = await engineRepository.GetByIdAsync(model.EngineId);

			Vehicle? vehicleToEdit = await vehicleRepository.GetAllAttached()
				.Where(v => v.Id == model.Id && v.IsDeleted == false)
				.FirstOrDefaultAsync();

			if (transmission == null || branch == null || engine == null || vehicleToEdit == null)
			{
				return false;
			}

			vehicleToEdit.TransmissionId = transmission.Id;
			vehicleToEdit.BranchId = branch.Id;
			vehicleToEdit.EngineId = engine.Id;
			vehicleToEdit.Color = model.Color;
			vehicleToEdit.RegistrationNumber = model.RegistrationNumber;
			vehicleToEdit.PricePerDay = model.PricePerDay;
			vehicleToEdit.ImageUrl = model.ImageUrl;

			isSuccessful = await vehicleRepository.UpdateAsync(vehicleToEdit);

			if (!isSuccessful)
			{
				return false;
			}

			return true;
		}

		public async Task<bool> CreateAndAddVehicleAsync(AddVehicleViewModel model)
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
					return false;
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
					Year = model.Year,
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

		public async Task<bool> DeleteVehicleAsync(EditVehicleListViewModel model)
		{

			var parsedGuid = model.VehicleId;

			Vehicle vehicleToDelete = vehicleRepository.GetById(parsedGuid);
			if (vehicleToDelete == null)
			{
				return false;
			}

			vehicleToDelete.IsDeleted = true;

			await vehicleRepository.UpdateAsync(vehicleToDelete);

			return true;
		}

		public async Task<IEnumerable<EditVehicleListViewModel>> FilterVehiclesAsync(string branchId, string vehicleType)
		{
			Guid validGuid = Guid.NewGuid();

			bool isGuidValid = IsGuidValid(branchId, ref validGuid);

			if (!isGuidValid)
			{
				return null;
			}


			var vehicles = await vehicleRepository.GetAllAttached()
				.Where(v => v.Branch.Id == validGuid && v.VehicleType.Name == vehicleType && v.IsDeleted == false)
				.Select(v => new EditVehicleListViewModel()
				{
					Make = v.Make.Name,
					Model = v.Model,
					VehicleType = v.VehicleType.ToString()!,
					SeatsCount = v.SeatsCount,
					TransmissionType = v.Transmission.Type.ToString(),
					Mileage = v.Mileage,
					ImageUrl = v.ImageUrl!,
					BranchName = v.Branch.Name,
					BranchId = validGuid,
					VehicleId = v.Id
				})
				.ToListAsync();

			return vehicles;
		}

		public async Task<IEnumerable<EditVehicleListViewModel>> GetAllVehiclesAsync(string id)
		{
			Guid validGuid = Guid.NewGuid();

			bool isGuidValid = IsGuidValid(id, ref validGuid);
			if (!isGuidValid)
			{
				return null;
			}

			var vehicles = await vehicleRepository.GetAllAttached()
				.Include(v => v.Branch)
				.Where(v => v.Branch.Id == validGuid)
				.Where(v => v.IsDeleted == false)
				.Select(v => new EditVehicleListViewModel()
				{
					Make = v.Make.Name,
					Model = v.Model,
					VehicleType = v.VehicleType.ToString()!,
					SeatsCount = v.SeatsCount,
					TransmissionType = v.Transmission.Type.ToString(),
					Mileage = v.Mileage,
					ImageUrl = v.ImageUrl!,
					BranchName = v.Branch.Name,
					BranchId = validGuid,
					VehicleId = v.Id
				})
				.ToListAsync();



			return vehicles;
		}

		public async Task<(EditVehicleListViewModel? model, bool isFound)> GetVehicleToDeleteByIdAsync(string id)
		{
			Guid validGuid = Guid.NewGuid();

			bool isGuidValid = IsGuidValid(id, ref validGuid);
			if (!isGuidValid)
			{
				return (null, false);
			}

			Vehicle? vehicle = await vehicleRepository.GetAllAttached()
				.Where(v => v.Id == validGuid)
				.Include(v => v.Make)
				.Include(v => v.Transmission)
				.Include(v => v.Branch)
				.Include(v => v.VehicleType)
				.FirstOrDefaultAsync();
			if (vehicle == null)
			{
				return (null, false);
			}

			EditVehicleListViewModel model = new EditVehicleListViewModel()
			{
				Make = vehicle.Make.Name,
				Model = vehicle.Model,
				VehicleType = vehicle.VehicleType.ToString()!,
				SeatsCount = vehicle.SeatsCount,
				TransmissionType = vehicle.Transmission.Type.ToString(),
				Mileage = vehicle.Mileage,
				ImageUrl = vehicle.ImageUrl!,
				BranchName = vehicle.Branch.Name,
				BranchId = vehicle.BranchId,
				VehicleId = vehicle.Id
			};

			return (model, true);
		}

		public async Task<(EditVehicleViewModel? model, bool isFound)> GetVehicleToEditByIdAsync(string id)
		{
			Guid validGuid = Guid.NewGuid();

			bool isGuidValid = IsGuidValid(id, ref validGuid);
			if (!isGuidValid)
			{
				return (null, false);
			}

			Vehicle? vehicleToEdit = await vehicleRepository.GetAllAttached()
				.Where(v => v.Id == validGuid && v.IsDeleted == false)
				.FirstOrDefaultAsync();

			if (vehicleToEdit == null)
			{
				return (null, false);
			}

			EditVehicleViewModel model = new EditVehicleViewModel()
			{
				Id = vehicleToEdit.Id,
				Color = vehicleToEdit.Color,
				TransmissionId = vehicleToEdit.TransmissionId,
				RegistrationNumber = vehicleToEdit?.RegistrationNumber,
				ImageUrl = vehicleToEdit?.ImageUrl,
				BranchId = vehicleToEdit!.BranchId,
				EngineId = vehicleToEdit.EngineId,
				PricePerDay = vehicleToEdit.PricePerDay,
			};

			return (model, true);
		}
	}
}
