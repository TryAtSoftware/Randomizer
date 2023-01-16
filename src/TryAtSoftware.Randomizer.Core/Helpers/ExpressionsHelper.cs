namespace TryAtSoftware.Randomizer.Core.Helpers;

using System;
using System.Linq.Expressions;
using System.Reflection;
using TryAtSoftware.Extensions.Reflection;

public static class ExpressionsHelper
{
    public static PropertyInfo GetPropertyInfo<T, TValue>(this Expression<Func<T, TValue>> expression)
    {
        var memberInfo = expression.GetMemberInfo();
        if (memberInfo is not PropertyInfo propertyInfo) throw new InvalidOperationException("The referenced member is not a property.");
        return propertyInfo;
    }
}