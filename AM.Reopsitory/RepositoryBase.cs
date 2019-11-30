using AM.Api.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AM.Reopsitory
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected ArticleManagementContext AppDbContext { get; set; }

        public RepositoryBase(ArticleManagementContext repositoryContext)
        {
            this.AppDbContext = repositoryContext;
        }

        public async Task<IEnumerable<T>> FindAllAsync()
        {
            return await this.AppDbContext.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await this.AppDbContext.Set<T>().Where(expression).ToListAsync();
        }

        public void Create(T entity)
        {
            this.AppDbContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            this.AppDbContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            this.AppDbContext.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> Filter(string sort, int skip, int take, string search,
            Expression<Func<T, object>> orderbyExpression,
            Expression<Func<T, bool>> expression
            )
        {
            IEnumerable<T> result = new List<T>();
            IQueryable<T> res;

            if (!string.IsNullOrEmpty(search))
            {
                res = this.AppDbContext.Set<T>().Where(expression).AsQueryable();
            }
            else
            {
                res = this.AppDbContext.Set<T>().AsQueryable();
            }


            if (sort == "asc")
            {

                result = await res.OrderBy(orderbyExpression).Skip(skip).Take(take).ToListAsync();
            }
            else
            {
                result = await res.OrderByDescending(orderbyExpression).Skip(skip).Take(take).ToListAsync();
            }
            return result;
        }

        public async Task SaveAsync()
        {
            await this.AppDbContext.SaveChangesAsync();
        }

        public int Count(T enity)
        {
            return this.AppDbContext.Set<T>().Count();
        }
    }
}
