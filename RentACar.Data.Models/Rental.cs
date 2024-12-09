using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentACar.Data.Models
{
	public class Rental
	{
		public Rental()
		{
			Id = Guid.NewGuid();
		}

		[Key]
		[Comment("Identifier of the rental.")]
		public Guid Id { get; set; }

		[Required]
		[ForeignKey(nameof(Customer))]
		[Comment("Identifier of the customer.")]
		public Guid CustomerId { get; set; }
		public virtual ApplicationUser Customer { get; set; } = null!;

		[Required]
		[Comment("The rental starting date.")]
		public DateTime StartDate { get; set; }

		[Required]
		[Comment("The rental ending date.")]
		public DateTime EndDate { get; set; }

		[Required]
		[Precision(18, 2)]
		[Comment("Total price of the rental.")]
		public decimal TotalPrice { get; set; } //Price * days 
		public bool IsActive { get; set; }

		[Required]
		[ForeignKey(nameof(Payment))]
		[Comment("Identifier of the payment.")]
		public Guid PaymentId { get; set; }
		public virtual Payment Payment { get; set; } = null!;
		public virtual Vehicle Vehicle { get; set; } = null!;

		[Required]
		public Guid VehicleId { get; set; }
	}
}

