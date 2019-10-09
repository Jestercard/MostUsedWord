using System;
using System.Collections.Generic;
using System.Threading;
using System.Collections;

namespace MostUsedWord
{
    class UserInterface
    {
        public string inputText;
        public bool endProgram = false;

        public bool debug = true;

        Sorter sorter = new Sorter();

        List<string> statsList = new List<string>()
        { "Most Used Word", "Most Used Letter", "Longest Word", "Shortest Word" };

        ArrayList statsListFilled = new ArrayList();

        public void Loop()
        {
            IntroScreen();
            while (!endProgram)
            {
                ClearStats();
                GetText();
                ResultsIntoFilledList();
                DisplayStats();
                NewTextOrExit();
            }
            EndProgram();
        }

        private void ClearStats()
        {
            statsListFilled.Clear();
            sorter.ClearDicts();
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
            sorter.SortText(inputText);
            statsListFilled.Add(sorter.mostUsedWord);
            statsListFilled.Add(sorter.mostUsedLetter);
            statsListFilled.Add(sorter.longestWord);
            statsListFilled.Add(sorter.shortestWord);
            if (debug)
            {
                sorter.ShowDictsFull();
            }
        }

        private void DisplayStats()
        {
            Console.WriteLine("");
            Console.WriteLine("Here are the stats for your entered Text");
            Console.WriteLine("________________________________________");
            Console.WriteLine($"{statsList[0]}: {statsListFilled[0]}");
            Console.WriteLine($"{statsList[1]}: {statsListFilled[1]}");
            Console.WriteLine($"{statsList[2]}: {statsListFilled[2]}");
            Console.WriteLine($"{statsList[3]}: {statsListFilled[3]}");
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
        }
        private void EndProgram()
        {
            Console.WriteLine("");
            Console.WriteLine("GoodBye! Press Any Key to Exit.");
        }
    }
}
