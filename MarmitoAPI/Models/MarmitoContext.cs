using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MarmitoAPI.Models
{
    public class MarmitoContext : DbContext
    {
        public MarmitoContext(DbContextOptions<MarmitoContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Mito> Mitos { get; set; }
    }
}