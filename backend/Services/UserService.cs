using Microsoft.EntityFrameworkCore;
using Ristorante.Common;
using Ristorante.Data;
using Ristorante.DTOs;
using Ristorante.Models;
using Ristorante.Services.Interfaces;
using System.Threading.Tasks;

namespace Ristorante.Services
{
    public class UserService : IUserService
    {
        private readonly RistoranteDbContext _context;

        public UserService(RistoranteDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<object>> RegisterAsync(RegisterUserDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                return ServiceResponse<object>.Fail("Email già registrata");

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = passwordHash,
                Role = "User"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var data = new
            {
                user.Id,
                user.Email,
                user.Role
            };

            return ServiceResponse<object>.Ok(data, "Utente registrato con successo");
        }
    }
}
