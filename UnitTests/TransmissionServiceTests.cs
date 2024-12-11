using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Data.Models;
using RentACar.Services.Data;
using RentACar.Tests.Repositories;
using RentACar.Web.ViewModels.Parts;

namespace RentACar.Tests
{
	public class TransmissionServiceTests
	{
		private TransmissionService _transmissionService;
		private RentACarDbContext _dbContext;
		private InMemoryRepository<Transmission, Guid> _repository;

		[SetUp]
		public void Setup()
		{
			var options = new DbContextOptionsBuilder<RentACarDbContext>()
				.UseInMemoryDatabase("TestDatabase")
				.Options;

			_dbContext = new RentACarDbContext(options);
			_repository = new InMemoryRepository<Transmission, Guid>(_dbContext);
			_transmissionService = new TransmissionService(_repository);
		}

		[TearDown]
		public void TearDown()
		{
			_dbContext.Database.EnsureDeleted();
			_dbContext.Database.EnsureCreated();
			_dbContext.Dispose();
		}

		[Test]
		public async Task CreateTransmissionAsync_ShouldReturnTrue_WhenValidDataPassed()
		{
			CreateTransmissionViewModel model = new CreateTransmissionViewModel()
			{
				GearsCount = 4,
				TransmissionType = Data.Models.Enums.TransmissionType.Manual,
			};

			var result = await _transmissionService.CreateTransmissionAsync(model);

			result.Should().BeTrue();
		}

		[Test]
		public async Task CreateTransmissionAsync_ShouldReturnFalse_WhenInvalidDataPassed()
		{
			CreateTransmissionViewModel model = new CreateTransmissionViewModel()
			{
				GearsCount = 0, //Less than GearsMinCountValidation
				TransmissionType = Data.Models.Enums.TransmissionType.Manual,
			};

			CreateTransmissionViewModel model2 = new CreateTransmissionViewModel()
			{
				GearsCount = 13, //More than GearsMinCountValidation
				TransmissionType = Data.Models.Enums.TransmissionType.Manual,
			};

			var result = await _transmissionService.CreateTransmissionAsync(model);
			var result2 = await _transmissionService.CreateTransmissionAsync(model2);

			result.Should().BeFalse();
			result2.Should().BeFalse();
		}

		[Test]
		public async Task DeleteTransmissionAsync_ShouldReturnTrue_IfValidAndExistingGuidIsPassed()
		{
			Guid transmissionId = Guid.NewGuid();
			Transmission transmission = new Transmission()
			{
				Id = transmissionId,
				GearsCount = 5,
				Type = Data.Models.Enums.TransmissionType.Automatic
			};

			await _repository.AddAsync(transmission);

			var result = await _transmissionService.DeleteTransmissionAsync(transmission.Id);
			var deletedTransmission = await _repository.GetByIdAsync(transmission.Id);

			result.Should().BeTrue();
			deletedTransmission.IsDeleted.Should().BeTrue();
		}

		[Test]
		public async Task DeleteTransmissionAsync_ShouldReturnFalse_IfInvalidAndExistingGuidIsPassed()
		{
			Guid transmissionId = Guid.NewGuid();
			Transmission transmission = new Transmission()
			{
				Id = transmissionId,
				GearsCount = 5,
				Type = Data.Models.Enums.TransmissionType.Automatic
			};

			await _repository.AddAsync(transmission);

			var result = await _transmissionService.DeleteTransmissionAsync(Guid.NewGuid());
			var deletedTransmission = await _repository.GetByIdAsync(transmission.Id);

			result.Should().BeFalse();
			deletedTransmission.IsDeleted.Should().BeFalse();
		}

		[Test]
		public async Task GetAllTransmissionsAsync_ShouldGetAllNotDeletedTransmissions()
		{
			int count = _repository.GetAllAttached().Where(t => t.IsDeleted == false).Count();

			var result = await _transmissionService.GetAllTransmissionsAsync();

			result.Count().Should().Be(count);
		}

		[Test]
		public async Task GetAllTransmissionsForDeletionAsync_ShouldGetAllNotDeletedTransmissions()
		{
			int count = _repository.GetAllAttached().Where(t => t.IsDeleted == false).Count();

			var result = await _transmissionService.GetAllTransmissionsForDeletionAsync();

			result.Count().Should().Be(count);
		}
	}
}
