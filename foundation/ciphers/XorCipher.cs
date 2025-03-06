using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace foundation.ciphers
{
    public class XorCipher : ICipher
    {
        private int Key;
        public XorCipher(int key)
        {
            Key = key;
        }

        public string Decrypt(string cipherText)
        {
            return new String([ .. cipherText.Select(c => ((int)c) ^ Key).Select(c => (char)c)]);
        }

        public string Encrypt(string plainText)
        {
            return new String([.. plainText.Select(c => ((int)c) ^ Key).Select(c => (char)c)]);
        }
    }
}
