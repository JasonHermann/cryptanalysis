﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace foundation.ciphers
{
    [Flags]
    public enum SubstitutionOptions
    {
        UnmappedCharacters_LeaveAsIs  = 1 << 1,
        UnmappedCharacters_Remove     = 1 << 2,
        UnmappedCharacters_ThrowError = 1 << 3,
        /// <summary>
        /// In a polyalphabetic cipher, does encountering an unmapped character advance the alphabet index?
        /// </summary>
        UnmappedCharacters_AdvancesAlphabet       = 1 << 4,
        UnmappedCharacters_DoesNotAdvanceAlphabet = 1 << 5,
    }

    public class SubstitutionCipher : ICipher
    {
        private readonly Dictionary<char, char> _encryptMapping;
        private readonly Dictionary<char, char> _decryptMapping;
        private readonly SubstitutionOptions _encryptionOptions;
        private readonly SubstitutionOptions _decryptionOptions;

        /// <summary>
        /// Expects a dictionary mapping for ENCRYPTION.
        /// The decryption mapping is derived automatically.
        /// </summary>
        /// <param name="encryptMapping">Maps original char -> substituted char</param>
        public SubstitutionCipher(Dictionary<char, char> encryptMapping,
            SubstitutionOptions encryptionOptions = SubstitutionOptions.UnmappedCharacters_LeaveAsIs,
            SubstitutionOptions decryptionOptions = SubstitutionOptions.UnmappedCharacters_LeaveAsIs)
        {
            _encryptionOptions = encryptionOptions;
            _decryptionOptions = decryptionOptions;

            // Validate that this is a proper one-to-one mapping
            if (!IsValidMapping(encryptMapping))
            {
                throw new ArgumentException("Encrypt mapping is invalid or not one-to-one.");
            }

            _encryptMapping = new Dictionary<char, char>(encryptMapping);

            // Build the reverse mapping for decryption
            _decryptMapping = _encryptMapping
                .ToDictionary(kvp => kvp.Value, kvp => kvp.Key);
        }

        public string Encrypt(string plainText)
        {
            return Crypt(plainText, _encryptMapping, _encryptionOptions);

        }
        public string Decrypt(string cipherText)
        {
            return Crypt(cipherText, _decryptMapping, _decryptionOptions);
        }

        static string Crypt(string text, Dictionary<char, char> lookup, SubstitutionOptions options)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            var sb = new StringBuilder(text.Length);

            foreach (var ch in text)
            {
                if (lookup.TryGetValue(ch, out char mappedChar))
                {
                    sb.Append(mappedChar);
                }
                else
                {
                    switch (options)
                    {
                        case SubstitutionOptions.UnmappedCharacters_LeaveAsIs:
                            sb.Append(ch);
                            break;
                        case SubstitutionOptions.UnmappedCharacters_Remove:
                            break;
                        case SubstitutionOptions.UnmappedCharacters_ThrowError:
                            var message = string.Format("Character {0} is not mapped in dictionary.", ch);
                            throw new NotSupportedException(message);
                    };
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
