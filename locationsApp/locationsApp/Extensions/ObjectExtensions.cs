using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace locationsApp.Extensions
{
    public static class ObjectExtensions
    {

        /// <summary>
        /// Check to determine if a object is null 
        /// </summary>
        /// <param name="obj">The object</param>
        /// <returns>The answer</returns>
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        /// <summary>
        /// Check to determine if a object is not null 
        /// </summary>
        /// <param name="obj">The object</param>
        /// <returns>The answer</returns>
        public static bool IsNotNull(this object obj)
        {
            return !IsNull(obj);
        }

        public static T ToObject<T>(this IDictionary<string, object> source)
            where T : class, new()
        {
            var someObject = new T();
            var someObjectType = someObject.GetType();

            foreach (var item in source)
            {
                someObjectType
                            .GetProperty(item.Key)
                            .SetValue(someObject, item.Value, null);
            }

            return someObject;
        }

        public static IDictionary<string, object> AsDictionary(this object source, BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
        {
            return source.GetType().GetProperties(bindingAttr).ToDictionary
            (
                propInfo => propInfo.Name,
                propInfo => propInfo.GetValue(source, null)
            );
        }

        public static string ToDebugString<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            return "{" + string.Join(",", dictionary.Select(kv => kv.Key + "=" + kv.Value).ToArray()) + "}";
        }

    }

}
