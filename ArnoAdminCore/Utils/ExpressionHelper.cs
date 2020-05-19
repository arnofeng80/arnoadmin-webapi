using ArnoAdminCore.Base.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ArnoAdminCore.Utils
{
    public class ExpressionHelper<T>
    {
        private static readonly MethodInfo containsMethod = typeof(string).GetMethod("Contains", new Type[] { typeof(string) });
        private static readonly MethodInfo startsWithMethod = typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) });
        private static readonly MethodInfo endsWithMethod = typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) });
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
                    Expression exp = GetExpression(searcher, parameter, prop);
                    if (exp != null)
                    {
                        query = query == null ? exp : Expression.And(query, exp);
                    }
                }
            }
            return query == null ? null : Expression.Lambda<Func<T, bool>>(query, parameter);
        }

        private static Expression GetExpression(object searcher, ParameterExpression parameter, PropertyInfo prop)
        {
            object val = prop.GetValue(searcher);

            MemberExpression memberExp = Expression.PropertyOrField(parameter, prop.Name);
            ConstantExpression constantVal = null;
            if (val != null)
            {
                if (prop.PropertyType == typeof(DateRange))
                {
                    DateRange dr = (DateRange)val;
                    ConstantExpression constantValBegin = Expression.Constant(dr.BeginTime);
                    ConstantExpression constantValEnd = Expression.Constant(dr.EndTime);
                    return Expression.And(Expression.GreaterThanOrEqual(memberExp, constantValBegin), Expression.LessThanOrEqual(memberExp, constantValEnd));
                }
                else
                {
                    constantVal = Expression.Constant(val);
                    if (prop.PropertyType == typeof(string))
                    {
                        return Expression.Call(memberExp, containsMethod, constantVal);
                    }
                }
                return Expression.Equal(memberExp, constantVal);
            }
            return null;
        }

        public static IOrderedQueryable<T> OrderBy(IQueryable<T> source, string property)
        {
            return ApplyOrder(source, property, "OrderBy");
        }
        public static IOrderedQueryable<T> OrderByDescending(IQueryable<T> source, string property)
        {
            return ApplyOrder(source, property, "OrderByDescending");
        }
        public static IOrderedQueryable<T> ThenBy(IOrderedQueryable<T> source, string property)
        {
            return ApplyOrder(source, property, "ThenBy");
        }
        public static IOrderedQueryable<T> ThenByDescending(IOrderedQueryable<T> source, string property)
        {
            return ApplyOrder(source, property, "ThenByDescending");
        }
        static IOrderedQueryable<T> ApplyOrder(IQueryable<T> source, string property, string methodName)
        {
            string[] props = property.Split('.');
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            foreach (string prop in props)
            {
                PropertyInfo pi = type.GetProperty(prop, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

            object result = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                            && method.IsGenericMethodDefinition
                            && method.GetGenericArguments().Length == 2
                            && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), type)
                    .Invoke(null, new object[] { source, lambda });
            return (IOrderedQueryable<T>)result;
        }
    }
}
