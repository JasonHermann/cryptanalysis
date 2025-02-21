using foundation.ciphers;
using foundation.Permutations;

namespace applications.ciphers
{
    /// <summary>
    /// Example taken from Modern Cryptanalysis 
    /// https://swenson.io/Modern%20Cryptanalysis%20v1.1%202022-01-23.pdf
    /// </summary>
    public class Rot13Cipher : SubstitutionCipher
    {
        const string Alphabet = "abcdefghijklmnopqrstuvwxyz";
        const int RotationAmount = -13;
        readonly static SubstitutionOptions options = SubstitutionOptions.UnmappedCharacters_LeaveAsIs;
        readonly static Dictionary<char, char> mapping;

        static Rot13Cipher()
        {
            var mappings = RotationPermutation.Rotate(Alphabet, RotationAmount);

            mapping = new Dictionary<char, char>();
            var index = 0;
            foreach (var c in mappings)
            {
                mapping.Add(Alphabet[index], c);
                index++;
            }
        }
        public Rot13Cipher() : base(mapping, options, options)
        {
        }

        public Rot13Cipher(SubstitutionOptions options) : base(mapping, options, options)
        {
        }
    }
}
