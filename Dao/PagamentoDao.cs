using AnalisardorCartao.Entity;
using AnalisardorCartao.Repository;

namespace AnalisardorCartao.Dao
{
    public class PagamentoDao : Repositorio<PagamentosEntity, int>
    {
        public PagamentoDao() : base(App.ContextoConta)
        {
        }
    }
}
