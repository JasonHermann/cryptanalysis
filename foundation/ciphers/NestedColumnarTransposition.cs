using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace foundation.ciphers
{

    public class NestedColumnarTransposition : ICipher
    {
        List<int> _keys;
        public NestedColumnarTransposition(List<int> keys)
        {
            _keys = [.. keys.AsEnumerable()];
        }
        public string Decrypt(string cipherText)
        {
            foreach (var k in _keys.AsEnumerable().Reverse())
            {
                cipherText = ColumnarTranspositionCipher.Crypt(cipherText, k, true);
            }
            return cipherText;
        }

        public string Encrypt(string plainText)
        {
            foreach (var k in _keys)
            {
                plainText = ColumnarTranspositionCipher.Crypt(plainText, k, false);
            }
            return plainText;
        }
    }
}