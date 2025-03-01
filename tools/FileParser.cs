using System.Collections.Immutable;
using System.Text;

namespace tools
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

    public static class FileParser
    {

        public static void ProcessCorpusForFrequencies(string corpus)
        {
            if (corpus == null || corpus.Length == 0)
                throw new InvalidOperationException();

            // Make Lower Case
            corpus = corpus.ToLowerInvariant();

            // Strip all special characters
            var alphabet = new HashSet<char>("abcdefghijklmnopqrstuvwxyz");

            // Strip all words
            var words = corpus.Split(new[] { " ", "\r", "\n" }, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            var wordsCount = new Dictionary<string, int>();
            var totalWords = 0;

            // Rebuild each word using only the letters from the alphabet.
            for (int i = 0; i < words.Length; i++)
            {
                var word = words[i];
                var newWord = new StringBuilder();
                foreach(var c in word)
                {
                    if(alphabet.Contains(c))
                    {
                        newWord.Append(c);
                    }
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
                if(words_byLength.ContainsKey(word.Length) == false)
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
                            var trigram = string.Concat(word[i], word[i + 1], word[i+2]);

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
