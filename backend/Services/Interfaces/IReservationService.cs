using Ristorante.Common;
using Ristorante.Models;

namespace Ristorante.Services.Interfaces
{
    public interface IReservationService
    {
        Task<ServiceResponse<Reservation>> CreateAsync(Reservation reservation);
    }
}
