using Microsoft.EntityFrameworkCore;
using MFBackend.Models;
using Microsoft.EntityFrameworkCore.Design;

namespace MFBackend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Veiculos> Veiculos { get; set; }
        public DbSet<Consumo> Consumos { get; set; }
    }

}