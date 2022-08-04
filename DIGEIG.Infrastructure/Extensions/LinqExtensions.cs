using DIGEIG.Application.Filter;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace DIGEIG.Infrastructure.Extensions
{
    public static class LinqExtensions
    {
        public static IQueryable<T> Pagination<T>(this IQueryable<T> query, PaginationFilter paginationFilter)
        {
            if (!paginationFilter.ColumnOrdeBy.Trim().Equals("function"))
            {
                if (paginationFilter.IsOrderByDescending && !string.IsNullOrEmpty(paginationFilter.ColumnOrdeBy))
                {
                    query = query.OrderBy($"{paginationFilter.ColumnOrdeBy} descending");
                }
                else if (!string.IsNullOrEmpty(paginationFilter.ColumnOrdeBy) && !paginationFilter.IsOrderByDescending)
                {
                    query = query.OrderBy($"{paginationFilter.ColumnOrdeBy} ascending");
                }
            }

            var queryable = query.Skip(paginationFilter.PageNumber == 1 ? 0 : paginationFilter.PageNumber).Take(paginationFilter.PageSize);

            return queryable;
        }
    }
}
