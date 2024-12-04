using Microsoft.EntityFrameworkCore;
using RentACar.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace RentACar.Data.Models
{
	public class Payment
	{
		public Payment()
		{
			Id = Guid.NewGuid();
		}

		[Key]
		[Comment("Identifier of the payment.")]
		public Guid Id { get; set; }

		[Comment("The date of payment.")]
		public DateTime? PaymentDate { get; set; }

		[Required]
		[Precision(18, 2)]
		[Comment("The amount of the payment.")]
		public decimal Amount { get; set; }

		[Required]
		[Comment("The method of the payment.")]
		public PaymentMethod PaymentMethod { get; set; }

		[Required]
		[Comment("The status of the payment.")]
		public PaymentStatus Status { get; set; }
	}
}