using Microsoft.AspNetCore.Identity;

namespace RentACar.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
            Rentals = new List<Rental>();
        }
        public virtual ICollection<Rental> Rentals { get; set; }
    }
}
