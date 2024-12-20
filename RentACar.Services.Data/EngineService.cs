﻿using Microsoft.EntityFrameworkCore;
using RentACar.Data.Models;
using RentACar.Data.Models.Enums;
using RentACar.Data.Repository.Interfaces;
using RentACar.Services.Data.Interfaces;
using RentACar.Web.ViewModels;
using RentACar.Web.ViewModels.Parts;
using static RentACar.Common.EntityValidationConstants.Engine;

namespace RentACar.Services.Data
{
	public class EngineService : BaseService, IEngineService
	{
		private readonly IRepository<Engine, Guid> engineRepository;

		public EngineService(IRepository<Engine, Guid> engineRepository)
		{
			this.engineRepository = engineRepository;
		}
		public async Task<bool> CreateAndAddEngineAsync(CreateEngineViewModel model)
		{
			try
			{
				if (model.HP < HPMinRange || model.HP > HPMaxRange
					|| !Enum.GetValues(typeof(FuelType)).Cast<FuelType>().ToList().Contains(model.FuelType)
					|| model.Torque < MinTorque || model.Torque > MaxTorque
					|| (model.IsElectric && model.FuelEfficiency != FuelEfficiencyMinForElectric)
					|| (model.IsElectric && model.FuelEfficiency != FuelEfficiencyMinForElectric)
					|| (model.IsElectric && model.CylindersCount != CylindersMinCountForElectric)
					|| (model.Description != null && model.Description?.Length > DescriptionMaxLength)
					|| (model.IsElectric && model.FuelType != FuelType.Electric)
					|| model.FuelEfficiency < FuelEfficiencyMin || model.FuelEfficiency > FuelEfficiencyMax
					|| model.Displacement < DisplacementMinRange || model.Displacement > (decimal)DisplacementMaxRange
					|| model.CylindersCount < CylindersMinCount || model.CylindersCount > CylindersMaxCount)
				{
					return false;
				}

				Engine engine = new Engine()
				{
					FuelType = model.FuelType,
					HP = model.HP,
					Torque = model.Torque,
					FuelEfficiency = model.FuelEfficiency,
					Displacement = model.Displacement,
					CylindersCount = model.CylindersCount,
					Description = model.Description,
					IsElectric = model.FuelType.ToString() == "Electric" ? true : false
				};

				await engineRepository.AddAsync(engine);

				return true;
			}
			catch (Exception ex)
			{
				return false;
			}

		}

		public async Task<bool> DeleteEngineAsync(Guid engineId)
		{
			try
			{
				Engine engineToDelete = await engineRepository.GetByIdAsync(engineId);

				engineToDelete.IsDeleted = true;
				await engineRepository.UpdateAsync(engineToDelete);

				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public async Task<IEnumerable<AddVehicleEngineViewModel>> GetAllEnginesAsync()
		{
			var allEngines = (await engineRepository.GetAllAsync())
				.Where(e => e.IsDeleted == false)
				.ToList()
				.Select(e => new AddVehicleEngineViewModel()
				{
					Id = e.Id,
					FuelType = e.FuelType.ToString(),
					HP = e.HP,
					Displacement = e.Displacement,
					CylindersCount = e.CylindersCount
				});

			return allEngines;
		}

		public async Task<IEnumerable<DeleteEngineViewModel>> GetAllEnginesForDeletionAsync()
		{
			List<DeleteEngineViewModel> engines = await engineRepository.GetAllAttached()
				.Where(e => e.IsDeleted == false)
				.Select(e => new DeleteEngineViewModel()
				{
					Id = e.Id,
					HP = e.HP,
					FuelType = e.FuelType.ToString(),
					Displacement = e.Displacement,
					CylindersCount = e.CylindersCount
				})
				.ToListAsync();

			return engines;
		}
	}
}