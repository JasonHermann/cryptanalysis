using foundation.ciphers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace foundation.attacks.Xor
{
    public enum HintType
    {
        StartsWith = 1,
        KeyLength  = 2
    }
    public class XorCipherAttack
    {
        HintType HintType;
        String OutputHint;

        public XorCipherAttack(string outputHint, HintType hintType)
        {
            OutputHint = outputHint.ToLower();
            HintType = hintType;
        }

        public long FindKey(string capturedCipher, int maxKeyValue = int.MaxValue)
        {
            if(HintType == HintType.StartsWith)
            {
                for(int guess = 0; guess < maxKeyValue; guess++)
                {
                    var cipher = new XorCipher(guess, XorCipherFlags.XorScalesWithKeySize);

                    var plainText = cipher.Encrypt(capturedCipher).ToLower();

                    if(plainText.StartsWith(OutputHint))
                    {
                        return guess;
                    }
                }
            }
            else if(HintType == HintType.KeyLength)
            {
                var length = OutputHint.Length;
                byte[] a = [.. OutputHint[..length].Select(c => (byte)c)];
                byte[] b = [.. capturedCipher[..length].Select(c => (byte)c)];

                var c = a.Xor(b);

                var bi = new BigInteger(c);
                return (long)bi;
            }
            else
            {
                throw new NotImplementedException();
            }

            throw new KeyNotFoundException("");
        }


    }
}
