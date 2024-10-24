using AudioProject___BackEnd.Models;
using Microsoft.EntityFrameworkCore;
namespace AudioProject___BackEnd.Context
{
    public class AudioDbContext : DbContext
    {
        public AudioDbContext(DbContextOptions<AudioDbContext> options) : base(options) { }


        public DbSet<User> Users { get; set; }
    }
}
