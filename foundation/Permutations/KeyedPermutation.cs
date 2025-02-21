using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace foundation.Permutations
{
    /// <summary>
    /// A keyed permutation is performed by returning source as key, less duplication and less
    /// any keys not appearing in source.
    /// 
    /// Simplest case, the returned value is the key.
    /// </summary>
    public static class KeyedPermutation
    {
        public static IList<T> PermuteByKey<T>(IList<T> source, IList<T> key)
        {
            if (source == null) throw new ArgumentNullException();
            if (key == null) throw new ArgumentNullException();

            if (source.Count == 0) return [];

            var sourceHash = new HashSet<T>();
            for (int i = 0; i < source.Count; i++)
            {
                T e = source[i];
                if (sourceHash.Add(e) == false)
                    continue;
            }

            var output = new List<T>();
            var hash = new HashSet<T>();
            for (int i = 0; i < key.Count; i++)
            {
                T e = key[i];
                // Ignore values in key that don't appear in alphabet.
                if(sourceHash.Contains(e) == false)
                    continue;

                if (hash.Add(e) == false)
                    continue;
                output.Add(e);
            }
            for (int i = 0; i < source.Count; i++)
            {
                T e = source[i];
                if (hash.Add(e) == false)
                    continue;
                output.Add(e);
            }
            return output;
        }

        public static IEnumerable<T> PermuteByKey<T>(IEnumerable<T> source, IEnumerable<T> key)
        {
            if (source == null) throw new ArgumentNullException();
            if (key == null) throw new ArgumentNullException();

            var sourceHash = new HashSet<T>();
            foreach(var e in source)
            {
                if (sourceHash.Add(e) == false)
                    continue;
            }

            var hash = new HashSet<T>();
            foreach(var e in key)
            {
                // Ignore values in key that don't appear in alphabet.
                if (sourceHash.Contains(e) == false)
                    continue;

                if (hash.Add(e) == false)
                    continue;
                yield return e;
            }
            foreach (var e in source)
            {
                if (hash.Add(e) == false)
                    continue;
                yield return e;
            }
            yield break;
        }
    }
}
