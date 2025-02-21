using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace foundation.Permutations
{
    public static class RotationPermutation
    {
        public static IList<T> Rotate<T>(IList<T> source, int index)
        {
            if (source == null) throw new ArgumentNullException();
            if (source.Count == 0) return [];

            var count = source.Count;
            index = (index % count + count) % count;

            var output = new List<T>();
            for (int i = 0; i < source.Count; i++)
            {
                T rotated = source[(i - index + count) % count];
                output.Add(rotated);
            }
            return output;
        }

        public static IEnumerable<T> Rotate<T>(IEnumerable<T> source, int index)
        {
            if (source == null) throw new ArgumentNullException();
            var count = source.Count();
            index = (index % count + count) % count;

            foreach(var e in source.Skip(count - index))
            {
                yield return e;
            }
            foreach (var e in source.Take(count - index))
            {
                yield return e;
            }
            yield break;
        }
    }
}
