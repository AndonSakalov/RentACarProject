using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Data.Models;
using RentACar.Services.Data;
using RentACar.Tests.Repositories;

namespace RentACar.Tests
{
	public class UserAccountInfoServiceTests
	{
		private UserAccountInfoService _userAccountInfoService;
		private RentACarDbContext _dbContext;
		private InMemoryRepository<ApplicationUser, Guid> _userRepository;
		private InMemoryRepository<Rental, Guid> _rentalRepository;
		private InMemoryRepository<Vehicle, Guid> _vehicleRepository;

		[SetUp]
		public void Setup()
		{
			var options = new DbContextOptionsBuilder<RentACarDbContext>()
				.UseInMemoryDatabase("TestDatabase")
				.Options;

			_dbContext = new RentACarDbContext(options);
			_rentalRepository = new InMemoryRepository<Rental, Guid>(_dbContext);
			_vehicleRepository = new InMemoryRepository<Vehicle, Guid>(_dbContext);
			_userRepository = new InMemoryRepository<ApplicationUser, Guid>(_dbContext);
			_userAccountInfoService = new UserAccountInfoService(
												_userRepository,
												_rentalRepository,
												_vehicleRepository);
		}

		[TearDown]
		public void TearDown()
		{
			_dbContext.Database.EnsureDeleted();
			_dbContext.Database.EnsureCreated();
			_dbContext.Dispose();
		}

		[Test]
		public async Task GetUserInfoAsync_ShouldReturnFalse_IfInvalidOrNotExistingUserId()
		{
			var fixedDate = new DateTime(2024, 10, 25);
			Guid vehicleId = Guid.NewGuid();
			Guid rentalId = Guid.NewGuid();
			Guid branchId = Guid.NewGuid();
			Guid customerId = Guid.NewGuid();
			Guid paymentId = Guid.NewGuid();

			var passwordHasher = new PasswordHasher<ApplicationUser>();

			Payment payment = new Payment()
			{
				Id = paymentId
			};

			ApplicationUser customer = new ApplicationUser()
			{
				Id = customerId,
				UserName = "TestUsername",
				Email = "testemail@abv.bg"
			};
			customer.PasswordHash = passwordHasher.HashPassword(customer, "TestPassword123");

			Vehicle vehicle = new Vehicle
			{
				Id = vehicleId,
				Make = new Make()
				{
					Name = "Test Make",
					Country = "Test Make Country",
				},
				Color = "Test Color",
				Model = "Test Model",
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
					Name = "Test VehicleType Name"
				},
				RentalId = rentalId
			};

			Rental rental = new Rental()
			{
				Id = rentalId,
				CustomerId = customerId,
				Customer = customer,
				StartDate = fixedDate,
				EndDate = fixedDate.AddDays(3),
				PaymentId = paymentId,
				Payment = payment,
				TotalPrice = 200,
				VehicleId = vehicleId,
				Vehicle = vehicle,
				IsActive = true
			};

			await _vehicleRepository.AddAsync(vehicle);
			await _rentalRepository.AddAsync(rental);

			var result = await _userAccountInfoService.GetUserInfoAsync("InvalidId", 1, 3);

			var result2 = await _userAccountInfoService.GetUserInfoAsync(Guid.NewGuid().ToString(), 1, 3);

			result.isSuccessful.Should().BeFalse();
			result2.isSuccessful.Should().BeFalse();
		}


		[Test]
		public async Task GetUserInfoAsync_ShouldReturnTrue_WithValidData()
		{
			var fixedDate = new DateTime(2024, 10, 25);
			Guid vehicleId = Guid.NewGuid();
			Guid rentalId = Guid.NewGuid();
			Guid branchId = Guid.NewGuid();
			Guid customerId = Guid.NewGuid();
			Guid paymentId = Guid.NewGuid();

			var passwordHasher = new PasswordHasher<ApplicationUser>();

			Payment payment = new Payment()
			{
				Id = paymentId
			};

			ApplicationUser customer = new ApplicationUser()
			{
				Id = customerId,
				UserName = "TestUsername",
				Email = "testemail@abv.bg"
			};
			customer.PasswordHash = passwordHasher.HashPassword(customer, "TestPassword123");

			Vehicle vehicle = new Vehicle
			{
				Id = vehicleId,
				Make = new Make()
				{
					Name = "Test Make",
					Country = "Test Make Country",
				},
				Color = "Test Color",
				Model = "Test Model",
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
					Name = "Test VehicleType Name"
				},
				RentalId = rentalId
			};

			Rental rental = new Rental()
			{
				Id = rentalId,
				CustomerId = customerId,
				Customer = customer,
				StartDate = fixedDate,
				EndDate = fixedDate.AddDays(3),
				PaymentId = paymentId,
				Payment = payment,
				TotalPrice = 200,
				VehicleId = vehicleId,
				Vehicle = vehicle,
				IsActive = true
			};

			await _vehicleRepository.AddAsync(vehicle);
			await _rentalRepository.AddAsync(rental);

			var result = await _userAccountInfoService.GetUserInfoAsync(customer.Id.ToString(), 1, 3);


			result.isSuccessful.Should().BeTrue();
			result.model.Should().NotBeNull();
		}
	}
}
