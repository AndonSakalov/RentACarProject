using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Data.Models;
using RentACar.Services.Data;
using RentACar.Tests.Repositories;
using RentACar.Web.ViewModels.Rental;

namespace RentACar.Tests
{
	public class RentalServiceTests
	{
		private RentalService _rentalService;
		private RentACarDbContext _dbContext;
		private InMemoryRepository<Rental, Guid> _repository;
		private InMemoryRepository<Vehicle, Guid> _vehicleRepository;
		private InMemoryRepository<Branch, Guid> _branchRepository;
		private InMemoryRepository<Reservation, Guid> _reservationRepository;
		private InMemoryRepository<ApplicationUser, Guid> _applicationUserRepository;

		[SetUp]
		public void Setup()
		{
			var options = new DbContextOptionsBuilder<RentACarDbContext>()
				.UseInMemoryDatabase("TestDatabase")
				.Options;

			_dbContext = new RentACarDbContext(options);
			_repository = new InMemoryRepository<Rental, Guid>(_dbContext);
			_vehicleRepository = new InMemoryRepository<Vehicle, Guid>(_dbContext);
			_branchRepository = new InMemoryRepository<Branch, Guid>(_dbContext);
			_reservationRepository = new InMemoryRepository<Reservation, Guid>(_dbContext);
			_applicationUserRepository = new InMemoryRepository<ApplicationUser, Guid>(_dbContext);
			_rentalService = new RentalService(
												_repository,
												_vehicleRepository,
												_branchRepository,
												_reservationRepository,
												_applicationUserRepository);
		}

		[TearDown]
		public void TearDown()
		{
			_dbContext.Database.EnsureDeleted();
			_dbContext.Database.EnsureCreated();
			_dbContext.Dispose();
		}

		[Test]
		public async Task EndRentalAsync_WorksCorrectly_WithCorrectData()
		{
			var validModel = await SeedValidDataIntoDb();

			var result = await _rentalService.EndRentalAsync(validModel);

			result.Should().Be(true);
		}

		[Test]
		public async Task EndRentalAsync_ReturnsFalse_WithIncorrectData()
		{
			var testModel1 = await SeedValidDataIntoDb();
			testModel1.RentalId = Guid.Empty;
			var result = await _rentalService.EndRentalAsync(testModel1);
			result.Should().Be(false);

			var testModel2 = await SeedValidDataIntoDb();
			testModel2.VehicleId = Guid.Empty;
			result = await _rentalService.EndRentalAsync(testModel2);
			result.Should().Be(false);

			var testModel3 = await SeedValidDataIntoDb();
			testModel3.CustomerId = Guid.Empty;
			result = await _rentalService.EndRentalAsync(testModel3);
			result.Should().Be(false);
		}

		[Test]
		public async Task GetAllRentalsForBranchAsync_IsNotSuccessful_WithInvalidBranchId()
		{
			var model = await SeedValidDataIntoDb();

			var result = await _rentalService.GetAllRentalsForBranchAsync("InvalidGuid");

			result.isSuccessful.Should().Be(false);
			result.rentals.Should().Be(null);
		}

		[Test]
		public async Task GetAllRentalsForBranchAsync_IsNotSuccessful_WithNotExistingBranchId()
		{
			var model = await SeedValidDataIntoDb();

			var result = await _rentalService.GetAllRentalsForBranchAsync(Guid.NewGuid().ToString());

			result.isSuccessful.Should().Be(false);
			result.rentals.Should().Be(null);
		}

		[Test]
		public async Task GetAllRentalsForBranchAsync_IsSuccessful_WithValidAndExistingBranchId()
		{
			var model = await SeedValidDataIntoDb();

			var result = await _rentalService.GetAllRentalsForBranchAsync(model.BranchId.ToString());

			result.isSuccessful.Should().Be(true);
			result.rentals.Should().NotBeNull();
			result.rentals!.Rentals.Should().HaveCount(1);
		}

		[Test]
		public async Task GetReservationsAsync_ShouldReturnFalse_IfInvalidOrNotExistingBranchId()
		{
			var model = await SeedValidDataIntoDb();

			var result = _rentalService.GetReservationsAsync("invalid Guid");

			result.Result.isIdValid.Should().BeFalse();
			result.Result.dict.Should().BeNull();

			result = _rentalService.GetReservationsAsync(Guid.NewGuid().ToString());

			result.Result.isIdValid.Should().BeFalse();
			result.Result.dict.Should().BeNull();
		}

		[Test]
		public async Task GetReservationsAsync_ShouldReturnTrue_IfBranchIdIsValidAndExists()
		{
			var model = await SeedValidDataIntoDb();

			var result = _rentalService.GetReservationsAsync(model.BranchId.ToString());

			result.Result.isIdValid.Should().BeTrue();
			result.Result.dict.Should().NotBeNull();
		}

		[Test]
		public async Task GetVehicleRentalToRemoveAsync_ShouldBeSuccessful_WhenRentalIdAndVehicleIdAreValid()
		{
			var model = await SeedValidDataIntoDb();

			var result = _rentalService.GetVehicleRentalToRemoveAsync(model.RentalId.ToString(), model.VehicleId.ToString());

			result.Result.isSuccessful.Should().BeTrue();
			result.Result.model.Should().NotBeNull();
		}

		[Test]
		public async Task GetVehicleRentalToRemoveAsync_ShouldNotBeSuccessful_WhenRentalIdAndVehicleIdAreInvalidOrNotExisting()
		{
			var model = await SeedValidDataIntoDb();

			var result = _rentalService.GetVehicleRentalToRemoveAsync("InvalidRentalId", "InvalidVehicleId");

			result.Result.isSuccessful.Should().BeFalse();
			result.Result.model.Should().BeNull();

			result = _rentalService.GetVehicleRentalToRemoveAsync(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

			result.Result.isSuccessful.Should().BeFalse();
			result.Result.model.Should().BeNull();
		}

		[Test]
		public async Task ReserveVehicleAsync_ShouldBeSuccessful_WithValidData()
		{
			var fixedDate = new DateTime(2024, 10, 25);
			Guid vehicleId = Guid.NewGuid();
			Guid rentalId = Guid.NewGuid();
			Guid branchId = Guid.NewGuid();
			Guid customerId = Guid.NewGuid();
			Guid paymentId = Guid.NewGuid();

			var passwordHasher = new PasswordHasher<ApplicationUser>();

			Branch branch = new Branch()
			{
				Id = branchId,
				Name = "Test Branch",
				City = "Test City",
				Country = "Test Country",
				Address = "Test Address",
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
			};

			await _vehicleRepository.AddAsync(vehicle);
			await _branchRepository.AddAsync(branch);
			await _applicationUserRepository.AddAsync(customer);

			DealViewModel model = new DealViewModel()
			{
				Branch = new RentalBranchViewModel()
				{
					Address = branch.Address,
					City = branch.City,
					Country = branch.Country,
					Id = branch.Id,
					Name = branch.Name
				},
				PickupDate = fixedDate,
				Price = 200,
				ReturnDate = fixedDate.AddDays(3),
				Vehicle = new RentalVehicleViewModel()
				{
					Id = vehicle.Id,
					SeatsCount = vehicle.SeatsCount,
					ImageUrl = vehicle.ImageUrl,
					Name = "Test Vehicle Name",
					TransmissionType = "Manual"
				},
				VehicleType = vehicle.VehicleType!.ToString()
			};

			var result = await _rentalService.ReserveVehicleAsync(model, customer.Id.ToString());

			result.Should().BeTrue();
		}

		[Test]
		public async Task ReserveVehicleAsync_ShouldNotBeSuccessful_WithInvalidData()
		{
			var fixedDate = new DateTime(2024, 10, 25);
			Guid vehicleId = Guid.NewGuid();
			Guid rentalId = Guid.NewGuid();
			Guid branchId = Guid.NewGuid();
			Guid customerId = Guid.NewGuid();
			Guid paymentId = Guid.NewGuid();

			var passwordHasher = new PasswordHasher<ApplicationUser>();

			Branch branch = new Branch()
			{
				Id = branchId,
				Name = "Test Branch",
				City = "Test City",
				Country = "Test Country",
				Address = "Test Address",
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
			};

			await _vehicleRepository.AddAsync(vehicle);
			await _branchRepository.AddAsync(branch);
			await _applicationUserRepository.AddAsync(customer);

			DealViewModel model = new DealViewModel()
			{
				Branch = new RentalBranchViewModel()
				{
					Address = branch.Address,
					City = branch.City,
					Country = branch.Country,
					Id = branch.Id,
					Name = branch.Name
				},
				PickupDate = fixedDate,
				Price = 200,
				ReturnDate = fixedDate.AddDays(3),
				Vehicle = new RentalVehicleViewModel()
				{
					Id = vehicle.Id,
					SeatsCount = vehicle.SeatsCount,
					ImageUrl = vehicle.ImageUrl,
					Name = "Test Vehicle Name",
					TransmissionType = "Manual"
				},
				VehicleType = vehicle.VehicleType!.ToString()
			};

			var result = await _rentalService.ReserveVehicleAsync(model, Guid.NewGuid().ToString());
			result.Should().BeFalse();

			var result2 = await _rentalService.ReserveVehicleAsync(model, "InvalidUserId");
			result2.Should().BeFalse();

			model.Vehicle.Id = Guid.NewGuid();

			var result3 = await _rentalService.ReserveVehicleAsync(model, customer.Id.ToString());
			result3.Should().BeFalse();
		}

		[Test]
		public async Task SetReservationAsRental_ShouldReturnTrue_WithValidData()
		{
			var data = await SeedValidDataIntoDb();
			var fixedDate = new DateTime(2024, 10, 25);

			Reservation reservation = new Reservation()
			{
				Id = Guid.NewGuid(),
				IsActive = false,
				PickUpDate = fixedDate,
				ReturnDate = fixedDate.AddDays(3),
				Price = 200,
				UserId = data.CustomerId,
				VehicleId = data.VehicleId,
			};

			await _reservationRepository.AddAsync(reservation);

			Vehicle vehicle = await _vehicleRepository.GetByIdAsync(data.VehicleId);
			vehicle.RentalId = null;
			vehicle.IsRented = false;
			vehicle.Rental = null;
			await _vehicleRepository.UpdateAsync(vehicle);

			ConfirmReservationViewModel model = new ConfirmReservationViewModel()
			{
				BranchId = data.BranchId,
				CustomerId = data.CustomerId,
				PickupDate = fixedDate,
				ReturnDate = fixedDate.AddDays(3),
				Price = 200,
				ReservationId = reservation.Id,
				VehicleId = data.VehicleId
			};

			var result = await _rentalService.SetReservationAsRental(model);

			result.isSuccessful.Should().BeTrue();
			result.isVehicleFree.Should().BeTrue();

		}

		[Test]
		public async Task SetReservationAsRental_ShouldReturnFalse_WhenThereIsOngoingRental()
		{
			var data = await SeedValidDataIntoDb();
			var fixedDate = new DateTime(2024, 10, 25);

			Reservation reservation = new Reservation()
			{
				Id = Guid.NewGuid(),
				IsActive = false,
				PickUpDate = fixedDate,
				ReturnDate = fixedDate.AddDays(3),
				Price = 200,
				UserId = data.CustomerId,
				VehicleId = data.VehicleId,
			};

			await _reservationRepository.AddAsync(reservation);

			ConfirmReservationViewModel model = new ConfirmReservationViewModel()
			{
				BranchId = data.BranchId,
				CustomerId = data.CustomerId,
				PickupDate = fixedDate,
				ReturnDate = fixedDate.AddDays(3),
				Price = 200,
				ReservationId = reservation.Id,
				VehicleId = data.VehicleId
			};

			var result = await _rentalService.SetReservationAsRental(model);

			result.isSuccessful.Should().BeFalse();
			result.isVehicleFree.Should().BeFalse();

		}

		public async Task<EndRentalViewModel> SeedValidDataIntoDb()
		{
			try
			{
				var fixedDate = new DateTime(2024, 10, 25);
				Guid vehicleId = Guid.NewGuid();
				Guid rentalId = Guid.NewGuid();
				Guid branchId = Guid.NewGuid();
				Guid customerId = Guid.NewGuid();
				Guid paymentId = Guid.NewGuid();

				var passwordHasher = new PasswordHasher<ApplicationUser>();

				Branch branch = new Branch()
				{
					Id = branchId,
					Name = "Test Branch",
					City = "Test City",
					Country = "Test Country",
					Address = "Test Address",
				};

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
				await _repository.AddAsync(rental);
				await _branchRepository.AddAsync(branch);
				//await _applicationUserRepository.AddAsync(customer);

				EndRentalViewModel model = new EndRentalViewModel()
				{
					CustomerId = customer.Id,
					RentalId = rental.Id,
					AmountRequired = rental.TotalPrice,
					BranchId = branch.Id,
					CustomerEmail = customer.Email,
					KilometersTraveled = 0,
					PaymentAmount = rental.TotalPrice,
					PaymentId = payment.Id,
					PaymentMethod = Data.Models.Enums.PaymentMethod.Cash,
					PaymentStatus = Data.Models.Enums.PaymentStatus.Paid,
					VehicleId = vehicle.Id,
					VehicleName = "Test Vehicle Name"
				};

				return model;
			}
			catch (Exception)
			{

				return null;
			}
		}
	}
}