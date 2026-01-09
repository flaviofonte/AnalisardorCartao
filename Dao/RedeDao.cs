using AnalisardorCartao.Entity;
using AnalisardorCartao.Repository;

namespace AnalisardorCartao.Dao
{
    internal class RedeDao : Repositorio<RedeEntity, int>
    {
        public RedeDao() : base(App.Contexto) { }
    }
}
