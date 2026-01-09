using AnalisardorCartao.Entity;
using AnalisardorCartao.Repository;

namespace AnalisardorCartao.Dao
{
    public class ClientesDao : Repositorio<ClientesEntity, int>
    {
        public ClientesDao() : base(App.ContextoConta) { }
    }
}
