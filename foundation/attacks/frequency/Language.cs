using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace foundation.attacks.frequency
{
    public class Language(string name, Dictionary<char, double> frequencies)
    {
        public string Name { get; } = name;

        public Dictionary<char, double> Frequencies { get; } = frequencies;
    }

    public class DetailedLanguage : Language
    {
        public DetailedLanguage(string name, Dictionary<char, double> frequencies) : base(name, frequencies)
        {
        }

        /// <summary>
        /// For a specific size of word, return a list of the most common words of that length.
        /// Percentages are based on words of that size.
        /// 
        /// Not all percentages will add to 100 as only words of a threshold frequency will be included.
        /// </summary>
        public Dictionary<int, SortedList<double, string>> WordFrequencyByLength { get; set; } = new Dictionary<int, SortedList<double, string>>();

        /// <summary>
        /// Return the most common words.
        /// Percentages are based on all words.
        /// 
        /// Not all percentages will add to 100 as only words of a threshold frequency will be included.
        /// </summary>
        public SortedList<double, string> WordFrequency { get; set; } = new SortedList<double, string>();

        /// <summary>
        /// Return the most digrams. Percentages are based on all digrams.
        /// 
        /// Not all percentages will add to 100 as only digrams of a threshold frequency will be included.
        /// </summary>
        public SortedList<double, string> Digram { get; set; } = new SortedList<double, string>();

        /// <summary>
        /// Return the most trigrams. Percentages are based on all trigrams.
        /// 
        /// Not all percentages will add to 100 as only trigrams of a threshold frequency will be included.
        /// </summary>
        public SortedList<double, string> Trigram { get; set; } = new SortedList<double, string>();
    }
}
