using Microsoft.EntityFrameworkCore;
using Ristorante.Models;

namespace Ristorante.Data
{
    public class RistoranteDbContext : DbContext
    {
        public RistoranteDbContext(DbContextOptions<RistoranteDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
