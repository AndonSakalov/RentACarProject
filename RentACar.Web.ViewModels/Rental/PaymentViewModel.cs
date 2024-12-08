using RentACar.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace RentACar.Web.ViewModels.Rental
{
    public class PaymentViewModel
    {
        [Required]
        public Guid Id { get; set; }

        public DateTime? PaymentDate { get; set; }

        public decimal? Amount { get; set; }

        [Required]
        public PaymentMethod PaymentMethod { get; set; }

        [Required]
        public PaymentStatus PaymentStatus { get; set; }
    }
}
