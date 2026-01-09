using AnalisardorCartao.Entity;
using AnalisardorCartao.Repository;

namespace AnalisardorCartao.Dao
{
    public class LancPgtoDao : Repositorio<LancPgtoEntity, int>
    {
        public LancPgtoDao() : base(App.ContextoConta) { }
    }
}
