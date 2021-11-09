using Ardalis.Specification;
using PhanTichChungKhoan.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PhanTichChungKhoan.Application.Interfaces
{
    public interface IEfRepositoryBase
    {
        Task<T> FirstOrDefaultAsync<T>(ISpecification<T> spec) where T : class;
        Task<TResult> FirstOrDefaultAsync<T, TResult>(ISpecification<T> spec, Expression<Func<T, TResult>> selector) where T : class;

        Task<List<T>> ListAsync<T>(ISpecification<T> spec) where T : class;
        Task<List<TResult>> ListSelectAsync<T, TResult>(ISpecification<T> spec, Expression<Func<T, TResult>> selector) where T : class;
        Task<List<TResult>> ListSelectAsync<T, TResult>(Expression<Func<T, TResult>> selector) where T : class;
        Task<int> SaveChangesAsync();
        Task<List<T>> ListAsync<T>() where T : class;
        Task<T> AddAsync<T>(T entity) where T : EntityBase;
        Task AddRangeAsync<T>(IEnumerable<T> entities) where T : EntityBase;
        void DeleteRange<T>(IEnumerable<T> entities) where T : EntityBase;
        void UpdateRange<T>(IEnumerable<T> entities) where T : EntityBase;
    }
}
