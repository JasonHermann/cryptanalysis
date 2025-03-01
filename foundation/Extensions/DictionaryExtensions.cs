using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.Generic
{
    public static class DictionaryExtensions
    {
        public static bool IsBijection<T, S>(this Dictionary<T, S> d) where T: notnull
        {
            var values = new HashSet<S>();
            foreach (var kvp in d)
            {
                if (!values.Add(kvp.Value))
                    return false;
            }
            return true;
        }

        public static double MSE<T>(this Dictionary<T, double> a, Dictionary<T, double> b)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }

            var total = 0.00;
            var visited = new HashSet<T>();
            foreach (var e in a)
            {
                var k = e.Key;
                visited.Add(k);
                if (b.ContainsKey(k))
                {
                    total += Math.Pow(e.Value - b[k], 2);
                }
                else
                {
                    total += Math.Pow(e.Value, 2);
                }
            }
            foreach (var e in b)
            {
                var k = e.Key;
                if (visited.Contains(k) == false)
                {
                    total += Math.Pow(e.Value, 2);
                }
            }

            return total;
        }
    }
}
