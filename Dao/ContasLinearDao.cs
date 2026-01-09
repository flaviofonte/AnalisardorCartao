using AnalisardorCartao.Entity;
using AnalisardorCartao.Repository;

namespace AnalisardorCartao.Dao
{
    internal class ContasLinearDao : Repositorio<ContasLinearEntity, int>
    {
        public ContasLinearDao() : base(App.Contexto) { }
    }
}
