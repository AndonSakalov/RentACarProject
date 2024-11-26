﻿using RentACar.Web.ViewModels;

namespace RentACar.Services.Data.Interfaces
{
	public interface IVehicleService
	{
		Task<bool> CreateAndAddVehicleAsync(AddVehicleViewModel model);
		Task<IEnumerable<EditVehicleListViewModel>> GetAllVehiclesAsync(string id);
		Task<IEnumerable<EditVehicleListViewModel>> FilterVehiclesAsync(string branchId, string vehicleType);
	}
}
