using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Ristorante.Common;
using Ristorante.Data;
using Ristorante.DTOs;
using Ristorante.Models;
using Ristorante.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ristorante.Services
{
    public class AuthService : IAuthService
    {
        private readonly RistoranteDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(RistoranteDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<ServiceResponse<object>> LoginAsync(LoginDto dto)
        {
            // Verifica utente
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null)
                return ServiceResponse<object>.Fail("Credenziali non valide");

            // Verifica password
            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return ServiceResponse<object>.Fail("Credenziali non valide");

            // Genera JWT
            var token = GenerateJwtToken(user);

            var data = new
            {
                token,
                user = new { user.Id, user.Email, user.Role }
            };

            return ServiceResponse<object>.Ok(data, "Login effettuato");
        }

        private string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("Jwt");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(double.Parse(jwtSettings["ExpireHours"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
