namespace RentACar.Web.ViewModels
{
	public class VehicleFilterViewModel
	{
		public string BranchId { get; set; } = null!;
		public string PickupDate { get; set; } = null!;
		public string ReturnDate { get; set; } = null!;
		public string VehicleTypeName { get; set; } = null!;
		public List<string> PriceRanges { get; set; } = new List<string>();
		public string? TransmissionType { get; set; }
		public int DoorsCount { get; set; }
	}
}
