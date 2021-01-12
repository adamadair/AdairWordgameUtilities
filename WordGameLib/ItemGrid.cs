using System;
using System.Collections.Generic;
using System.Linq;

namespace WordGameLib
{
    class ItemGrid<T>
    {
        public int Height { get; }
        
        public int Width { get; }

        private List<T> Values { get; }
        public ItemGrid(int height, int width, IEnumerable<T> values)
        {
            this.Height = height;
            this.Width = width;
            this.Values = values.ToList();
            if (height * width != Values.Count)
            {
                throw new Exception($"{height} x {width} grid requires {height * width} values.");
            }

        }

        public T this[int row, int col]
        {
            get
            {
                if (row < 0 || row >= Height || col < 0 || col >= Width)
                {
                    throw new IndexOutOfRangeException("Invalid index into grid.");
                }

                var index = row * Width + col;
                return Values[index];
            }
            set
            {
                if (row < 0 || row >= Height || col < 0 || col >= Width)
                {
                    throw new IndexOutOfRangeException("Invalid index into grid.");
                }

                var index = row * Width + col;
                Values[index] = value;
            }
        }

    }
}
