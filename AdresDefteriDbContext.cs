using Microsoft.EntityFrameworkCore;

namespace AdresDefteri
{
    public class AdresDefteriDbContext : DbContext
    {
        public DbSet<Musteri> Musteri { get; set; }
        public DbSet<Personel> Personel { get; set; }
        public DbSet<Paydas> Paydas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=AdresDefteri;User Id=sa;Password=sa;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Personel>()
                .Property(s => s.Id)
                .UseIdentityColumn();

            modelBuilder.Entity<Paydas>()
                .Property(s => s.Id)
                .UseIdentityColumn();

            modelBuilder.Entity<Musteri>()
                .Property(s => s.Id)
                .UseIdentityColumn();
        }
    }
}
