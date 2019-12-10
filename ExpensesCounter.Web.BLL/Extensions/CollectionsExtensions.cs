using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpensesCounter.Web.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpensesCounter.Web.BLL.Extensions
{
    public static class CollectionsExtensions
    {
        public static TEntity FindById<TEntity>(this IQueryable<TEntity> entities, int id) where TEntity : BaseEntity
        {
            return entities.FirstOrDefault(entity => entity.Id == id);
        }

        public static Task<TEntity> FindByIdAsync<TEntity>(this IQueryable<TEntity> entities, int id)
            where TEntity : BaseEntity
        {
            return entities.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public static IQueryable<TEntity> MakeOffset<TEntity>(this IQueryable<TEntity> entities, int? offset,
                                                              int? count)
        {
            var offsetted = offset.HasValue ? entities.Skip(offset.Value) : entities;
            var taked = count.HasValue ? offsetted.Take(count.Value) : offsetted;

            return taked;
        }

        public static IEnumerable<TEntity> MakeOffset<TEntity>(this IEnumerable<TEntity> entities, int? offset,
                                                               int? count)
        {
            var offsetted = offset.HasValue ? entities.Skip(offset.Value) : entities;
            var taked = count.HasValue ? offsetted.Take(count.Value) : offsetted;

            return taked;
        }

        public static List<TEntity> MakeOffset<TEntity>(this List<TEntity> entities, int? offset,
                                                        int? count)
        {
            var offsetted = offset.HasValue ? entities.Skip(offset.Value) : entities;
            var taked = count.HasValue ? offsetted.Take(count.Value) : offsetted;

            return taked.ToList();
        }
    }
}