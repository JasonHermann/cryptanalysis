using foundation.ciphers;
using foundation.Permutations;

namespace applications.ciphers
{
    /// <summary>
    /// Example taken from Modern Cryptanalysis 
    /// https://swenson.io/Modern%20Cryptanalysis%20v1.1%202022-01-23.pdf
    /// </summary>
    public class CaesarCipher : SubstitutionCipher
    {
        const string Alphabet = "abcdefghijklmnopqrstuvwxyz";
        const int RotationAmount = -3;
        readonly static SubstitutionOptions options = SubstitutionOptions.UnmappedCharacters_LeaveAsIs;
        readonly static Dictionary<char, char> mapping;

        static CaesarCipher()
        {
            var mappings = RotationPermutation.Rotate(Alphabet, RotationAmount);

            mapping = new Dictionary<char, char>();
            var index = 0;
            foreach(var c in mappings)
            {
                mapping.Add(Alphabet[index], c);
                index++;
            }
        }
        public CaesarCipher() : base(mapping, options, options)
        {
        }

        public CaesarCipher(SubstitutionOptions options) : base(mapping, options, options)
        {
        }
    }
}
