using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace foundation.ciphers
{
    [Flags]
    public enum XorCipherFlags
    {
        XorScalesWithKeySize = 1,
    }
    public class XorCipher : ICipher
    {
        private long Key;
        private XorCipherFlags Flags;
        private byte[] Key_Array;
        private int Bytes;

        public XorCipher(long key, XorCipherFlags flags = default, int keySize = 0)
        {
            Key = key;
            Flags = flags;
            Key_Array = BitConverter.GetBytes(key);
            Bytes = 0;
            if (keySize != 0)
            {
                Bytes = keySize;
                Key_Array = Key_Array.Take(Bytes).ToArray();
                return;
            }

            for (int i = 0; i < Key_Array.Length; i++)
            {
                if (Key_Array[i] != 0)
                {
                    Bytes = i + 1;
                }
            }
            if (Key_Array.Length > Bytes)
            {

                Key_Array = Key_Array.Take(Bytes).ToArray();
            }
        }

        public string Decrypt(string cipherText)
        {
            return Crypt(cipherText);
        }

        public string Encrypt(string plainText)
        {
            return Crypt(plainText);
        }

        public string Crypt(string t)
        {
            // Special case
            if (Key == 0)
                return t;

            if (Flags.HasFlag(XorCipherFlags.XorScalesWithKeySize))
            {
                var output = "";
                for (int c = 0; c < t.Length; c = c + Bytes)
                {
                    string s = "";
                    if (c + Bytes > t.Length)
                    {
                        var current = t.Substring(c);
                        var b = Key_Array.Take(current.Length).ToArray().Xor([.. current.Select(c => (byte)c)]);
                        s = new String([.. b.Select(c => (char)c)]);
                    }
                    else
                    {
                        var current = t.Substring(c, Bytes);
                        var b = Key_Array.Xor([.. current.Select(c => (byte)c)]);
                        s = new String([.. b.Select(c => (char)c)]);
                    }
                    output += s;
                }
                return output;
            }
            else
            {
                return new String([.. t.Select(c => ((int)c) ^ Key).Select(c => (char)c)]);
            }
        }
    }
}
