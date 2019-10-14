using System;

namespace MostUsedWord
{
    class UserInterface
    {
        public string inputText;
        private bool endProgram = false;
        public bool debug = true;

        Sorter sorter = new Sorter();

        public void Loop()
        {
            IntroScreen();
            while (!endProgram)
            {
                sorter.ClearDictsAndLists();
                GetText();
                ResultsIntoFilledList();
                DisplayStats();
                NewTextOrExit();
            }
            EndProgram();
        }

        private void IntroScreen()
        {
            Console.WriteLine("Hello! Welcome to the Text Analyzer");
            Console.WriteLine("___________________________________");
        }

        private void GetText()
        {
            Console.WriteLine("");
            Console.WriteLine("Please write your text below to be Analyzed: ");
            Console.WriteLine("");
            inputText = Console.ReadLine();
        }

        private void ResultsIntoFilledList()
        {
            sorter.SortText(inputText.ToLower());
            //shows the full list of each dictionary and list if debug option is enabled
            if (debug)
            {
                sorter.ShowDictsAndLists();
            }
        }

        private void DisplayStats()
        {

            Console.WriteLine("");
            Console.WriteLine("Here are the stats for your entered Text");
            Console.WriteLine("________________________________________");
            Console.WriteLine($"Most Used Word(s) appears {sorter.MostUsedWordValue} time(s):");
            foreach (var c in sorter.mostUsedWord)
            {
                Console.WriteLine(" " + c);
            }
            Console.WriteLine($"Most Used Letter(s) appears {sorter.MostUsedLetterValue} time(s):");
            foreach (var c in sorter.mostUsedLetter)
            {
                Console.WriteLine(" " + c);
            }
            Console.WriteLine("Longest Word(s):");
            foreach (var c in sorter.longestWord)
            {
                Console.WriteLine(" " + c);
            }
            Console.WriteLine("Shortest Word(s):");
            foreach (var c in sorter.shortestWord)
            {
                Console.WriteLine(" " + c);
            }

        }

        private void NewTextOrExit()
        {
            Console.WriteLine("");
            Console.WriteLine("Would you like to analyze another text? Y/N");
            string redo = Console.ReadLine();
            if (redo.ToLower() == "n")
            {
                endProgram = true;
            }
            Console.Clear();
        }

        private void EndProgram()
        {
            Console.WriteLine("");
            Console.WriteLine("GoodBye! Press Any Key to Exit.");
        }
    }
}
