using AnalisardorCartao.Entity;
using AnalisardorCartao.Repository;

namespace AnalisardorCartao.Dao
{
    public class TipoPagamentoDao : Repositorio<TipoPagamentoEntity, int>
    {
        public TipoPagamentoDao() : base(App.ContextoConta)
        {
        }
    }
}
