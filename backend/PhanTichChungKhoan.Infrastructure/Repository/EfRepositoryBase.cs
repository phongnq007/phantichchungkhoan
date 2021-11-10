using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhanTichChungKhoan.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PhanTichChungKhoan.Infrastructure.Repository
{
    public abstract class EfRepositoryBase
    {
        protected readonly DbContext _dbContext;

        public EfRepositoryBase(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private IQueryable<T> ApplySpecification<T>(ISpecification<T> spec) where T: class
        {
            var evaluator = new SpecificationEvaluator<T>();
            return evaluator.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
        }

        public async Task<T> FirstOrDefaultAsync<T>(ISpecification<T> spec) where T : class
        {
            var specResult = ApplySpecification(spec);
            return await specResult.FirstOrDefaultAsync();
        }

        public async Task<TResult> FirstOrDefaultAsync<T, TResult>(ISpecification<T> spec, Expression<Func<T, TResult>> selector) where T : class
        {
            var specResult = ApplySpecification(spec).Select(selector);
            return await specResult.FirstOrDefaultAsync();
        }

        public async Task<List<T>> ListAsync<T>(ISpecification<T> spec) where T : class
        {
            var specResult = ApplySpecification(spec);
            return await specResult.ToListAsync();
        }

        public async Task<List<TResult>> ListSelectAsync<T, TResult>(ISpecification<T> spec, Expression<Func<T, TResult>> selector) where T : class
        {
            var specResult = ApplySpecification(spec).Select(selector);
            return await specResult.ToListAsync();
        }

        public async Task<List<TResult>> ListSelectAsync<T, TResult>(Expression<Func<T, TResult>> selector) where T : class
        {
            var query = _dbContext.Set<T>().AsQueryable().Select(selector);
            return await query.ToListAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<List<T>> ListAsync<T>() where T : class
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> AddAsync<T>(T entity) where T : EntityBase
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task AddRangeAsync<T>(IEnumerable<T> entities) where T : EntityBase
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
        }

        public void DeleteRange<T>(IEnumerable<T> entities) where T : EntityBase
        {
            _dbContext.Set<T>().RemoveRange(entities);
        }

        public void UpdateRange<T>(IEnumerable<T> entities) where T : EntityBase
        {
            foreach (var item in entities)
            {
                _dbContext.Entry<T>(item).State = EntityState.Modified;
            }
        }
    }
}
