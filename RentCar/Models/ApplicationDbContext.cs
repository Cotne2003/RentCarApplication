using Microsoft.EntityFrameworkCore;
using RentCar.Models.Entities;

namespace RentCar.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Car> cars { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Message> messages { get; set; }
        public DbSet<FavoriteCar> favoriteCars { get; set; }
    }
}
