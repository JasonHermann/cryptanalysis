using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace foundation.attacks.frequency
{
    public static class LetterFrequencyAnalysis
    {
        public static char[] IgnoreChars = ['\r','\n',' ', ',', '.', '~', '!', '?', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '(', ')', '[',']', '-', ';', ':', '_'];
        public static HashSet<char> Ignore = [.. IgnoreChars];

        public static SortedList<double, string> FromCorpus(string corpus, List<Language> languages)
        {
            var cf = FrequencyFromCorpus(corpus);

            // Given a corpus, evaluate the MSE for each target language and return the most
            // probable language.
            var output = new SortedList<double, string>();
            foreach (var l in languages)
            {
                var mse = l.Frequencies.MSE(cf);
                output.Add(mse, l.Name);
            }

            return output;
        }

        public static Dictionary<char, double> FrequencyFromCorpus(string corpus)
        {
            var output = new Dictionary<char, double>();
            double count = 0;
            foreach (var c in corpus)
            {
                if (Ignore.Contains(c)) continue; // Ignore some characters

                if (output.ContainsKey(c) == false)
                {
                    output.Add(c, 0);
                }
                output[c]++;
                count++;
            }
            foreach (var c in output)
            {
                output[c.Key] = output[c.Key] / count;
            }

            return output;
        }

        public static List<Language> LoadFromFile(string relativePath = @".\data\frequency.csv")
        {
            var csv = File.ReadAllText(relativePath);

            var lines = csv.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            var header = lines[0].Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var languageNames = new List<string>(header.Skip(1));
            var languageFrequencies = new List<Dictionary<char, double>>(languageNames.Select(x => new Dictionary<char, double>()));

            foreach(var line in lines.Skip(1)) // Skip the header row
            {
                var values = line.Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                var letter = values[0];
                if(char.TryParse(letter, out char c))
                {
                    int index = 0;
                    foreach (var letterFrequency in values.Skip(1))
                    {
                        var freqAsPercent = letterFrequency.Replace("%", "");
                        if (double.TryParse(freqAsPercent, out double result))
                        {
                            languageFrequencies[index].Add(c, result / 100.00);
                        }
                        else
                        {
                            throw new NotSupportedException();
                        }
                        index++;
                    }

                }
                else
                {
                    // For now let's ignore letters that don't parse
                    // There are some welsh letters that don't work so good.
                    //throw new NotSupportedException();
                }
            }

            var output = new List<Language>();
            for(int i = 0; i < languageNames.Count; i++)
            {
                output.Add(new Language(languageNames[i], languageFrequencies[i]));
            }
            return output;
        }

        /// <summary>
        /// Given two strings of the same length and an alphabet.
        /// For every character in a that appears in alphabet, is b the same character?
        /// </summary>
        /// <returns>Percentage of similarity</returns>
        /// <remarks>If no characters are comparable return 1.00 rather than divide by zero.</remarks>
        public static double Similarity(string a, string b, HashSet<char> alphabet)
        {
            if (a == null || b == null || alphabet == null)
                throw new ArgumentNullException();
            if (a.Length != b.Length)
                throw new InvalidOperationException("Both strings must be the same length.");

            if (a.Length == 0)
                return 1.00;

            int common = 0;
            int count = 0;
            for(int i = 0; i < a.Length; i++)
            {
                if (alphabet.Contains(a[i]))
                {
                    count++;
                    if (a[i] == b[i])
                        common++;
                }
            }

            return (double)common / (double)count;
        }
    }
}
