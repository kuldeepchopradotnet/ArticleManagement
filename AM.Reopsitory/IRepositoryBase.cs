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

        int Count(T enity);

        Task<IEnumerable<T>> Filter(string sort, int skip, int take, string search,
            Expression<Func<T, object>> orderbyExpression,
            Expression<Func<T, bool>> expression
            );
    }
}
