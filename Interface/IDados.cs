
using AnalisardorCartao.Repository;
using System.Collections.Generic;

namespace AnalisardorCartao.Interface
{
    public interface IDados<T, S> where T : class
    {
        List<T> GetTodos(string sortField);
        T GetById(S id);
        T GetByFilter(List<SearchField> filters);
        void Adicionar(T entity);
        void Atualizar(T entity);
        void Deletar(T entity);
        void Deletar(S id);
        List<T> GetAllByFilter(string sortField, string keyFiltro, object valueFiltro);
        List<T> GetAllByFilter(string sortField, SortDirectionEnum sortDirection, List<SearchField> filters);
    }
}
