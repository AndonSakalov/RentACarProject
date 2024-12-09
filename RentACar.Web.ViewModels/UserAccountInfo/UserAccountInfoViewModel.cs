using RentACar.Common;
using System.ComponentModel.DataAnnotations;

namespace RentACar.Web.ViewModels.UserAccountInfo
{
    public class UserAccountInfoViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        public string PhoneNumber { get; set; } = null!;

        public PaginatedList<RentalHistoryViewModel> Rentals { get; set; } = null!;
    }
}
