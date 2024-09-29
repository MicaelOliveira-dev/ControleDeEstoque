using Microsoft.EntityFrameworkCore;
using ControlaAiBack.Domain.Entities;  

namespace ControlaAiBack.Infra.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Criando GUID com Entity Framework
            modelBuilder.Entity<Users>()
                .Property(u => u.Id)
                .ValueGeneratedOnAdd(); 

            base.OnModelCreating(modelBuilder);
        }

    }
}
