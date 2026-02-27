using Ristorante.Common;
using Ristorante.Models;

namespace Ristorante.Services.Interfaces
{
    public interface IRestaurantService
    {
        Task<ServiceResponse<List<Restaurant>>> GetAllAsync();
        Task<ServiceResponse<Restaurant>> GetByIdAsync(int id);
        Task<ServiceResponse<Restaurant>> CreateAsync(Restaurant restaurant);
        Task<ServiceResponse<object>> UpdateAsync(int id, Restaurant restaurant);
        Task<ServiceResponse<object>> DeleteAsync(int id);
    }
}
