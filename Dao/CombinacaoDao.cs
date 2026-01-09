using AnalisardorCartao.Entity;
using AnalisardorCartao.Repository;

namespace AnalisardorCartao.Dao
{
    public class CombinacaoDao : Repositorio<CombinacaoEntity, int>
    {
        public CombinacaoDao() : base(App.ContextoConta)
        {
        }
    }
}
