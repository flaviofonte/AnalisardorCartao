using AnalisardorCartao.Entity;
using AnalisardorCartao.Repository;
using System;

namespace AnalisardorCartao.Dao
{
    internal class AlinhadosDao : Repositorio<AlinhadosEntity, DateTime>
    {
        public AlinhadosDao() : base(App.Contexto) { }
    }
}
