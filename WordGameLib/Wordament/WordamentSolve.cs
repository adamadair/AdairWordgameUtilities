using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordGameLib.Wordament
{
    public class WordamentSolve : WordDictionary
    {
        public const int Height = 4;
        public const int Width = 4;
  
        public string[] FindWords(IEnumerable<string> gridContents)
        {
            var results = new List<string>();
            var gridValues = gridContents.ToList();
            if (gridValues.Count() != 16)
                throw new Exception("Wordament grids require exactly 16 grid values.");

            var searchGrid = new ItemGrid<GridBlock>(4, 4, gridValues.Select(v => new GridBlock(v)));

            var sb = new StringBuilder();

            for (var x = 0; x < 4; ++x)
            for (var y = 0; y < 4; ++y)
            {
                Search(results, x, y, 0, sb, searchGrid);
            }
            results.Sort(CompareWordsByLength);
            return results.ToArray();
        }

        private void Search(List<string> results, int x, int y, int depth, StringBuilder sb, ItemGrid<GridBlock> searchGrid)
        {
            
            if (depth >= MaxLength ||
                x < 0 ||
                x >= Width ||
                y < 0 ||
                y >= Height)
            {
                return;
            }

            if (searchGrid[x, y].Searched) return;
            var block = searchGrid[x, y];
            if (block.StartOnly && depth > 0) return;


            foreach (var blockTry in block.Tries)
            {
                var end = sb.Length;
                var len = blockTry.Length;
                sb.Append(blockTry);

                string key = sb.ToString();
                if (depth > 0 && !PartialKeyExists(key))
                {
                    sb.Remove(end, len);
                    continue;
                }

                searchGrid[x, y].Searched = true;

                if (RawKeyExists(key) && key.Length >= MinLength && !results.Contains(key)) results.Add(key);

                if (!block.StopOnly)
                {
                    Search(results, x - 1, y - 1, depth + 1, sb, searchGrid);
                    Search(results, x, y - 1, depth + 1, sb, searchGrid);
                    Search(results, x + 1, y - 1, depth + 1, sb, searchGrid);
                    Search(results, x - 1, y, depth + 1, sb, searchGrid);
                    Search(results, x + 1, y, depth + 1, sb, searchGrid);
                    Search(results, x - 1, y + 1, depth + 1, sb, searchGrid);
                    Search(results, x, y + 1, depth + 1, sb, searchGrid);
                    Search(results, x + 1, y + 1, depth + 1, sb, searchGrid);
                }

                sb.Remove(end, len);
                searchGrid[x, y].Searched = false;
            }
        }

    }



    internal class GridBlock
    {

        public GridBlock(string v)
        {
            Value = v.Trim().ToUpper();
            Searched = false;
            StartOnly = Value.Length > 1 && Value.EndsWith("-");
            StopOnly = Value.Length > 1 && Value.StartsWith("-");
            var t = Value.Replace("-", "");
            Tries = t.Split("/".ToCharArray());
        }
        public string Value { get; set; }

        public bool Searched { get; set; }

        public bool StartOnly { get; }

        public bool StopOnly { get; set; }

        public string[] Tries { get; }

    }
}
