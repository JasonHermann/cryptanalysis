using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace foundation.attacks.frequency
{
    public interface ILetterFrequency
    {
        HashSet<string> Ignored { get; }
        HashSet<char> Alphabet { get; }
        Dictionary<char, int> LetterFrequency { get; }
    }

    public interface IWordFrequency
    {
        HashSet<char> WordDelimiters { get; }
        Dictionary<int, Dictionary<string, int>> WordsByLength { get; }
        Dictionary<string, int> WordFrequency { get; }
    }
    public interface InGramFrequency
    {
        Dictionary<int, Dictionary<string, int>> WordsByLength { get; }
        Dictionary<string, int> WordFrequency { get; }
    }


    public class FrequencyHistogram : IWordFrequency, InGramFrequency, ILetterFrequency
    {
        public readonly string[] DefaultIgnore = [" ", "\r", "\n", "\t", ",", ".", "!", "?", ";", ":", "(", ")",
            "1","2","3","4","5","6","7","8","9","0"];

        public readonly string[] DefaultDelimiters = [" ", "\r", "\n"];

        public static FrequencyHistogram MakeHistogramFromCorpus()
        {
            return null;
        }

        public static FrequencyHistogram MakeHistogramFromCorpus_RestrictedAlphabet()
        {
            return null;
        }

        FrequencyHistogram(List<string> corpus, string[]? ignore = null)
        {
            corpus.ForEach(x => ProcessCorpusForFrequencies(x, ignore ?? DefaultIgnore));
        }

        public Dictionary<char, int> LetterFrequency { get; } = [];

        public Dictionary<string, int> DigramFrequency { get; } = [];

        public Dictionary<string, int> TrigramFrequency { get; } = [];

        public Dictionary<int, Dictionary<string, int>> WordsByLength { get; } = [];

        public Dictionary<string, int> WordFrequency { get; } = [];

        public HashSet<char> Alphabet { get; } = [];

        public HashSet<string> Ignored { get; } = [];

        public HashSet<char> WordDelimiters { get; } = [];

        void ProcessCorpusForFrequencies(string corpus, string[] ignore)
        {
            if (corpus == null || corpus.Length == 0)
                throw new InvalidOperationException();

            // Make Lower Case
            corpus = corpus.ToLowerInvariant();

            // Strip all words
            var words = corpus.Split(ignore, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            var wordsCount = new Dictionary<string, int>();
            var totalWords = 0;

            // Rebuild each word using only the letters from the alphabet.
            for (int i = 0; i < words.Length; i++)
            {
                var word = words[i];
                var newWord = new StringBuilder();
                foreach (var c in word)
                {
                    if (Alphabet.Contains(c) == false)
                    {
                        Alphabet.Add(c);
                    }
                    newWord.Append(c);
                }
                var w = newWord.ToString();
                if (wordsCount.ContainsKey(w) == false)
                {
                    wordsCount.Add(w, 0);
                }
                wordsCount[w]++;
                totalWords++;
            }

            // Measure words of various lengths
            // Measure digrams and trigrams
            // Ignore certain characters
            var digrams = new Dictionary<string, int>();
            var trigrams = new Dictionary<string, int>();
            var words_byLength = new Dictionary<int, Dictionary<string, int>>();

            var totalDigrams = 0;
            var totalTrigrams = 0;
            var totalWords_byLength = new Dictionary<int, int>();

            foreach (var wc in wordsCount)
            {
                // This word.Key
                var word = wc.Key;
                var count = wc.Value;

                // add this to the dictionary of words of a specific length
                if (words_byLength.ContainsKey(word.Length) == false)
                {
                    words_byLength.Add(word.Length, new Dictionary<string, int>());
                    totalWords_byLength.Add(word.Length, 0);
                }
                var d = words_byLength[word.Length];

                if (d.ContainsKey(word) == false)
                {
                    d.Add(word, 0);
                }
                d[word] = d[word] + count;
                totalWords_byLength[word.Length] += count;

                // Break up the word into digrams
                if (word.Length > 1)
                {
                    for (int i = 0; i < word.Length - 1; i++)
                    {
                        var digram = string.Concat(word[i], word[i + 1]);

                        if (digrams.ContainsKey(digram) == false)
                        {
                            digrams.Add(digram, 0);
                        }
                        digrams[digram] = digrams[digram] + count;
                        totalDigrams += count;
                    }

                    // Break up the word into trigrams
                    if (word.Length > 2)
                    {
                        for (int i = 0; i < word.Length - 2; i++)
                        {
                            var trigram = string.Concat(word[i], word[i + 1], word[i + 2]);

                            if (trigrams.ContainsKey(trigram) == false)
                            {
                                trigrams.Add(trigram, 0);
                            }
                            trigrams[trigram] = trigrams[trigram] + count;
                            totalTrigrams += count;
                        }
                    }
                }

            }


            var rankedWords = new List<RankedString>();
            var rankedDigrams = new List<RankedString>();
            var rankedTrigrams = new List<RankedString>();
            var rankedWords_byLength = new Dictionary<int, List<RankedString>>();
            foreach (var word in wordsCount)
            {
                rankedWords.Add(new RankedString() { Frequency = word.Value, Word = word.Key });
            }
            rankedWords = rankedWords.OrderByDescending(r => r.Frequency).ToList();

            foreach (var word in digrams)
            {
                rankedDigrams.Add(new RankedString() { Frequency = word.Value, Word = word.Key });
            }
            rankedDigrams = rankedDigrams.OrderByDescending(r => r.Frequency).ToList();

            foreach (var word in trigrams)
            {
                rankedTrigrams.Add(new RankedString() { Frequency = word.Value, Word = word.Key });
            }
            rankedTrigrams = rankedTrigrams.OrderByDescending(r => r.Frequency).ToList();

            foreach (var size in words_byLength)
            {
                var rank = new List<RankedString>();
                foreach (var word in size.Value)
                {
                    rank.Add(new RankedString() { Frequency = word.Value, Word = word.Key });
                }
                rank = rank.OrderByDescending(r => r.Frequency).ToList();
                rankedWords_byLength.Add(size.Key, rank);
            }

        }
    }
}
