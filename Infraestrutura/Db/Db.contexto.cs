using Microsoft.EntityFrameworkCore;
using MinimalApi.Dominio.Entidades;
using Microsoft.Extensions.Configuration;


namespace MinimalApi.Infraestrutura.Db
{
    public class DbContexto : DbContext
    {   
        private readonly IConfiguration _configuracaoAppSettings;

        public DbContexto(IConfiguration configuracaoAppSettings)
        {
            _configuracaoAppSettings = configuracaoAppSettings;
        }

        public DbSet<Administrador> Administradores { get; set; } = default!;
        public DbSet<Veiculos> Veiculos { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Administrador>().HasData(
                new Administrador
                {
                    Id = 1,
                    Email = "adiministrador@teste.com",
                    Senha = "123456",
                    Perfil = "Adm"
                }
            );

            modelBuilder.Entity<Veiculos>().HasData(
                new Veiculos
                {
                 Id = 1,
                Nome = "Fusca",
                Marca = "Volkswagen",
                Ano = 1978
                }
);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var stringConexao = _configuracaoAppSettings.GetConnectionString("ConexaoPadrao");
                if (!string.IsNullOrEmpty(stringConexao))
                {
                    optionsBuilder.UseSqlServer(stringConexao);
                }
            }
        }
    }
}
