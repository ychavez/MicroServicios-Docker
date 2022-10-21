using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts;
using Ordering.Domain.Common;
using Ordering.Infrastructure.Persistence;
using System.Linq.Expressions;

namespace Ordering.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : EntityBase
    {
        private readonly OrderContext orderContext;

        public GenericRepository(OrderContext orderContext)
        {
            this.orderContext = orderContext;
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
         => await orderContext.Set<T>().ToListAsync();

        public async Task<IReadOnlyList<T>> GetAsync(int offset, int limit,
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            params string[] includeStrings)
        {
            /// obtenemos la consulta dinamica
            IQueryable<T> query = orderContext.Set<T>();
            /// especificamos la paginacion
            query = query.Skip(offset).Take(limit);
            // especificamos los include (join)
            query = includeStrings.Aggregate(query, (current, itemInclude) => current.Include(itemInclude));

            //agregamos un filtro si es que hay
            if (predicate is not null) query = query.Where(predicate);

            // retornamos la lista ordenada si es que se puso order by
            if (orderBy is not null) return await orderBy(query).ToListAsync();


            // regresamos el resultado
            return await query.ToListAsync();


        }
    }
}
