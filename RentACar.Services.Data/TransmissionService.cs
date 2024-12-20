﻿using Microsoft.EntityFrameworkCore;
using RentACar.Data.Models;
using RentACar.Data.Models.Enums;
using RentACar.Data.Repository.Interfaces;
using RentACar.Services.Data.Interfaces;
using RentACar.Web.ViewModels;
using RentACar.Web.ViewModels.Parts;
using static RentACar.Common.EntityValidationConstants.Transmission;

namespace RentACar.Services.Data
{
	public class TransmissionService : BaseService, ITransmissionService
	{
		private readonly IRepository<Transmission, Guid> transmissionRepository;

		public TransmissionService(IRepository<Transmission, Guid> transmissionRepository)
		{
			this.transmissionRepository = transmissionRepository;
		}

		public async Task<bool> CreateTransmissionAsync(CreateTransmissionViewModel model)
		{
			try
			{
				if (model.GearsCount < MinGearsCount || model.GearsCount > MaxGearsCount ||
					!Enum.GetValues(typeof(TransmissionType)).Cast<TransmissionType>().ToList().Contains(model.TransmissionType))
				{
					return false;
				}

				Transmission transmission = new Transmission()
				{
					GearsCount = model.GearsCount,
					Type = model.TransmissionType
				};

				await transmissionRepository.AddAsync(transmission);

				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public async Task<bool> DeleteTransmissionAsync(Guid id)
		{
			try
			{
				Transmission transmissionToDelete = transmissionRepository.GetById(id);

				transmissionToDelete.IsDeleted = true;
				await transmissionRepository.UpdateAsync(transmissionToDelete);

				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public async Task<IEnumerable<AddVehicleTransmissionViewModel>> GetAllTransmissionsAsync()
		{
			var allTransmissions = await transmissionRepository.GetAllAttached()
				.Where(t => t.IsDeleted == false)
				.Select(t => new AddVehicleTransmissionViewModel()
				{
					Id = t.Id,
					Type = t.Type.ToString(),
					GearsCount = t.GearsCount
				})
				.ToListAsync();

			return allTransmissions;
		}

		public async Task<IEnumerable<DeleteTransmissionViewModel>> GetAllTransmissionsForDeletionAsync()
		{
			List<DeleteTransmissionViewModel> transmissions = await transmissionRepository.GetAllAttached()
				.Where(t => t.IsDeleted == false)
				.Select(t => new DeleteTransmissionViewModel()
				{
					Id = t.Id,
					TransmissionType = t.Type.ToString(),
					GearsCount = t.GearsCount
				})
				.ToListAsync();

			return transmissions;
		}
	}
}
