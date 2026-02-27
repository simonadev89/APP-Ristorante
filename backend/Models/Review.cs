namespace Ristorante.Models
{
    // Rappresenta la recensione che un utente lascia per un ristorante.
    public class Review
    {
        public int Id { get; set; }               // Identificativo recensione
        public int UserId { get; set; }           // Utente che scrive
        public int RestaurantId { get; set; }     // Ristorante recensito
        public int Rating { get; set; }           // Voto da 1 a 5
        public string Comment { get; set; } = null!; // Commento testuale
        public DateTime Date { get; set; } = DateTime.Now; // Data creazione
    }
}
