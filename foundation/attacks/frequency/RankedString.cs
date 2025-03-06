using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace foundation.attacks.frequency
{
    public struct RankedString
    {
        public string Word;
        public int Frequency;

        public override string ToString()
        {
            return string.Format("{0}: {1}", Word, Frequency);
        }
    }
}
