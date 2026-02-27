using Microsoft.EntityFrameworkCore;
using Ristorante.Common;
using Ristorante.Data;
using Ristorante.Models;
using Ristorante.Services.Interfaces;

namespace Ristorante.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly RistoranteDbContext _context;

        public RestaurantService(RistoranteDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<Restaurant>>> GetAllAsync()
        {
            var restaurants = await _context.Restaurants.ToListAsync();
            return ServiceResponse<List<Restaurant>>.Ok(restaurants);
        }

        public async Task<ServiceResponse<Restaurant>> GetByIdAsync(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            return restaurant != null
                ? ServiceResponse<Restaurant>.Ok(restaurant)
                : ServiceResponse<Restaurant>.Fail("Ristorante non trovato");
        }

        public async Task<ServiceResponse<Restaurant>> CreateAsync(Restaurant restaurant)
        {
            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();
            return ServiceResponse<Restaurant>.Ok(restaurant, "Ristorante creato");
        }

        public async Task<ServiceResponse<object>> UpdateAsync(int id, Restaurant updated)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null)
                return ServiceResponse<object>.Fail("Ristorante non trovato");

            restaurant.Name = updated.Name;
            restaurant.City = updated.City;
            restaurant.TotalSeats = updated.TotalSeats;
            restaurant.Description = updated.Description;

            await _context.SaveChangesAsync();
            return ServiceResponse<object>.Ok(null!, "Ristorante aggiornato");
        }

        public async Task<ServiceResponse<object>> DeleteAsync(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null)
                return ServiceResponse<object>.Fail("Ristorante non trovato");

            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();
            return ServiceResponse<object>.Ok(null, "Ristorante eliminato");
        }
    }
}
