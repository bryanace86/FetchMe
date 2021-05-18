using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FetchMe.Services.TagServices
{
    public static class TagPicker
    {
        public static T Pick<T>(this IQueryable<T> source, Expression<Func<T, string>> column, string filter) where T : class, new()
        {
            if (filter is null) { return null; }

            Expression<Func<T, bool>> columnFilter = Expression.Lambda<Func<T, bool>>(Expression.Equal(column.Body, Expression.Constant(filter)), column.Parameters);

            T result = source.FirstOrDefault(columnFilter);

            if (result is null)
            {
                result = new T();
                Expression<Action<T>> assign = Expression.Lambda<Action<T>>(Expression.Assign(column.Body, Expression.Constant(filter)), column.Parameters);

                // If you can't compile with the true because you are using an old .NET, remove it
                Action<T> assignCompiled = assign.Compile(true);
                assignCompiled(result);
            }

            return result;
        }

        public static T PickDate<T>(this IQueryable<T> source, Expression<Func<T, DateTime>> column, DateTime filter) where T : class, new()
        {
            if (filter == DateTime.MinValue) { return null; }

            Expression<Func<T, bool>> columnFilter = Expression.Lambda<Func<T, bool>>(Expression.Equal(column.Body, Expression.Constant(filter)), column.Parameters);

            T result = source.FirstOrDefault(columnFilter);

            if (result is null)
            {
                result = new T();
                Expression<Action<T>> assign = Expression.Lambda<Action<T>>(Expression.Assign(column.Body, Expression.Constant(filter)), column.Parameters);

                // If you can't compile with the true because you are using an old .NET, remove it
                Action<T> assignCompiled = assign.Compile(true);
                assignCompiled(result);
            }

            return result;
        }

    }
}
