using System.Linq.Expressions;

namespace VetAppointment.WebAPI.Util
{
    public static class FilteringUtil
    {
        public static Expression<Func<T, bool>> CreateAndExpression<T>(IEnumerable<Func<T, bool>> conditions)
        {
            Expression exp = Expression<Func<T, bool>>.Constant(true);

            var param = Expression<Func<T, bool>>.Parameter(typeof(T));

            foreach (var condition in conditions)
            {
                exp = Expression<Func<T, bool>>.AndAlso(exp, (Expression<Func<T, bool>>) (x => condition(x)));
            }

            return Expression<Func<T, bool>>.Lambda<Func<T, bool>>(exp, param);
        }
    }
}
