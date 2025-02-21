using foundation.ciphers;
using foundation.Permutations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace applications.ciphers
{
    public static class VigenèreCipherGenerator
    {
        public static Dictionary<char, Dictionary<char, char>> VigenèreTable(string alphabet = "abcdefghijklmnopqrstuvwxyz")
        {
            var output = new Dictionary<char, Dictionary<char, char>>();
            var rotationIndex = 0;
            foreach (var e in alphabet)
            {
                if (output.ContainsKey(e))
                    throw new NotSupportedException("Alphabet contains the same letter more than once.");

                var d = new Dictionary<char, char>();
                var current = RotationPermutation.Rotate(alphabet, rotationIndex * -1);

                var index = 0;
                foreach (var c in current)
                {
                    d.Add(alphabet[index], c);
                    index++;
                }

                rotationIndex += 1;
                output.Add(e, d);
            }
            return output;
        }

        public static List<Dictionary<char, char>> GenerateVigenèreMappingFromKey(Dictionary<char, Dictionary<char, char>> table, string key)
        {
            var output = new List<Dictionary<char, char>>();

            foreach (var e in key)
            {
                output.Add(table[e]);
            }
            return output;
        }

        public static PolyAlphabeticCipher MakeVigenèreCipherFromKey(string key, SubstitutionOptions options = default)
        {
            var table = VigenèreTable();
            var mappings = GenerateVigenèreMappingFromKey(table, key);

            return (options == default ?
                new PolyAlphabeticCipher(mappings)
                : new PolyAlphabeticCipher(mappings, options, options));
        }
    }
}
