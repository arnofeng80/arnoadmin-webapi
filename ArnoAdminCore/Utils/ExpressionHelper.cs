using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ArnoAdminCore.Utils
{
    public class ExpressionHelper<T>
    {
        public static Expression<Func<T, bool>> CreateExpression(Object searcher)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "p");
            PropertyInfo[] props = searcher.GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
            Expression query = null;
            foreach (PropertyInfo prop in props)
            {
                object val = prop.GetValue(searcher);
                if(val != null)
                {
                    ConstantExpression constantVal = Expression.Constant(val);
                    MemberExpression memberExp = Expression.PropertyOrField(parameter, prop.Name);
                    Expression exp = Expression.Call(memberExp, typeof(string).GetMethod("Contains"), constantVal);
                    query = query == null ? exp : Expression.And(query, exp);
                }
            }
            return query == null ? null : Expression.Lambda<Func<T, bool>>(query, parameter);
        }
    }
}
