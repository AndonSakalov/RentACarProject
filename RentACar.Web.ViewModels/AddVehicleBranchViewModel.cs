﻿using System.ComponentModel.DataAnnotations;

namespace RentACar.Web.ViewModels
{
	public class AddVehicleBranchViewModel
	{
		public Guid Id { get; set; }

		[Required]
		public string Name { get; set; } = null!;

		[Required]
		public string Address { get; set; } = null!;

		[Required]
		public string City { get; set; } = null!;

		[Required]
		public string Country { get; set; } = null!;

		public string DisplayName => $"{Name} - {Address}({City}, {Country})";
	}
}
