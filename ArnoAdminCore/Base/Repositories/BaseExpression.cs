using ArnoAdminCore.Base.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ArnoAdminCore.Base.Repositories
{
    public class BaseExpression<TEntity>
    {
        protected static readonly MethodInfo containsMethod = typeof(string).GetMethod("Contains", new Type[] { typeof(string) });
        protected static readonly MethodInfo startsWithMethod = typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) });
        protected static readonly MethodInfo endsWithMethod = typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) });
        protected Expression<Func<TEntity, bool>> CreateExpression(Object searcher)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "p");
            PropertyInfo[] props = searcher.GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
            Expression query = null;
            foreach (PropertyInfo prop in props)
            {
                object val = prop.GetValue(searcher);
                if (val != null)
                {
                    Expression exp = GetExpression(searcher, parameter, prop);
                    if (exp != null)
                    {
                        query = query == null ? exp : Expression.And(query, exp);
                    }
                }
            }
            return query == null ? null : Expression.Lambda<Func<TEntity, bool>>(query, parameter);
        }

        protected virtual Expression GetExpression(object searcher, ParameterExpression parameter, PropertyInfo prop)
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

        public IOrderedQueryable<TEntity> OrderBy(IQueryable<TEntity> source, string property)
        {
            return ApplyOrder(source, property, "OrderBy");
        }
        public IOrderedQueryable<TEntity> OrderByDescending(IQueryable<TEntity> source, string property)
        {
            return ApplyOrder(source, property, "OrderByDescending");
        }
        public IOrderedQueryable<TEntity> ThenBy(IOrderedQueryable<TEntity> source, string property)
        {
            return ApplyOrder(source, property, "ThenBy");
        }
        public IOrderedQueryable<TEntity> ThenByDescending(IOrderedQueryable<TEntity> source, string property)
        {
            return ApplyOrder(source, property, "ThenByDescending");
        }
        protected IOrderedQueryable<TEntity> ApplyOrder(IQueryable<TEntity> source, string property, string methodName)
        {
            string[] props = property.Split('.');
            Type type = typeof(TEntity);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            foreach (string prop in props)
            {
                PropertyInfo pi = type.GetProperty(prop, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(TEntity), type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

            object result = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                            && method.IsGenericMethodDefinition
                            && method.GetGenericArguments().Length == 2
                            && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(TEntity), type)
                    .Invoke(null, new object[] { source, lambda });
            return (IOrderedQueryable<TEntity>)result;
        }
    }
}
