using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Data.Models;
using RentACar.Services.Data;
using RentACar.Tests.Repositories;
using RentACar.Web.ViewModels;
using static RentACar.Common.EntityValidationConstants.Branch;
namespace RentACar.Tests
{
	[TestFixture]
	public class BranchServiceTests
	{
		private BranchService _branchService;
		private RentACarDbContext _dbContext;
		private InMemoryRepository<Branch, Guid> _repository;

		[SetUp]
		public void Setup()
		{
			var options = new DbContextOptionsBuilder<RentACarDbContext>()
				.UseInMemoryDatabase("TestDatabase")
				.Options;

			_dbContext = new RentACarDbContext(options);
			_repository = new InMemoryRepository<Branch, Guid>(_dbContext);
			_branchService = new BranchService(_repository);
		}

		[TearDown]
		public void TearDown()
		{
			_dbContext.Database.EnsureDeleted();
			_dbContext.Database.EnsureCreated();
			_dbContext.Dispose();
		}

		[Test]
		public async Task GetAllOrderedByLocationAsync_ShouldReturnBranch_WhenBranchExists()
		{
			// Arrange
			var branchId = Guid.NewGuid();
			var fixedDate = new DateTime(2024, 10, 25);

			var branch = CreateBranch(branchId, fixedDate);

			_repository.Add(branch);

			var viewModel = new SearchBranchViewModel
			{
				City = "Test City",
				PickUpDate = DateTime.Now.ToString(RentalDateFormat),
				ReturnDate = DateTime.Now.AddDays(3).ToString(RentalDateFormat),
			};

			// Act
			var result = await _branchService.GetAllOrderedByLocationAsync(viewModel);

			// Assert
			result.Should().NotBeNull();
			result.First().VehiclesCount.Should().Be(1);
			result.First().Name.Should().Be("Test Branch");
			result.First().Address.Should().Be("Test Address");
			result.First().Country.Should().Be("Test Country");
		}

		[Test]
		public async Task GetAllVehicleTypesAsync_ShouldReturnVehicleTypes_WhenBranchExistsAndDatesAreValid()
		{
			// Arrange
			var branchId = Guid.NewGuid();
			var fixedDate = new DateTime(2024, 10, 25);

			var branch = CreateBranch(branchId, fixedDate);

			_repository.Add(branch);

			var pickupDate = fixedDate.AddDays(1).ToString(RentalDateFormat);
			var returnDate = fixedDate.AddDays(3).ToString(RentalDateFormat);

			// Act
			var result = await _branchService.GetAllVehicleTypesAsync(branchId.ToString(), pickupDate, returnDate);

			// Assert
			result.Should().NotBeNull();
			result.Should().HaveCount(1);
			result.First().Name.Should().Be("Test VehicleType Name");
			result.First().Description.Should().Be("Test VehicleType");
			result.First().PickUpDate.Should().Be(pickupDate);
			result.First().ReturnDate.Should().Be(returnDate);
		}

		[Test]
		public async Task GetAllVehicleTypesAsync_ShouldReturnNull_WhenBranchIdIsInvalid()
		{
			// Arrange
			var branchId = Guid.NewGuid();
			var fixedDate = new DateTime(2024, 10, 25);

			var branch = CreateBranch(branchId, fixedDate);

			_repository.Add(branch);

			var pickupDate = fixedDate.AddDays(1).ToString(RentalDateFormat);
			var returnDate = fixedDate.AddDays(3).ToString(RentalDateFormat);

			var invalidBranchId = "invalidBranchId";

			// Act
			var result = await _branchService.GetAllVehicleTypesAsync(invalidBranchId, pickupDate, returnDate);

			// Assert
			result.Should().BeNull();
		}

		[Test]
		public async Task GetAllOrderedByLocationAsync_ShouldReturnNullOrEmpty_WhenNoBranchIsFoundInCity()
		{
			// Arrange
			var branchId = Guid.NewGuid();
			var fixedDate = new DateTime(2024, 10, 25);

			var branch = CreateBranch(branchId, fixedDate);

			_repository.Add(branch);

			var viewModel = new SearchBranchViewModel
			{
				City = "Invalid City",
				PickUpDate = DateTime.Now.ToString(),
				ReturnDate = DateTime.Now.AddDays(3).ToString(),
			};

			// Act
			var result = await _branchService.GetAllOrderedByLocationAsync(viewModel);

			// Assert
			result.Should().BeNullOrEmpty();
		}

		[Test]
		public async Task GetAllOrderedByLocationAsync_ShouldReturnNullOrEmpty_WhenDatesAreInvalid()
		{
			// Arrange
			var branchId = Guid.NewGuid();
			var fixedDate = new DateTime(2024, 10, 25);

			var branch = CreateBranch(branchId, fixedDate);

			_repository.Add(branch);

			var viewModel = new SearchBranchViewModel
			{
				City = "Test City",
				PickUpDate = "Invalid Date",
				ReturnDate = "Invalid Date",
			};

			// Act
			var result = await _branchService.GetAllOrderedByLocationAsync(viewModel);

			// Assert
			result.Should().BeNullOrEmpty();
		}

		[Test]
		public async Task GetAllVehicleTypesAsync_ShouldReturnNull_WhenBranchExistsAndDatesAreInvalid()
		{
			// Arrange
			var branchId = Guid.NewGuid();
			var fixedDate = new DateTime(2024, 10, 25);

			var branch = CreateBranch(branchId, fixedDate);

			_repository.Add(branch);

			var pickupDate = "Invalid Date";
			var returnDate = "Invalid Date";

			// Act
			var result = await _branchService.GetAllVehicleTypesAsync(branchId.ToString(), pickupDate, returnDate);

			// Assert
			result.Should().BeNull();
		}

		[Test]
		public async Task GetAllVehicleTypesAsync_ShouldReturnNull_WhenBranchExistsAndDatesAreTheSame()
		{
			// Arrange
			var branchId = Guid.NewGuid();
			var fixedDate = new DateTime(2024, 10, 25);

			var branch = CreateBranch(branchId, fixedDate);

			_repository.Add(branch);

			var pickupDate = fixedDate.AddDays(1).ToString(RentalDateFormat);
			var returnDate = fixedDate.AddDays(1).ToString(RentalDateFormat);

			// Act
			var result = await _branchService.GetAllVehicleTypesAsync(branchId.ToString(), pickupDate, returnDate);

			// Assert
			result.Should().BeNull();
		}

		[Test]
		public async Task GetAllVehicleTypesAsync_ShouldReturnNull_WhenBranchExistsAndPickupDateIsBiggerThanReturnDate()
		{
			// Arrange
			var branchId = Guid.NewGuid();
			var fixedDate = new DateTime(2024, 10, 25);

			var branch = CreateBranch(branchId, fixedDate);

			_repository.Add(branch);

			var pickupDate = fixedDate.AddDays(2).ToString(RentalDateFormat);
			var returnDate = fixedDate.AddDays(1).ToString(RentalDateFormat);

			// Act
			var result = await _branchService.GetAllVehicleTypesAsync(branchId.ToString(), pickupDate, returnDate);

			// Assert
			result.Should().BeNull();
		}

		[Test]
		public async Task GetAllVehiclesForCurrentBranchAsync_ShouldReturnVehicles_WhenAllDataIsValid()
		{
			var branchId = Guid.NewGuid();
			var fixedDate = new DateTime(2024, 10, 25);

			var branch = CreateBranch(branchId, fixedDate);

			_repository.Add(branch);

			var pickupDate = fixedDate.AddDays(1).ToString(RentalDateFormat);
			var returnDate = fixedDate.AddDays(3).ToString(RentalDateFormat);

			var result = await _branchService.GetAllVehiclesForCurrentBranchAsync(branchId.ToString(), pickupDate, returnDate, branch.Vehicles.First().VehicleType.Name);

			result.Should().NotBeNull();
			result.First().Name.Should().Be($"Test Make {branch.Vehicles.First().Model}");
		}

		[Test]
		public async Task GetAllVehiclesForCurrentBranchAsync_ShouldReturnNull_WhenAllDatesAreInvalid()
		{
			var branchId = Guid.NewGuid();
			var fixedDate = new DateTime(2024, 10, 25);

			var branch = CreateBranch(branchId, fixedDate);

			_repository.Add(branch);
			//pickupDate > returnDate

			var pickupDate = fixedDate.AddDays(3).ToString(RentalDateFormat);
			var returnDate = fixedDate.AddDays(1).ToString(RentalDateFormat);

			var result = await _branchService.GetAllVehiclesForCurrentBranchAsync(branchId.ToString(), pickupDate, returnDate, branch.Vehicles.First().VehicleType.Name);

			result.Should().BeNull();

			//pickupDate == returnDate
			pickupDate = fixedDate.AddDays(1).ToString(RentalDateFormat);
			returnDate = fixedDate.AddDays(1).ToString(RentalDateFormat);

			result = await _branchService.GetAllVehiclesForCurrentBranchAsync(branchId.ToString(), pickupDate, returnDate, branch.Vehicles.First().VehicleType.Name);

			result.Should().BeNull();
		}

		[Test]
		public async Task GetAllVehiclesForCurrentBranchAsync_ShouldReturnNull_WhenBranchIdIsInvalid()
		{
			var branchId = Guid.NewGuid();
			var fixedDate = new DateTime(2024, 10, 25);

			var branch = CreateBranch(branchId, fixedDate);

			_repository.Add(branch);

			var pickupDate = fixedDate.AddDays(1).ToString(RentalDateFormat);
			var returnDate = fixedDate.AddDays(3).ToString(RentalDateFormat);

			string invalidBranchId = "Invalid Branch Id";

			var result = await _branchService.GetAllVehiclesForCurrentBranchAsync(invalidBranchId, pickupDate, returnDate, branch.Vehicles.First().VehicleType.Name);

			result.Should().BeNull();
		}

		[Test]
		public async Task GetAllVehiclesForCurrentBranchAsync_ShouldReturnEmpty_WhenVehicleTypeIsNotFound()
		{
			var branchId = Guid.NewGuid();
			var fixedDate = new DateTime(2024, 10, 25);

			var branch = CreateBranch(branchId, fixedDate);

			_repository.Add(branch);

			var pickupDate = fixedDate.AddDays(1).ToString(RentalDateFormat);
			var returnDate = fixedDate.AddDays(3).ToString(RentalDateFormat);

			string invalidVehicleType = "Invalid Vehicle Type";

			var result = await _branchService.GetAllVehiclesForCurrentBranchAsync(branch.Id.ToString(), pickupDate, returnDate, invalidVehicleType);

			result.Should().BeEmpty();
		}

		[Test]
		public async Task GetAllVehiclesForCurrentBranchAsync_ShouldReturnEmpty_WhenAllVehiclesAreInRentalAndDatesOverlap()
		{
			var branchId = Guid.NewGuid();
			var fixedDate = new DateTime(2024, 10, 25);

			var branch = CreateBranch(branchId, fixedDate);

			_repository.Add(branch);

			var pickupDate = fixedDate.AddDays(1).ToString(RentalDateFormat);
			var returnDate = fixedDate.AddDays(3).ToString(RentalDateFormat);

			branch.Vehicles.First().RentalId = Guid.NewGuid();
			branch.Vehicles.First().Rental = new Rental()
			{
				EndDate = fixedDate.AddDays(2)
			};


			await _repository.UpdateAsync(branch);

			var result = await _branchService.GetAllVehiclesForCurrentBranchAsync(branch.Id.ToString(), pickupDate, returnDate, branch.Vehicles.First().VehicleType.Name);

			result.Should().BeEmpty();
		}

		[Test]
		public async Task GetAllBranchesAsync_ShouldReturnBranches_IfAny()
		{
			var branchId = Guid.NewGuid();
			var fixedDate = new DateTime(2024, 10, 25);

			var branch = CreateBranch(branchId, fixedDate);

			_repository.Add(branch);

			var result = await _branchService.GetAllBranchesAsync();

			result.Should().NotBeNull();
			result.First().Name.Should().Be("Test Branch");
			result.First().Address.Should().Be("Test Address");
			result.First().Country.Should().Be("Test Country");
		}

		[Test]
		public async Task StaffSearchBranchesAsync_ShouldReturnBranch_IfCityIsCorrect()
		{
			var branchId = Guid.NewGuid();
			var fixedDate = new DateTime(2024, 10, 25);

			var branch = CreateBranch(branchId, fixedDate);

			_repository.Add(branch);

			StaffSearchViewModel model = new StaffSearchViewModel()
			{
				City = branch.City,
			};

			var result = await _branchService.StaffSearchBranchesAsync(model);

			result.Should().NotBeNull();
			result.First().Name.Should().Be("Test Branch");
			result.First().Address.Should().Be("Test Address");
			result.First().Id.Should().Be(branchId);
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
								EngineId = Guid.NewGuid(),
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
