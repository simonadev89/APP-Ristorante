namespace Ristorante.Models
{
    //Rappresenta un ristorante che può ricevere prenotazioni.
    public class Restaurant
    {
        public int Id { get; set; }               // Identificativo ristorante
        public string Name { get; set; } = null!; // Nome ristorante
        public string Address { get; set; } = null!; // Indirizzo completo
        public string City { get; set; } = null!;    // Città
        public string Description { get; set; } = null!; // Descrizione
        public int TotalSeats { get; set; }        // Numero totale posti disponibili

        public List<Reservation> Reservations { get; set; } = new();
        // Un ristorante puo avere molte prenotazioni
    }
}

