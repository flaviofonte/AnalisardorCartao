using AnalisardorCartao.Entity;
using AnalisardorCartao.Repository;

namespace AnalisardorCartao.Dao
{
    internal class LinearDao : Repositorio<LinearEntity, int>
    {
        public LinearDao() : base(App.Contexto) { }
    }
}
