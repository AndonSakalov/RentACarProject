using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentACar.Data.Models
{
	public class Reservation
	{
		[Required]
		public Guid Id { get; set; }

		[Required]
		[ForeignKey(nameof(User))]
		public Guid UserId { get; set; }

		[Required]
		public virtual ApplicationUser User { get; set; } = null!;

		[Required]
		[ForeignKey(nameof(Vehicle))]
		public Guid VehicleId { get; set; }

		[Required]
		public virtual Vehicle Vehicle { get; set; } = null!;

		[Required]
		[Comment("The pick up date for the reserved vehicle.")]
		public DateTime PickUpDate { get; set; }

		[Required]
		[Comment("The return date for the reserved vehicle.")]
		public DateTime ReturnDate { get; set; }

		[Required]
		[Precision(18, 2)]
		[Comment("The price for the reserved rental.")]
		public decimal Price { get; set; }

		[Required]
		public bool isActive { get; set; }
	}
}
