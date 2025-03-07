using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class ByteArrayExtensions
    {
        public static byte[] Xor(this byte[] a, byte[] b)
        {
            // Ensure both arrays have the same length
            if (a.Length != b.Length)
            {
                throw new ArgumentException("Byte arrays must have the same length to XOR.");
            }

            byte[] result = new byte[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                result[i] = (byte)(a[i] ^ b[i]);
            }

            return result;
        }
    }
}
