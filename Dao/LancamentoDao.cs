using AnalisardorCartao.Entity;
using AnalisardorCartao.Repository;

namespace AnalisardorCartao.Dao
{
    public class LancamentoDao : Repositorio<LancamentoEntity, int>
    {
        public LancamentoDao() : base(App.ContextoConta) { }
    }
}
