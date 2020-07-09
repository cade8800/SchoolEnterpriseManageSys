using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Utilities.CommomHelper
{
    public static class ExpressionHelper
    {
        public static Expression DefaultExpress()
        {
            return Expression.Equal(Expression.Constant(1), Expression.Constant(1));
        }

        public static MethodCallExpression ContainsString(Expression instance, string property, string constant)
        {
            MemberExpression mbExp = Expression.Property(instance, property);
            ConstantExpression cnsExp = Expression.Constant(constant);
            MethodCallExpression callExp = Expression.Call(mbExp, typeof(string).GetMethod("Contains"), cnsExp);
            return callExp;
        }

        public static BinaryExpression GreaterThanOrEqual(Expression instance, string property, object constant)
        {
            MemberExpression mbExp = Expression.Property(instance, property);
            ConstantExpression cnsExp = Expression.Constant(constant);
            BinaryExpression binExp = Expression.GreaterThanOrEqual(mbExp, cnsExp);
            return binExp;
        }

        public static BinaryExpression LessThanOrEqual(Expression instance, string property, object constant)
        {
            MemberExpression mbExp = Expression.Property(instance, property);
            ConstantExpression cnsExp = Expression.Constant(constant);
            BinaryExpression binExp = Expression.LessThanOrEqual(mbExp, cnsExp);
            return binExp;
        }

        public static BinaryExpression Equal(Expression instance, string property, object constant)
        {
            MemberExpression mbExp = Expression.Property(instance, property);
            ConstantExpression cnsExp = Expression.Constant(constant);
            BinaryExpression binExp = Expression.Equal(mbExp, cnsExp);
            return binExp;
        }

        public static string GetPropertyName(Expression<Func<object>> propertyexpression)
        {
            var expression = propertyexpression.Body as System.Linq.Expressions.MemberExpression;
            return expression.Member.Name;
        }

        public static string GetPropertyName<T>(Expression<Func<T, object>> propertyexpression)
        {
            var expression = propertyexpression.Body as MemberExpression;
            if (expression == null
                && propertyexpression.Body is UnaryExpression
                && propertyexpression.Body.NodeType == ExpressionType.Convert)
            {
                var exp = propertyexpression.Body as UnaryExpression;
                return (exp.Operand as MemberExpression).Member.Name;
            }
            return expression.Member.Name;
        }

        public static Dictionary<MemberInfo, Func<T, object>> GetMemberAccessor<T>(Expression<Func<T, object>> accessorExpression)
        {
            if (accessorExpression.Body is NewExpression)
            {
                var cols = accessorExpression.Body as NewExpression;
                var colInvoker = new Dictionary<MemberInfo, Func<T, object>>();
                for (int i = 0; i < cols.Members.Count; i++)
                {
                    colInvoker.Add(cols.Members[i], LambdaExpression.Lambda<Func<T, object>>(Expression.Convert(cols.Arguments[i], typeof(object)), accessorExpression.Parameters).Compile());
                }
                return colInvoker;
            }
            else if (accessorExpression.Body is MemberInitExpression)
            {
                var cols = accessorExpression.Body as MemberInitExpression;
                var colInvoker = new Dictionary<MemberInfo, Func<T, object>>();
                var assignments = cols.Bindings.Where(c => c is MemberAssignment);
                foreach (var assign in assignments)
                {
                    var assignment = assign as MemberAssignment;

                    colInvoker.Add(assignment.Member,
                        LambdaExpression.Lambda<Func<T, object>>(Expression.Convert(assignment.Expression, typeof(object)), accessorExpression.Parameters).Compile());
                }
                return colInvoker;
            }
            else
                throw new ArgumentException("accessorExpression.Body必须为MemberInitExpression或NewExpression");
        }

    }
}
