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

        public async Task<T> AddAsync(T entity)
        {
            await orderContext.AddAsync(entity);
            await orderContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            orderContext.Set<T>().Remove(entity);
            await orderContext.SaveChangesAsync();
        }


        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
         => await orderContext.Set<T>().Where(predicate).ToListAsync();

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

        public async Task<T> GetByIdAsync(int id)
        {

            //// nos trae el primero de una consulta y si no hay nada regresa null
            //// select top 1
            //var entity = await orderContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id); 


            //// nos trae el primero pero si no hay alguna coincidencia manda un error
            //var entity1 = await orderContext.Set<T>().FirstAsync(x => x.Id == id);


            //// nos trae el primero pero si habia mas en la consulta nos manda un error y si no habia nada NULL
            //// select top 2
            //var entity2 = await orderContext.Set<T>().SingleOrDefaultAsync(x => x.Id == id);

            //// nos trae el primero pero si habia mas en la consulta nos manda un error y error tambien con null
            //// select top 2
            //var entity3 = await orderContext.Set<T>().SingleAsync(x => x.Id == id);

            /// el find va a buscar por llave primaria siempre
            var entity = await orderContext.Set<T>().FindAsync(id);

            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            orderContext.Entry(entity).State = EntityState.Modified;
            await orderContext.SaveChangesAsync();
            return entity;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await orderContext.Set<T>().ToListAsync();
        }
    }
}
