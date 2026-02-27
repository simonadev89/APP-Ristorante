using System.ComponentModel.DataAnnotations.Schema;

namespace Ristorante.Models
{
    // Rappresenta una prenotazione fatta da un utente per un ristorante.
    
    public class Reservation
    {
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        [ForeignKey("Restaurant")]
        public int RestaurantId { get; set; }     // Ristorante scelto
        public Restaurant Restaurant { get; set; } = null!;
        public DateTime DateTime { get; set; }    // Data e ora prenotazione
        public int NumberOfPeople { get; set; }   // Numero persone
        public string Status { get; set; } = "Pending"; // Stato: Pending, Confirmed, Rejected
        
        // Lo stato serve a sapere se la prenotazione è stata confermata dal gestore.
    }
}
