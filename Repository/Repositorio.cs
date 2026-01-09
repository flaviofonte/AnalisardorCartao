using System;
using System.Collections.Generic;
using System.Linq;
using AnalisardorCartao.Interface;
using Microsoft.EntityFrameworkCore;

namespace AnalisardorCartao.Repository
{
    public class Repositorio<T, S> : IDados<T, S> where T : class
    {
        protected DbContext _Dados;
        protected DbSet<T> _DbSet;

        public Repositorio(DbContext dados)
        {
            _Dados = dados ?? throw new NullReferenceException("Server Dados é nulo");
            //_Dados.ChangeTracker.AutoDetectChangesEnabled = false;
            _DbSet = _Dados.Set<T>();
        }

        public void Adicionar(T entity)
        {
            var entry = _Dados.Entry(entity);
            _DbSet.Attach(entity);
            entry.State = EntityState.Added;
            _Dados.SaveChanges();
            entry.State = EntityState.Detached;
        }

        public void Atualizar(T entity)
        {
            var entry = _Dados.Entry(entity);
            _DbSet.Attach(entity);
            entry.State = EntityState.Modified;
            _Dados.SaveChanges();
            entry.State = EntityState.Detached;
        }

        public void Deletar(T entity)
        {
            var entry = _Dados.Entry(entity);
            _DbSet.Attach(entity);
            entry.State = EntityState.Deleted;
            _Dados.SaveChanges();
        }

        public void Deletar(S id)
        {
            T entity = GetById(id);
            if (entity == null) return;
            Deletar(entity);
        }

        public T GetById(S id)
        {
            T entry = _DbSet.Find(id);
            if (entry != null)
            {
                _Dados.Entry(entry).State = EntityState.Detached;
            }
            return entry;
        }

        public T GetByFilter(List<SearchField> filters)
        {
            return _DbSet.AsNoTracking()
                .Where(FilterLinq<T>.GetWherePredicate(filters.ToArray()))
                .FirstOrDefault();
        }

        public List<T> GetAllByFilter(List<SearchField> filter)
        {
            List<T> where = _DbSet.AsNoTracking()
                .AsQueryable()
                .Where(FilterLinq<T>.GetWherePredicate(filter.ToArray()))
                .ToList();
            return where;
        }

        public List<T> GetAllByFilter(string sortField, SortDirectionEnum sortDirection, List<SearchField> filters)
        {
            string[] sorts;

            IQueryable<T> where = _DbSet.AsNoTracking();
            where = where.Where(FilterLinq<T>.GetWherePredicate(filters.ToArray()));

            if (sortField.Contains("|"))
            {
                sorts = sortField.Split('|');
                IOrderedQueryable<T> order;
                order = (sortDirection == SortDirectionEnum.Ascending)
                    ? where.OrderBy(o => EF.Property<object>(o, sorts[0]))
                    : where.OrderByDescending(o => EF.Property<object>(o, sorts[0]));

                for (int i = 1; i < sorts.Length; i++)
                {
                    order = (sortDirection == SortDirectionEnum.Ascending)
                        ? order.ThenBy(o => EF.Property<object>(o, sorts[i]))
                        : order.ThenByDescending(o => EF.Property<object>(o, sorts[i]));
                }
                return order.ToList();
            }
            else
            {
                where = (sortDirection == SortDirectionEnum.Ascending)
                    ? where.OrderBy(o => EF.Property<object>(o, sortField))
                    : where.OrderByDescending(o => EF.Property<object>(o, sortField));
                return where.ToList();
            }
        }

        public List<T> GetTodos(string sortField)
        {
            List<T> orderBy = _DbSet.AsNoTracking()
                .OrderBy(x => EF.Property<object>(x, sortField))
                .ToList();
            return orderBy;
        }

        public List<T> GetAllByFilter(string sortField, string keyFiltro, object valueFiltro)
        {
            List<T> orderBy = _DbSet.AsNoTracking()
                .Where(x => EF.Property<object>(x, keyFiltro) == valueFiltro)
                .OrderBy(x => EF.Property<object>(x, sortField))
                .ToList();
            return orderBy;
        }

        public T GetByFilter(string keyFiltro, object valueFiltro)
        {
            T orderBy = _DbSet.AsNoTracking()
                .Where(x => EF.Property<object>(x, keyFiltro) == valueFiltro)
                .FirstOrDefault();
            return orderBy;
        }
    }
}
