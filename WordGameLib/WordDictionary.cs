using System.Collections.Generic;
using System.IO;
using AWA.TernarySearchTree;

namespace WordGameLib
{
    public class WordDictionary
    {
        private TstDictionary<string, string> Dictionary { get; } = new TstDictionary<string, string>();
        public void LoadDictionary(string dictionaryFile)
        {
            Dictionary.Clear();
            var sr = File.OpenText(dictionaryFile);
            var s = sr.ReadLine();
            while (s != null)
            {
                if (s.Length >= MinLength)
                {
                    var w = s.ToUpper();
                    Dictionary[w] = w;
                    if (s.Length > MaxLength)
                    {
                        MaxLength = s.Length;
                    }
                }
                s = sr.ReadLine();
            }
            sr.Close();
            Dictionary.BalanceSearchTree();
            Loaded = true;
        }

        public void LoadDictionary(IEnumerable<string> words)
        {
            Dictionary.Clear();
            foreach (var word in words)
            {
                if (word.Length >= MinLength)
                {
                    var w = word.ToUpper();
                    Dictionary[w] = w;
                    if (word.Length > MaxLength)
                    {
                        MaxLength = word.Length;
                    }
                }    
            }
            Dictionary.BalanceSearchTree();
            Loaded = true;
        }

        public void Clear()
        {
            Dictionary.Clear();
        }

        public bool Loaded { get; private set; }

        public int MaxLength { get; private set; }

        public int MinLength { get; set; } = 3;

        public bool PartialKeyExists(string key) => Dictionary.PartialKeyExists(key.ToUpper());

        public bool RawPartialKeyExists(string key) => Dictionary.PartialKeyExists(key);

        public bool KeyExists(string key) => Dictionary[key.ToUpper()] != null;

        public bool RawKeyExists(string key) => Dictionary[key] != null;

        public static int CompareWordsByLength(string x, string y)
        {
            if (x == null)
            {
                if (y == null)
                {
                    // If x is null and y is null, they're
                    // equal. 
                    return 0;
                }
                else
                {
                    // If x is null and y is not null, y
                    // is greater. 
                    return -1;
                }
            }
            else
            {
                // If x is not null...
                //
                if (y == null)
                    // ...and y is null, x is greater.
                {
                    return 1;
                }
                else
                {
                    // ...and y is not null, compare the 
                    // lengths of the two strings.
                    //
                    int retval = y.Length.CompareTo(x.Length);

                    if (retval != 0)
                    {
                        // If the strings are not of equal length,
                        // the longer string is greater.
                        //
                        return retval;
                    }
                    else
                    {
                        // If the strings are of equal length,
                        // sort them with ordinary string comparison.
                        //
                        return x.CompareTo(y);
                    }
                }
            }
        }

    }
}
