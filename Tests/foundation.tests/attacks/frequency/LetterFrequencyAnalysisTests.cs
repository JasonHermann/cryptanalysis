using foundation.attacks.frequency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace foundation.tests.attacks.frequency
{
    public class LetterFrequencyAnalysisTests
    {
        [Fact]
        public void FrequencyFromCorpus_HappyPath()
        {
            // Arrange
            var corpus = "AaBbCcDdEeFfGgHhIiJj"; // 10 distinct letters

            // Act
            var f = LetterFrequencyAnalysis.FrequencyFromCorpus(corpus);
            var letter_A = f['A'];
            var letter_a = f['a'];

            // Assert
            Assert.Equal(0.05, letter_A);
            Assert.Equal(0.05, letter_a);
        }

        [Fact]
        public void FrequencyFromCorpus_HappyPath_2()
        {
            // Arrange
            var corpus = "AaBbCcDdEeFfGgHhIiJj".ToLower(); // 10 distinct letters


            // Act
            var f = LetterFrequencyAnalysis.FrequencyFromCorpus(corpus);
            var contains_A = f.ContainsKey('A');
            var letter_a = f['a'];

            // Assert
            Assert.False(contains_A);
            Assert.Equal(0.10, letter_a);
        }

        [Fact]
        public void FrequencyFromCorpus_NonLettersAreIgnored()
        {
            // Arrange
            var corpus = "AaBbCcDdEeFfGgHhIiJj  .,~?!".ToLower(); // 10 distinct letters


            // Act
            var f = LetterFrequencyAnalysis.FrequencyFromCorpus(corpus);
            var contains_space = f.ContainsKey(' ');
            var letter_a = f['a'];

            // Assert
            Assert.False(contains_space);
            Assert.Equal(0.10, letter_a);
        }

        [Fact]
        public void LoadFromFile_HappyPath()
        {
            // Arrange

            // Act
            var languages = LetterFrequencyAnalysis.LoadFromFile();

            // Assert
            Assert.Equal(16, languages.Count);
        }

        [Fact]
        public void DonQuijote_HappyPath_DetectsSpanish()
        {
            // Arrange
            var corpus = ExampleText.DonQuijote_Spanish.ToLowerInvariant();

            // Act
            var languages = LetterFrequencyAnalysis.LoadFromFile();
            var rankings = LetterFrequencyAnalysis.FromCorpus(corpus, languages);

            // Assert
            Assert.Equal(16, rankings.Count);
            Assert.Equal("Spanish", rankings.GetValueAtIndex(0));
        }

        [Fact]
        public void TomSawyer_HappyPath_DetectsEnglish()
        {
            // Arrange
            var corpus = ExampleText.TomSawyer_English.ToLowerInvariant();

            // Act
            var languages = LetterFrequencyAnalysis.LoadFromFile();
            var rankings = LetterFrequencyAnalysis.FromCorpus(corpus, languages);

            // Assert
            Assert.Equal(16, rankings.Count);
            Assert.Equal("English", rankings.GetValueAtIndex(0));
        }

        [Fact]
        public void Similarity_SameStrings_Returns1()
        {
            // Arrange
            var a = "This is a test string!".ToLower();
            var b = "This is a test string?".ToLower();
            var alphabet = new HashSet<char>("abcdefghijklmnopqrstuvwxyz");

            // Act
            var similarity = LetterFrequencyAnalysis.Similarity(a, b, alphabet);

            // Assert
            Assert.Equal(1.00, similarity);
        }

        [Fact]
        public void Similarity_SimilarStrings_ReturnsCorrectValue()
        {
            // Arrange
            var a = "This is a test string!".ToLower();
            var b = "That is a best string?".ToLower();
            var alphabet = new HashSet<char>("abcdefghijklmnopqrstuvwxyz");

            // Act
            var similarity = LetterFrequencyAnalysis.Similarity(a, b, alphabet);

            // Assert
            Assert.Equal(1.00 - 3.0/17.0, similarity); // 3 of 17 wrong letters.
        }
    }
}
