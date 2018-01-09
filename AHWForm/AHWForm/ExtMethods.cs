using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHWForm
{
    public static class ExtMethods
    {
        public static IEnumerable<T> Flatten<T, R>(this IEnumerable<T> source, Func<T, R> recursion) where R : IEnumerable<T>
        {
            return source.SelectMany(x => (recursion(x) != null && recursion(x).Any()) ? recursion(x).Flatten(recursion) : null)
                         .Where(x => x != null);
        }
    }
}