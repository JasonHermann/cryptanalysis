using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace foundation.ciphers
{
    public class ColumnarTranspositionCipher : ICipher
    {
        int _key;
        public ColumnarTranspositionCipher(int key)
        {
            _key = key;
        }

        public string Decrypt(string cipherText)
        {
            return Crypt(cipherText, _key, true);
        }

        public string Encrypt(string plainText)
        {
            return Crypt(plainText, _key, false);
        }

        public static string Crypt(string text, int key, bool decrypt)
        {
            var output = decrypt ? new StringBuilder(text) : new StringBuilder(text.Length);
            int workingKey = key > text.Length ? text.Length : key;

            var column = 0;
            var row = 0;
            foreach (var c in text) // c isn't used for anything it is a counter
            {
                if (column + row * workingKey >= text.Length)
                {
                    column += 1;
                    row = 0;
                }
                var workingIndex = row * workingKey + column;

                if(decrypt)
                {
                    output[workingIndex] = c;
                }
                else
                {
                    output.Append(text[workingIndex]);
                }
                row += 1;
            }

            return output.ToString();
        }
    }
}
