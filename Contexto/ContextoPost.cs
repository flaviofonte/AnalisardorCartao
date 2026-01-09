using AnalisardorCartao.Entity;
using Microsoft.EntityFrameworkCore;

namespace AnalisardorCartao.Contexto
{
    public class ContextoPost : DbContext
    {
        public DbSet<RedeEntity> RedeEntities { get; set; }
        public DbSet<LinearEntity> LinearEntities { get; set; }
        public DbSet<ContasLinearEntity> ContasLinears { get; set; }
        public DbSet<AlinhadosEntity> AlinhadosViews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dados = Funcoes.LeConfiguracao("dados");
            string _conexao;

            _conexao = $"Server={dados};Database=Cartoes;User Id=sa;Password=Flavio2014;trustServerCertificate=true;";
            optionsBuilder
                .EnableServiceProviderCaching(false)
                .UseSqlServer(_conexao)
                .EnableDetailedErrors(true)
                .EnableSensitiveDataLogging(true);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RedeEntity>().Property(r => r.Codigo).HasColumnType("int");
            modelBuilder.Entity<RedeEntity>().Property(r => r.Parcela).HasColumnType("smallint");
            modelBuilder.Entity<RedeEntity>().Property(r => r.Parcelas).HasColumnType("smallint");
            modelBuilder.Entity<RedeEntity>().Property(r => r.ValorBruto).HasColumnType("decimal(12,2)");
            modelBuilder.Entity<RedeEntity>().Property(r => r.ValorLiquido).HasColumnType("decimal(12,2)");

            modelBuilder.Entity<LinearEntity>();
            modelBuilder.Entity<AlinhadosEntity>().ToTable("Conciliacao").HasNoKey();
            modelBuilder.Entity<ContasLinearEntity>().Property(c => c.ContaLinearID).HasColumnType("int");
            modelBuilder.Entity<ContasLinearEntity>().Property(c => c.Valor).HasColumnType("decimal(12,2)");
            base.OnModelCreating(modelBuilder);
        }
    }
}
