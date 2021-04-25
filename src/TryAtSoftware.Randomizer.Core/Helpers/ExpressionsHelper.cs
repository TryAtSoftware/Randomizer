namespace TryAtSoftware.Randomizer.Core.Helpers
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using JetBrains.Annotations;

    public static class ExpressionsHelper
    {
        [NotNull]
        public static PropertyInfo GetPropertyInfo<T, TValue>(this Expression<Func<T, TValue>> propertyLambda)
        {
            if (propertyLambda is null)
                throw new ArgumentNullException(nameof(propertyLambda));

            MemberExpression memberExpression;
            if (propertyLambda.Body is UnaryExpression body)
                memberExpression = body.Operand as MemberExpression;
            else
                memberExpression = propertyLambda.Body as MemberExpression;

            if (memberExpression?.Member is PropertyInfo propInfo)
                return propInfo;

            throw new InvalidOperationException("The member expression was not successfully interpreted.");
        }
    }
}