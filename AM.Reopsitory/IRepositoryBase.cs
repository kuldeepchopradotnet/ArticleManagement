using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AM.Reopsitory
{
    public interface IRepositoryBase<T>
    {
        Task<IEnumerable<T>> FindAllAsync();
        Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveAsync();

        int Count(Expression<Func<T, bool>> expression = null);

        Task<IEnumerable<T>> Filter(string sort = "asc", int skip = 0, int take = 2147483647,
                           Expression<Func<T, object>> orderbyExpression = null,
                           Expression<Func<T, bool>> expression = null);
    }
}
