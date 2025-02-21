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
    }
}
