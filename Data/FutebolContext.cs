using Microsoft.EntityFrameworkCore;
using Models;

namespace Data
{
    public class FutebolContext : DbContext
    {
        public DbSet<Jogador> Jogadores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "server=127.0.0.1;port=3306;database=bancodedadosfut;user=root;password=baba";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }
}