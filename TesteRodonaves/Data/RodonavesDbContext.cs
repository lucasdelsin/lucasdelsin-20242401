using Microsoft.EntityFrameworkCore;
using TesteRodonaves.Models;

namespace TesteRodonaves.Data
{
    public class RodonavesDbContext : DbContext
    {
        public RodonavesDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Unidade> Unidades { get; set; }

        public DbSet<Colaborador> Colaboradores { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração do relacionamento entre Departamento e Usuario
            modelBuilder.Entity<Colaborador>()
                .HasOne(c => c.Unidade)
                .WithMany(un => un.Colaboradores)
                .HasForeignKey(c => c.Unidade_IdFK);
        }
    }
}
