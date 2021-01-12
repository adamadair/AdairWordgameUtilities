using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using WordGameLib.Wordament;

namespace WordGameLibTests
{
    [TestClass]
    public class WordamentTests
    {
        private const string EASY_BOARD = "D O L T "
                                        + "L N E R "
                                        + "I N U W "
                                        + "L F E R";


        [TestMethod]
        public void WordamentSolveEasyTest()
        {
            var wordList = new List<string>
            {
                "LINE",
                "LINER",
                "FUR",
                "FUN",
                "FRANK",
                "TON",
                "LIFE",
                "DONE",
                "DOLT",
                "TRUE",
                "REFINERY"
            };
            WordamentSolve solver = new WordamentSolve();
            solver.LoadDictionary(wordList);
            var result = solver.FindWords(EASY_BOARD.Split());
            Assert.IsTrue(solver.KeyExists("line"));
            Assert.IsTrue(solver.PartialKeyExists("line"));
            Assert.IsTrue(solver.PartialKeyExists("li"));
            Assert.IsTrue(solver.RawPartialKeyExists("LINE"));
            Assert.IsFalse(solver.RawPartialKeyExists("li"));
            Assert.AreEqual(8,result.Length);
            foreach (var s in result)
            {
                Console.WriteLine(s);
            }
        }

        private const string SPLIT_LETTER_BOARD = "D O L T/Y "
                                                + "L N E R "
                                                + "I N U S "
                                                + "L F E R";

        [TestMethod]
        public void WordamentSolveSplitletterTest()
        {
            var wordList = new List<string>
            {
                "LINE",
                "LINER",
                "FUR",
                "FUN",
                "FRANK",
                "TON",
                "LIFE",
                "DONE",
                "DOLT",
                "TRUE",
                "REFINERY",
                "REFINER",
                "REFINE",
                "YES",
                "LYE",
                "REF"
            };
            WordamentSolve solver = new WordamentSolve();
            solver.LoadDictionary(wordList);
            var result = solver.FindWords(SPLIT_LETTER_BOARD.Split());

            
            foreach (var s in result)
            {
                Console.WriteLine(s);
            }

            Assert.AreEqual(14, result.Length);
        }


        private const string MULTI_LETTER_BOARD   = "D O L T " 
                                                  + "L N I S "
                                                  + "I N U T "
                                                  + "L P ER A";

        [TestMethod]
        public void WordamentSolveMultiLetterTest()
        {
            var wordList = new List<string>
            {
                "LINE",
                "LINER",
                "FUR",
                "ERA",
                "PERT",

            };
            WordamentSolve solver = new WordamentSolve();
            solver.LoadDictionary(wordList);
            var result = solver.FindWords(MULTI_LETTER_BOARD.Split());


            foreach (var s in result)
            {
                Console.WriteLine(s);
            }

            Assert.AreEqual(3, result.Length);
        }

        private const string STARTONLY_LETTER_BOARD = "D O L T "
                                                  + "L N I S "
                                                  + "I N U T "
                                                  + "L P ER- A";

        [TestMethod]
        public void WordamentSolveStartLetterTest()
        {
            var wordList = new List<string>
            {
                "LINE",
                "LINER",
                "FUR",
                "ERA",
                "PERT",

            };
            WordamentSolve solver = new WordamentSolve();
            solver.LoadDictionary(wordList);
            var result = solver.FindWords(STARTONLY_LETTER_BOARD.Split());


            foreach (var s in result)
            {
                Console.WriteLine(s);
            }

            Assert.AreEqual(1, result.Length);
        }

        private const string STOPONLY_LETTER_BOARD = "D O L T "
                                                      + "L N I S "
                                                      + "I N U T "
                                                      + "L P -ER A";

        [TestMethod]
        public void WordamentSolveStopLetterTest()
        {
            var wordList = new List<string>
            {
                "LINE",
                "LINER",
                "FUR",
                "ERA",
                "PERT",

            };
            WordamentSolve solver = new WordamentSolve();
            solver.LoadDictionary(wordList);
            var result = solver.FindWords(STOPONLY_LETTER_BOARD.Split());


            foreach (var s in result)
            {
                Console.WriteLine(s);
            }

            Assert.AreEqual(1, result.Length);
            Assert.IsTrue(result.Contains("LINER"));
        }

    }
}
