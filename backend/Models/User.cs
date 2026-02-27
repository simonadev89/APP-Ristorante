namespace Ristorante.Models
{
    //Rappresenta un utente dell’app, può essere un normale utente o un gestore di ristorante.
    public class User
    {
        public int Id { get; set; }               // Identificativo unico
        public string Name { get; set; } = null!; // Nome dell’utente
        public string Email { get; set; } = null!; // Email per login
        public string PasswordHash { get; set; } = null!; // Password crittografata
        public string Role { get; set; } = "User"; // User o Manager

        public List<Reservation> Reservations { get; set; } = new();
        //un utente puo avere piu prenotazioni

    }
}