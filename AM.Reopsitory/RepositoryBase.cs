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

        public async Task<IEnumerable<T>> Filter(string sort = "asc", int skip = 0, int take = 2147483647,
                   Expression<Func<T, object>> orderbyExpression = null,
                   Expression<Func<T, bool>> expression = null)
        {
            IEnumerable<T> result = new List<T>();
            IQueryable<T> res;
            if (expression != null)
            {
                res = this.AppDbContext.Set<T>().Where(expression).AsQueryable();
            }
            else
            {
                res = this.AppDbContext.Set<T>().AsQueryable();
            }
            if (orderbyExpression == null)
            {
                result = await res.Skip(skip).Take(take).ToListAsync();
            }
            else
            {
                if (sort == "asc")
                {

                    result = await res.OrderBy(orderbyExpression).Skip(skip).Take(take).ToListAsync();
                }
                else
                {
                    result = await res.OrderByDescending(orderbyExpression).Skip(skip).Take(take).ToListAsync();
                }
            }
            return result;
        }

        public async Task SaveAsync()
        {
            await this.AppDbContext.SaveChangesAsync();
        }

        public int Count(Expression<Func<T, bool>> expression = null)
        {
            if (expression == null)
            {
                return this.AppDbContext.Set<T>().Count();
            }
            return this.AppDbContext.Set<T>().Where(expression).Count();
        }


    }
}
