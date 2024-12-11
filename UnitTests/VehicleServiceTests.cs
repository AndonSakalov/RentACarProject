using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Data.Models;
using RentACar.Services.Data;
using RentACar.Tests.Repositories;
using RentACar.Web.ViewModels;

namespace RentACar.Tests
{
	public class VehicleServiceTests
	{
		private VehicleService _vehicleService;
		private RentACarDbContext _dbContext;
		private InMemoryRepository<Vehicle, Guid> _vehicleRepository;
		private InMemoryRepository<Make, Guid> _makeRepository;
		private InMemoryRepository<VehicleType, Guid> _vehicleTypeRepository;
		private InMemoryRepository<Transmission, Guid> _transmissionRepository;
		private InMemoryRepository<Branch, Guid> _branchRepository;
		private InMemoryRepository<Engine, Guid> _engineRepository;


		[SetUp]
		public void Setup()
		{
			var options = new DbContextOptionsBuilder<RentACarDbContext>()
				.UseInMemoryDatabase("TestDatabase")
				.Options;

			_dbContext = new RentACarDbContext(options);
			_makeRepository = new InMemoryRepository<Make, Guid>(_dbContext);
			_vehicleRepository = new InMemoryRepository<Vehicle, Guid>(_dbContext);
			_vehicleTypeRepository = new InMemoryRepository<VehicleType, Guid>(_dbContext);
			_transmissionRepository = new InMemoryRepository<Transmission, Guid>(_dbContext);
			_branchRepository = new InMemoryRepository<Branch, Guid>(_dbContext);
			_engineRepository = new InMemoryRepository<Engine, Guid>(_dbContext);
			_vehicleService = new VehicleService(
												_vehicleRepository,
												_makeRepository,
												_vehicleTypeRepository,
												_transmissionRepository,
												_branchRepository,
												_engineRepository);
		}

		[TearDown]
		public void TearDown()
		{
			_dbContext.Database.EnsureDeleted();
			_dbContext.Database.EnsureCreated();
			_dbContext.Dispose();
		}

		[Test]
		public async Task EditAndSaveChangesAsync_ShouldWorkCorrectly_WithValidData()
		{
			var branchId = Guid.NewGuid();
			var fixedDate = new DateTime(2024, 10, 25);

			Branch branch = CreateBranch(branchId, fixedDate);
			Vehicle vehicle = branch.Vehicles.First();
			Make make = branch.Vehicles.First().Make;
			Engine engine = branch.Vehicles.First().Engine;
			Transmission transmission = branch.Vehicles.First().Transmission;

			await _engineRepository.AddAsync(engine);
			await _transmissionRepository.AddAsync(transmission);
			await _makeRepository.AddAsync(make);
			await _vehicleRepository.AddAsync(vehicle);
			await _branchRepository.AddAsync(branch);

			Engine newEngine = new Engine()
			{
				CylindersCount = 4,
				Displacement = 2,
				FuelEfficiency = 5,
				FuelType = Data.Models.Enums.FuelType.Petrol,
				HP = 555,
				IsDeleted = false,
				IsElectric = false,
				Torque = 555,
				Description = "New Engine"
			};

			await _engineRepository.AddAsync(newEngine);

			EditVehicleViewModel model = new EditVehicleViewModel()
			{
				Id = vehicle.Id,
				BranchId = branch.Id,
				Color = "New Test color",
				ImageUrl = "New Test Image Url",
				PricePerDay = 100,
				TransmissionId = transmission.Id,
				EngineId = newEngine.Id,
				RegistrationNumber = "Test Registration Number"
			};

			var result = await _vehicleService.EditAndSaveChangesAsync(model);
			var editedVehicle = await _vehicleRepository.GetAllAttached()
				.Where(v => v.Id == vehicle.Id)
				.Include(v => v.Make)
				.FirstOrDefaultAsync();

			result.Should().BeTrue();
			editedVehicle.Should().NotBeNull();
			editedVehicle.EngineId.Should().Be(newEngine.Id);
			editedVehicle.Color.Should().Be(model.Color);
			editedVehicle.RegistrationNumber.Should().Be(model.RegistrationNumber);
			editedVehicle.ImageUrl.Should().Be(model.ImageUrl);
		}

		[Test]
		public async Task CreateAndAddVehicleAsync_ShouldReturnFalse_IfDataIsInvalid()
		{
			var branchId = Guid.NewGuid();
			var fixedDate = new DateTime(2024, 10, 25);

			Branch branch = CreateBranch(branchId, fixedDate);

			Vehicle vehicle = branch.Vehicles.First();
			Make make = branch.Vehicles.First().Make;
			Engine engine = branch.Vehicles.First().Engine;
			Transmission transmission = branch.Vehicles.First().Transmission;
			VehicleType vehicleType = branch.Vehicles.First().VehicleType;

			AddVehicleViewModel model = new AddVehicleViewModel()
			{
				BranchId = Guid.NewGuid(),
				Color = "Added Vehicle Color",
				ImageUrl = "Added Vehicle ImgUrl",
				SeatsCount = 5,
				DoorsCount = 4,
				EngineId = engine.Id,
				Mileage = 1500,
				MakeId = make.Id,
				PricePerDay = vehicle.PricePerDay,
				Model = "Added Vehicle Model",
				RegistrationNumber = "New Number",
				TransmissionId = transmission.Id,
				VehicleTypeId = vehicleType.Id,
				Year = 2000,
			};

			var result = await _vehicleService.CreateAndAddVehicleAsync(model);

			result.Should().BeFalse();
		}

		[Test]
		public async Task DeleteVehicleAsync_ShouldDeleteVehicle_IfFound()
		{
			var branchId = Guid.NewGuid();
			var fixedDate = new DateTime(2024, 10, 25);

			Branch branch = CreateBranch(branchId, fixedDate);
			Vehicle vehicle = branch.Vehicles.First();
			Make make = branch.Vehicles.First().Make;
			Engine engine = branch.Vehicles.First().Engine;
			Transmission transmission = branch.Vehicles.First().Transmission;

			await _engineRepository.AddAsync(engine);
			await _transmissionRepository.AddAsync(transmission);
			await _makeRepository.AddAsync(make);
			await _vehicleRepository.AddAsync(vehicle);
			await _branchRepository.AddAsync(branch);

			EditVehicleListViewModel model = new EditVehicleListViewModel()
			{
				VehicleId = vehicle.Id
			};

			var result = await _vehicleService.DeleteVehicleAsync(model);
			var vehicleFound = await _vehicleRepository.GetByIdAsync(vehicle.Id);

			vehicleFound.IsDeleted.Should().BeTrue();
			result.Should().BeTrue();
		}

		[Test]
		public async Task GetAllVehiclesAsync_ShouldReturnNull_WhenInvalidGuid()
		{
			var result = await _vehicleService.GetAllVehiclesAsync("Invalid");

			result.Should().BeNull();
		}

		[Test]
		public async Task GetAllVehiclesAsync_ShouldReturnVehicles_IfFound()
		{
			var branchId = Guid.NewGuid();
			var fixedDate = new DateTime(2024, 10, 25);

			Branch branch = CreateBranch(branchId, fixedDate);

			Vehicle vehicle = branch.Vehicles.First();
			Make make = branch.Vehicles.First().Make;
			Engine engine = branch.Vehicles.First().Engine;
			Transmission transmission = branch.Vehicles.First().Transmission;
			VehicleType vehicleType = branch.Vehicles.First().VehicleType;


			await _engineRepository.AddAsync(engine);
			await _transmissionRepository.AddAsync(transmission);
			await _makeRepository.AddAsync(make);
			await _vehicleRepository.AddAsync(vehicle);
			await _branchRepository.AddAsync(branch);

			var result = await _vehicleService.GetAllVehiclesAsync(branch.Id.ToString());

			result.Should().HaveCount(1);
		}

		public Branch CreateBranch(Guid branchId, DateTime fixedDate)
		{
			return new Branch
			{
				Id = branchId,
				Name = "Test Branch",
				City = "Test City",
				Country = "Test Country",
				Address = "Test Address",
				Vehicles = new List<Vehicle>
						{
							new Vehicle
							{
								Id = Guid.NewGuid(),
								Make = new Make()
								{
									Name = "Test Make",
									Country = "Test Make Country",
								},
								Color = "Red",
								Model = "Camry",
								Transmission = new Transmission()
								{
									GearsCount = 4,
									Type = Data.Models.Enums.TransmissionType.Automatic
								},
								SeatsCount = 5,
								DoorsCount = 4,
								Year = 2022,
								Mileage = 15000,
								RegistrationNumber = "test-registrationNumber",
								ImageUrl = "test-img",
								VINNumber = "1HGBH41JXMN109186",
								AddedOn = fixedDate,
								BranchId = branchId,
								IsDeleted = false,
								Engine = new Engine()
								{
									CylindersCount = 4,
									Displacement = 2,
									FuelEfficiency = 5,
									FuelType = Data.Models.Enums.FuelType.Petrol,
									HP = 555,
									IsDeleted = false,
									IsElectric = false,
									Torque = 555
								},
								PricePerDay = 50.00m,
								VehicleType = new VehicleType()
								{
									Description = "Test VehicleType",
									ImageUrl = "test-vt-img",
									Name= "Test VehicleType Name"
								}
							}
						}
			};
		}
	}
}
