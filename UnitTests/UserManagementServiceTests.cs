using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Data.Models;
using RentACar.Services.Data;
using RentACar.Tests.Repositories;
using RentACar.Web.ViewModels;

namespace RentACar.Tests
{
	public class UserManagementServiceTests
	{
		private UserManagementService _userManagementService;
		private RentACarDbContext _dbContext;
		private InMemoryRepository<ApplicationUser, Guid> _repository;
		private UserManager<ApplicationUser> userManager;

		[SetUp]
		public void Setup()
		{
			var options = new DbContextOptionsBuilder<RentACarDbContext>()
				.UseInMemoryDatabase("TestDatabase")
				.Options;

			_dbContext = new RentACarDbContext(options);
			_repository = new InMemoryRepository<ApplicationUser, Guid>(_dbContext);
			_userManagementService = new UserManagementService(_repository, userManager);
		}

		[TearDown]
		public void TearDown()
		{
			_dbContext.Database.EnsureDeleted();
			_dbContext.Database.EnsureCreated();
			_dbContext.Dispose();
		}

		[Test]
		public async Task SearchUsers_ShouldFindExistingUser()
		{

			Guid customerId = Guid.NewGuid();
			var passwordHasher = new PasswordHasher<ApplicationUser>();
			ApplicationUser customer = new ApplicationUser()
			{
				Id = customerId,
				UserName = "TestUsername",
				Email = "testemail@abv.bg"
			};
			customer.PasswordHash = passwordHasher.HashPassword(customer, "TestPassword123");

			await _repository.AddAsync(customer);

			SearchUserViewModel model = new SearchUserViewModel()
			{
				Email = customer.Email,
				IsFound = false,
				UserId = customerId,
				Username = customer.UserName
			};

			var result = await _userManagementService.SearchUsersAsync(model);

			result.Should().NotBeNull();
			result.UserId.Should().Be(customerId);
		}

		[Test]
		public async Task SearchUsers_ShouldNotFindNotExistingUser()
		{

			Guid customerId = Guid.NewGuid();
			var passwordHasher = new PasswordHasher<ApplicationUser>();
			ApplicationUser customer = new ApplicationUser()
			{
				Id = customerId,
				UserName = "TestUsername",
				Email = "testemail@abv.bg"
			};
			customer.PasswordHash = passwordHasher.HashPassword(customer, "TestPassword123");

			await _repository.AddAsync(customer);

			SearchUserViewModel model = new SearchUserViewModel()
			{
				Email = "WrongEmail",
				IsFound = false,
				UserId = customer.Id,
				Username = customer.UserName
			};

			var result = await _userManagementService.SearchUsersAsync(model);

			result.IsFound.Should().BeFalse();
		}
	}
}
