namespace DotnetDevPack.Builder
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// 表达式构造器
    /// </summary>
    public static class ExpressionBuilder
    {
        /// <summary>
        /// AndAlso操作
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="first">左边操作符</param>
        /// <param name="second">右边操作符</param>
        /// <returns>构造好后的Expression对象</returns>
        public static Expression<Func<T, bool>> And<T>(
            this Expression<Func<T, bool>> first,
            Expression<Func<T, bool>> second)
        {
            return first.AndAlso<T>(second, Expression.AndAlso);
        }

        /// <summary>
        /// OrElse操作
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="first">左边操作符</param>
        /// <param name="second">右边操作符</param>
        /// <returns>构造好后的Expression对象</returns>
        public static Expression<Func<T, bool>> Or<T>(
            this Expression<Func<T, bool>> first,
            Expression<Func<T, bool>> second)
        {
            return first.AndAlso<T>(second, Expression.OrElse);
        }

        private static Expression<Func<T, bool>> AndAlso<T>(
        this Expression<Func<T, bool>> expr1,
        Expression<Func<T, bool>> expr2,
        Func<Expression, Expression, BinaryExpression> func)
        {
            var parameter = Expression.Parameter(typeof(T));

            var leftVisitor = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
            var left = leftVisitor.Visit(expr1.Body);

            var rightVisitor = new ReplaceExpressionVisitor(expr2.Parameters[0], parameter);
            var right = rightVisitor.Visit(expr2.Body);

            return Expression.Lambda<Func<T, bool>>(
                func(left, right), parameter);
        }

        private class ReplaceExpressionVisitor
            : ExpressionVisitor
        {
            private readonly Expression oldValue;
            private readonly Expression newValue;

            public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
            {
                this.oldValue = oldValue;
                this.newValue = newValue;
            }

            public override Expression Visit(Expression node)
            {
                if (node == oldValue)
                {
                    return newValue;
                }

                return base.Visit(node);
            }
        }
    }
}