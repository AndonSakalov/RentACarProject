using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Data.Models;
using RentACar.Services.Data;
using RentACar.Tests.Repositories;
using RentACar.Web.ViewModels.Parts;

namespace RentACar.Tests
{
	public class MakeServiceTests
	{
		private MakeService _makeService;
		private RentACarDbContext _dbContext;
		private InMemoryRepository<Make, Guid> _repository;

		[SetUp]
		public void Setup()
		{
			var options = new DbContextOptionsBuilder<RentACarDbContext>()
				.UseInMemoryDatabase("TestDatabase")
				.Options;

			_dbContext = new RentACarDbContext(options);
			_repository = new InMemoryRepository<Make, Guid>(_dbContext);
			_makeService = new MakeService(_repository);
		}

		[TearDown]
		public void TearDown()
		{
			_dbContext.Database.EnsureDeleted();
			_dbContext.Database.EnsureCreated();
			_dbContext.Dispose();
		}

		[Test]
		public async Task CreateMakeAsync_ShouldReturnTrue_WithCorrectData()
		{
			CreateMakeViewModel model = new CreateMakeViewModel()
			{
				Name = "Test Make Name",
				Country = "Test Country Name"
			};

			bool result = await _makeService.CreateMakeAsync(model);

			Make? createdMake = await _repository.GetAllAttached()
				.Where(m => m.Name == model.Name && m.Country == model.Country)
				.FirstOrDefaultAsync();

			result.Should().Be(true);
			createdMake.Should().NotBeNull();
		}
		[Test]
		public async Task CreateMakeAsync_ShouldReturnFalse_WithIncorrectData()
		{
			CreateMakeViewModel model = new CreateMakeViewModel()
			{
				Name = "A",
				Country = "B"
			};

			bool result = await _makeService.CreateMakeAsync(model);
			Make? createdMake = await _repository.GetAllAttached()
				.Where(m => m.Name == model.Name && m.Country == model.Country)
				.FirstOrDefaultAsync();

			result.Should().Be(false);
			createdMake.Should().BeNull();
		}

		[Test]
		public async Task DeleteMakeAsync_ShouldReturnTrue_WithCorrectMakeId()
		{
			Guid makeId = Guid.NewGuid();
			Make make = new Make()
			{
				Id = makeId,
				Name = "Test Make Name",
				Country = "Test Make Country"
			};

			await _repository.AddAsync(make);

			var result = await _makeService.DeleteMakeAsync(makeId);
			var deletedMake = await _repository.GetByIdAsync(makeId);

			deletedMake.IsDeleted.Should().Be(true);
			result.Should().Be(true);
		}

		[Test]
		public async Task DeleteMakeAsync_ShouldReturnFalse_WithIncorrectCorrectMakeId()
		{
			Guid testId = Guid.NewGuid();
			Make make = new Make()
			{
				Id = Guid.NewGuid(),
				Name = "Test Make Name",
				Country = "Test Make Country"
			};

			await _repository.AddAsync(make);

			var result = await _makeService.DeleteMakeAsync(testId);
			var deletedMake = await _repository.GetByIdAsync(make.Id);

			deletedMake.IsDeleted.Should().Be(false);
			result.Should().Be(false);
		}

		[Test]
		public async Task GetAllMakesAsync_Should_ReturnAllMakes()
		{
			int count = _repository.GetAllAttached().Count();

			var result = await _makeService.GetAllMakesAsync();

			result.Count().Should().Be(count);
		}

		[Test]
		public async Task GetAllMakesForDeletionAsync_Should_ReturnAllMakes()
		{
			int count = _repository.GetAllAttached().Count();

			var result = await _makeService.GetAllMakesForDeletionAsync();

			result.Count().Should().Be(count);
		}
	}
}
