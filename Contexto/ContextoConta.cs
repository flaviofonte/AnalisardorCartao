using AnalisardorCartao.Entity;
using Microsoft.EntityFrameworkCore;

namespace AnalisardorCartao.Contexto
{
    public class ContextoConta : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dados = Funcoes.LeConfiguracao("dadosconta");
            string _conexao;

            _conexao = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + dados + ";Mode=Share Deny None;Jet OLEDB:Database Password=jfmeunome;";

            //_conexao = @"Provider=Microsoft.Jet.OLEDB.4.0;Database Password=jfmeunome;Data source=" + dados;
            optionsBuilder.EnableServiceProviderCaching(false).UseJet(_conexao);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LancamentoEntity>();
            modelBuilder.Entity<LancPgtoEntity>();
            modelBuilder.Entity<ClientesEntity>();
            modelBuilder.Entity<PagamentosEntity>();
            modelBuilder.Entity<TipoPagamentoEntity>();
            modelBuilder.Entity<CombinacaoEntity>();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<LancamentoEntity> Lancamentos { get; set; }
        public DbSet<LancPgtoEntity> LancPgtos { get; set; }
        public DbSet<ClientesEntity> Clientes { get; set; }
        public DbSet<PagamentosEntity> Pagamentos { get; set; }
        public DbSet<TipoPagamentoEntity> TipoPagamento { get; set; }
        public DbSet<CombinacaoEntity> Combinacao { get; set; }
    }
}
