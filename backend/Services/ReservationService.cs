using Microsoft.EntityFrameworkCore;
using Ristorante.Common;
using Ristorante.Data;
using Ristorante.Models;
using Ristorante.Services.Interfaces;

namespace Ristorante.Services
{
    public class ReservationService : IReservationService
    {
        private readonly RistoranteDbContext _context;

        public ReservationService(RistoranteDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<Reservation>> CreateAsync(Reservation reservation)
        {
            // Verifica ristorante esiste
            var restaurant = await _context.Restaurants
                .FirstOrDefaultAsync(r => r.Id == reservation.RestaurantId);

            if (restaurant == null)
                return ServiceResponse<Reservation>.Fail("Ristorante non trovato");

            // Verifica posti disponibili
            if (reservation.NumberOfPeople > restaurant.TotalSeats)
                return ServiceResponse<Reservation>.Fail("Posti insufficienti");

            // Imposta stato iniziale
            reservation.Status = "Pending";
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return ServiceResponse<Reservation>.Ok(reservation, "Prenotazione creata");
        }
    }
}
