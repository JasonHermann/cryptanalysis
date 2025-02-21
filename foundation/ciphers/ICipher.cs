﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace foundation.ciphers
{
    interface ICipher
    {
        string Encrypt(string plainText);
        string Decrypt(string cipherText);
    }
}
