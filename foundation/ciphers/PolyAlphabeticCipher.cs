using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace foundation.ciphers
{
    public class PolyAlphabeticCipher : ICipher
    {
        private readonly List<Dictionary<char, char>> _encryptMapping;
        private readonly List<Dictionary<char, char>> _decryptMapping;
        private readonly SubstitutionOptions _encryptionOptions;
        private readonly SubstitutionOptions _decryptionOptions;

        public PolyAlphabeticCipher(List<Dictionary<char, char>> encryptMapping,
            SubstitutionOptions encryptionOptions = SubstitutionOptions.UnmappedCharacters_LeaveAsIs | SubstitutionOptions.UnmappedCharacters_AdvancesAlphabet,
            SubstitutionOptions decryptionOptions = SubstitutionOptions.UnmappedCharacters_LeaveAsIs | SubstitutionOptions.UnmappedCharacters_AdvancesAlphabet)
        {
            _encryptionOptions = encryptionOptions;
            _decryptionOptions = decryptionOptions;

            // Validate that this is a proper one-to-one mapping
            _encryptMapping = new List<Dictionary<char, char>>();
            _decryptMapping = new List<Dictionary<char, char>>();
            foreach (var mapping in encryptMapping)
            {
                if (!IsValidMapping(mapping))
                {
                    throw new ArgumentException("Encrypt mapping is invalid or not one-to-one.");
                }
                _encryptMapping.Add(new Dictionary<char, char>(mapping));
                _decryptMapping.Add(mapping.ToDictionary(kvp => kvp.Value, kvp => kvp.Key));
            }
        }

        public string Encrypt(string plainText)
        {
            return Crypt(plainText, _encryptMapping, _encryptionOptions);
        }

        public string Decrypt(string cipherText)
        {
            return Crypt(cipherText, _decryptMapping, _decryptionOptions);
        }

        static string Crypt(string text, List<Dictionary<char, char>> lookups, SubstitutionOptions options)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            var sb = new StringBuilder(text.Length);


            var keyIndex = 0;
            foreach (var ch in text)
            {
                var mapping = lookups[keyIndex];
                if (mapping.TryGetValue(ch, out char mappedChar))
                {
                    sb.Append(mappedChar);
                    keyIndex = (keyIndex + 1) % lookups.Count;
                }
                else
                {
                    if (options.HasFlag(SubstitutionOptions.UnmappedCharacters_AdvancesAlphabet))
                    {
                        keyIndex = (keyIndex + 1) % lookups.Count;
                    }

                    if (options.HasFlag(SubstitutionOptions.UnmappedCharacters_LeaveAsIs))
                    {
                        sb.Append(ch);
                    }
                    else if (options.HasFlag(SubstitutionOptions.UnmappedCharacters_Remove))
                    {

                    }
                    else if (options.HasFlag(SubstitutionOptions.UnmappedCharacters_ThrowError))
                    {
                        var message = string.Format("Character {0} is not mapped in dictionary.", ch);
                        throw new NotSupportedException(message);
                    }
                }

            }
            return sb.ToString();
        }

        /// <summary>
        /// Ensures the dictionary is one-to-one
        /// (no two letters map to the same letter).
        /// </summary>
        private bool IsValidMapping(Dictionary<char, char> mapping)
        {
            return mapping.IsBijection();
        }
    }
}
