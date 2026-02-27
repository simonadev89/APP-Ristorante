using Microsoft.AspNetCore.Hosting.Server;
using System.ComponentModel.DataAnnotations;

namespace Ristorante.DTOs
{
    //Il DTO è un oggetto che rappresenta i dati della richiesta, separato dal modello database (User).
    //Serve a protezione e pulizia: il frontend invia solo ciò che serve, non tutta la tabella Users.
    public class RegisterUserDto

    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = null!;

        [Required]
        [Compare("Password", ErrorMessage = "Le password non coincidono")]
        public string ConfirmPassword { get; set; } = null!;

        [Required]
        public string? Role { get; set; } // opzionale, default "User"
    }
}
