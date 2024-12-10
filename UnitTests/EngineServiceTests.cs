using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Data.Models;
using RentACar.Services.Data;
using RentACar.Tests.Repositories;
using RentACar.Web.ViewModels.Parts;

namespace RentACar.Tests
{
	public class EngineServiceTests
	{
		private EngineService _engineService;
		private RentACarDbContext _dbContext;
		private InMemoryRepository<Engine, Guid> _repository;

		[SetUp]
		public void Setup()
		{
			var options = new DbContextOptionsBuilder<RentACarDbContext>()
				.UseInMemoryDatabase("TestDatabase")
				.Options;

			_dbContext = new RentACarDbContext(options);
			_repository = new InMemoryRepository<Engine, Guid>(_dbContext);
			_engineService = new EngineService(_repository);
		}

		[TearDown]
		public void TearDown()
		{
			_dbContext.Database.EnsureDeleted();
			_dbContext.Database.EnsureCreated();
			_dbContext.Dispose();
		}

		[Test]
		public async Task CreateAndAddEngineAsync_WorksWith_NotTemperedData()
		{
			CreateEngineViewModel model = new CreateEngineViewModel()
			{
				HP = 250,
				CylindersCount = 4,
				Displacement = 2,
				FuelEfficiency = 6,
				FuelType = Data.Models.Enums.FuelType.Diesel,
				Torque = 350,
				Description = "Test Description for this engine!"
			};

			bool result = await _engineService.CreateAndAddEngineAsync(model);

			Engine addedEngine = _repository.GetAllAttached()
				.Where(e => e.Description == model.Description)
				.First();

			result.Should().Be(true);
			addedEngine.HP.Should().Be(model.HP);
			addedEngine.CylindersCount.Should().Be(model.CylindersCount);
			addedEngine.Displacement.Should().Be(model.Displacement);
			addedEngine.FuelEfficiency.Should().Be(model.FuelEfficiency);
			addedEngine.FuelType.Should().Be(model.FuelType);
			addedEngine.Torque.Should().Be(model.Torque);
		}

		[Test]
		public async Task CreateAndAddEngineAsync_ReturnsFalse_WithTemperedData()
		{
			CreateEngineViewModel model = new CreateEngineViewModel()
			{
				HP = 500000,
				CylindersCount = 3,
				Displacement = 2,
				FuelEfficiency = 6,
				FuelType = Data.Models.Enums.FuelType.Diesel,
				Torque = 350
			};

			bool result = await _engineService.CreateAndAddEngineAsync(model);

			Engine? addedEngine = _repository.GetAllAttached()
				.FirstOrDefault();

			result.Should().Be(false);
			addedEngine.Should().Be(null);
		}

		[Test]
		public async Task DeleteEngineAsync_ShouldReturnTrue_WhenValidEngineId_IsPassed()
		{
			Guid engineId = Guid.NewGuid();
			Engine engine = new Engine()
			{
				Id = engineId,
				HP = 250,
				CylindersCount = 4,
				Displacement = 2,
				FuelEfficiency = 6,
				FuelType = Data.Models.Enums.FuelType.Diesel,
				Torque = 350,
				Description = "Test Description!",
				IsElectric = false
			};
			await _repository.AddAsync(engine);

			bool result = await _engineService.DeleteEngineAsync(engineId);

			result.Should().Be(true);
		}

		[Test]
		public async Task GetAllEnginesAsync_GetsAllEnginesFromTheDbCorrectly()
		{
			Guid engineId = Guid.NewGuid();
			Engine engine = new Engine()
			{
				Id = engineId,
				HP = 250,
				CylindersCount = 4,
				Displacement = 2,
				FuelEfficiency = 6,
				FuelType = Data.Models.Enums.FuelType.Diesel,
				Torque = 350,
				Description = "Test Description!",
				IsElectric = false
			};
			_repository.Add(engine);

			var result = await _engineService.GetAllEnginesAsync();

			result.Should().HaveCount(1);
			result.First().Id.Should().Be(engineId);
		}

		[Test]
		public async Task GetAllEnginesForDeletionAsync_GetsAllEnginesFromTheDbCorrectly()
		{
			Guid engineId = Guid.NewGuid();
			Engine engine = new Engine()
			{
				Id = engineId,
				HP = 250,
				CylindersCount = 4,
				Displacement = 2,
				FuelEfficiency = 6,
				FuelType = Data.Models.Enums.FuelType.Diesel,
				Torque = 350,
				Description = "Test Description!",
				IsElectric = false
			};
			_repository.Add(engine);

			var result = await _engineService.GetAllEnginesForDeletionAsync();
			try
			{


				var z = result.Where(e => e.HP == 150).FirstOrDefault();

				var change = await _repository.GetByIdAsync(z.Id);
				change.HP = 157;
				await _repository.UpdateAsync(change);
			}
			catch (Exception)
			{

				throw;
			}


			result.Should().HaveCount(1);
			result.First().Id.Should().Be(engineId);
		}
	}
}
