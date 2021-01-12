using System;
using System.Collections.Generic;
using System.Text;

namespace WordGameLib.Grid
{
    public class GridSolve : WordDictionary
    {
     
        private SearchChar[,] _searchGrid;
        private int _gridWidth;
        private int _gridHeight;


        public string[] FindWords(int width, int height, string gridContents)
        {
            var results = new List<string>();
            if (width * height != gridContents.Length)
                throw new Exception("Contents length does not equal the number of cells in the grid.");

            _gridWidth = width; _gridHeight = height;

            InitSearchGrid(width, height, gridContents);
            StringBuilder sb = new StringBuilder();

            for (int x = 0; x < width; ++x)
                for (int y = 0; y < height; ++y)
                {
                    Search(results, x, y, 0, sb);
                }
            results.Sort(CompareWordsByLength);
            return results.ToArray();
        }

        private void Search(List<string> results, int x, int y, int depth, StringBuilder sb)
        {
            if (depth >= MaxLength ||
                x < 0 ||
                x >= _gridWidth ||
                y < 0 ||
                y >= _gridHeight)
            {
                return;
            }

            if (_searchGrid[x, y].Searched) return;

            sb.Append(_searchGrid[x, y].C);
            string key = sb.ToString();
            if (depth > 0 && !PartialKeyExists(key))
            {
                sb.Remove(depth, 1);
                return;
            }
            _searchGrid[x, y].Searched = true;
            
            if (RawKeyExists(key) && key.Length >= MinLength && !results.Contains(key)) results.Add(key);

            Search(results, x - 1, y - 1, depth + 1, sb);
            Search(results, x, y - 1, depth + 1, sb);
            Search(results, x + 1, y - 1, depth + 1, sb);
            Search(results, x - 1, y, depth + 1, sb);
            Search(results, x + 1, y, depth + 1, sb);
            Search(results, x - 1, y + 1, depth + 1, sb);
            Search(results, x, y + 1, depth + 1, sb);
            Search(results, x + 1, y + 1, depth + 1, sb);

            sb.Remove(depth, 1);
            _searchGrid[x, y].Searched = false;
        }

        private void InitSearchGrid(int x, int y, string contents)
        {
            var index = 0;
            _searchGrid = new SearchChar[x, y];
            for (var i = 0; i < y; ++i)
                for (var j = 0; j < x; ++j)
                {
                    _searchGrid[j, i] = new SearchChar {C = contents[index], Searched = false};
                    ++index;
                }
        }

    }

    internal struct SearchChar
    {
        public char C;
        public bool Searched;
    }
}
