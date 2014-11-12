using System;
using System.Linq.Expressions;
using System.Reflection;

namespace MPC.Repository.BaseRepository
{    /// <summary>
    /// Extract property name from a property expression
    /// </summary>
    public static class PropertyReference
    {
        /// <summary>
        /// Extract property name from a property expression
        /// </summary>
        public static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException("propertyExpression");
            }
            MemberExpression memberExpression = propertyExpression.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException("propertyExpression");
            }
            PropertyInfo property = memberExpression.Member as PropertyInfo;
            if (property == null)
            {
                throw new ArgumentException("propertyExpression");
            }
            if (property.GetGetMethod(true).IsStatic)
            {
                throw new ArgumentException("propertyExpression");
            }
            return memberExpression.Member.Name;
        }
    }
}
