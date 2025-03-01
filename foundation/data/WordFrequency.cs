using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace foundation.data
{
    public static class WordFrequency
    {
        public static readonly List<string> CommonWords = new()
        {
            "the","and","a", "to", "of", "he", "was", "it", "in", "that", "his", "you", "i", "with", "but", "they", "had", "for", "him", "as", "she", "on"
        };

        public static readonly List<string> CommonDigrams = new()
        {
            "th","he","an","in", "er", "nd", "re", "ou", "ed", "to", "it","at", "ha","on","hi", "ng", "en", "as", "nt", "st", "or", "is"
        };

        public static readonly List<string> CommonTrigrams = new()
        {
            "the","and","ing","hat","her","was","ere","his","tha","you","for","ver","ith","with","tom", "ent","this","ter","ght","all","hey","out"
        };

        public static readonly Dictionary<int, List<string>> CommonWords_ByLength = new()
        {
            {1, new List<string>(){"a", "i"} },
            {2, new List<string>(){ "to","of","he","it","in","as","on","at","so", "up", "be", "by", "no", "if", "me" } },
            {3, new List<string>(){ "the", "and","was","his", "you", "tom", "but", "had", "for", "him", "she", "all", "her", "not" } },
            {4, new List<string>(){ "that", "with", "they", "said", "then", "were", "this" } },
            {5, new List<string>(){ "there", "would", "their", "could", "about", "never", "again", "began", "other", "after", "right" } },
        };

    }
}
