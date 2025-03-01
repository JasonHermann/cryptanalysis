using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tools
{
    public static class UseCases
    {
        /// <summary>
        /// There is a table of data on Wikipedia that lists the frequency of letters
        /// https://en.wikipedia.org/wiki/Letter_frequency#References
        /// 
        /// This is a helper that converts that table into raw frequency analysis data as a csv.
        /// The resulting file will still need cleaned-up.
        /// </summary>
        public static void WikiFrequencyAnalyis()
        {
            var path = @".\data\frequency.txt";
            var content = File.ReadAllText(path);

            var doc = new HtmlDocument();
            doc.LoadHtml(content);

            var tableNode = doc.DocumentNode.SelectSingleNode("//table");
            if (tableNode == null)
            {
                throw new InvalidOperationException("File not in the correct format it seems.");
            }

            // Select all table rows
            var rows = tableNode.SelectNodes(".//tr");

            // Prepare a CSV output file path
            string csvPath = "frequency.csv";
            using (StreamWriter writer = new StreamWriter(csvPath))
            {
                foreach (var row in rows)
                {
                    // Select all cells (td/th) in this row
                    var cells = row.SelectNodes("./th|./td");
                    if (cells != null)
                    {
                        // Extract the inner text for each cell and trim
                        // Join them with commas for CSV format
                        string csvLine = string.Join(",",
                            cells.Select(cell => cell.InnerText.Trim().Replace(",", ";")));

                        writer.WriteLine(csvLine);
                    }
                }
            }
        }


        public static void TomSawyerAnalysis()
        {
            var path = @".\data\tom_sawyer.txt";
            var content = File.ReadAllText(path);
            FileParser.ProcessCorpusForFrequencies(content);
        }
    }
}
