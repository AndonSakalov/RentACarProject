using RentACar.Data.Models;

namespace RentACar.Services.Data.Interfaces
{
    public interface IVehicleService
    {
        Task<IEnumerable<Make>> GetAllMakes();
    }
}
