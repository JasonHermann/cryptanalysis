using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace foundation.attacks.frequency
{
    public class WordDensityHistogram
    {
        public WordDensityHistogram(FrequencyHistogram frequency)
        {
            Alphabet = [.. frequency.Alphabet];

            if (ReferenceEquals(Alphabet, frequency.Alphabet))
                throw new InvalidOperationException();

            double s = frequency.LetterFrequency.Sum(kvp => kvp.Value);
            LetterFrequency = new Dictionary<char, double>(frequency.LetterFrequency.Select(kvp => new KeyValuePair<char, double>(kvp.Key, kvp.Value / s)));
            s = frequency.DigramFrequency.Sum(kvp => kvp.Value);
            DigramFrequency = new Dictionary<string, double>(frequency.DigramFrequency.Select(kvp => new KeyValuePair<string, double>(kvp.Key, kvp.Value / s)));
            s = frequency.TrigramFrequency.Sum(kvp => kvp.Value);
            TrigramFrequency = new Dictionary<string, double>(frequency.TrigramFrequency.Select(kvp => new KeyValuePair<string, double>(kvp.Key, kvp.Value / s)));
        }

        public Dictionary<char, double> LetterFrequency { get; }

        public Dictionary<string, double> DigramFrequency { get; }

        public Dictionary<string, double> TrigramFrequency { get; }

        public List<char> Alphabet { get; }

    }
}
