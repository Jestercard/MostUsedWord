using System;
using System.Collections.Generic;
using System.Linq;

namespace MostUsedWord
{
    class Sorter
    {
        //Lists to display to user after text is entered
        public List<string> mostUsedWord = new List<string>();
        public List<char> mostUsedLetter = new List<char>();
        public List<string> longestWord = new List<string>();
        public List<string> shortestWord = new List<string>();

        public int MostUsedWordValue { get; set; }
        public int MostUsedLetterValue { get; set; }

        //All the letters and words entered into their own seperate lists, not accounting for dupes
        List<char> rawLetters = new List<char>();
        List<string> rawWords = new List<string>();

        //Each word or letter paired with a value
        Dictionary<char, int> letterTimesUsed = new Dictionary<char, int>();
        Dictionary<string, int> wordTimeUsed = new Dictionary<string, int>();
        Dictionary<string, int> wordLengths = new Dictionary<string, int>();

        //characters to not include in any of the above lists or dictionaries
        List<char> inclusions = new List<char>() {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k',
                                                  'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v',
                                                  'w', 'x', 'y', 'z', '-', '0', '1', '2', '3', '4', '5',
                                                  '6', '7', '8', '9'};

        public void SortText(string inputText)
        {
            //turns the input text into the raw lists, where exclusions are removed but not dupes
            GetCharsToRawLetterList(inputText);
            GetWordsToRawWordList(inputText);
            //checks the raw lists for dupes and removes them, also tallies up the number of dupes by putting them into dictionaries
            CheckDictForDupes(rawLetters, letterTimesUsed);
            CheckDictForDupes(rawWords, wordTimeUsed);
            //checks the length of the words in the raw list and adds them to a different dictionary
            GetWordsToWordLengths(wordTimeUsed, wordLengths);
            //sets the lists with their appropriate strings, based on the values within the dictionaries
            GetMostUsedWordValue(wordTimeUsed, mostUsedWord);
            GetMostUsedLetterValue(letterTimesUsed, mostUsedLetter);
            FindBiggestValue(wordLengths, longestWord);
            FindShortestValue(wordLengths, shortestWord);
        }

        private void GetCharsToRawLetterList(string input)
        {
            char[] letterArray = input.ToCharArray();
            foreach(var a in letterArray)
            {
                if (inclusions.Contains(a))
                {
                    rawLetters.Add(a);
                }
            }
        }

        private void GetWordsToRawWordList(string input)
        {
            char seperator = ' ';
            string[] wordArray = input.Split(seperator, StringSplitOptions.RemoveEmptyEntries);
            foreach (var w in wordArray)
            {
                char[] charArray = w.ToCharArray();
                List<char> charList = new List<char>();
                foreach (var q in charArray)
                {
                    if (inclusions.Contains(q))
                    {
                        charList.Add(q);
                    }
                }
                char[] charArray2 = charList.ToArray();
                string cleansedword = new string(charArray2);
                rawWords.Add(cleansedword);
            }
        }

        private void CheckDictForDupes(List<char> list, Dictionary<char, int> dict)
        {
            foreach(var o in list)
            {
                if (dict.ContainsKey(o))
                {
                    dict.TryGetValue(o, out int previousCountWord);
                    dict.Remove(o);
                    dict.Add(o, ++previousCountWord);
                }
                else
                {
                    dict.Add(o, 1);
                }
            }
        }

        private void CheckDictForDupes(List<string> list, Dictionary<string, int> dict)
        {
            foreach(var o in list)
            {
                if (dict.ContainsKey(o))
                {
                    dict.TryGetValue(o, out int previousCountWord);
                    dict.Remove(o);
                    dict.Add(o, ++previousCountWord);
                }
                else
                {
                    dict.Add(o, 1);
                }
            }
        }
        
        private void GetWordsToWordLengths(Dictionary<string, int> dictInput, Dictionary<string, int> dict)
        {
            foreach(var p in dictInput)
            {
                if (!wordLengths.ContainsKey(p.Key))
                {
                    char[] array = p.Key.ToCharArray();
                    int length = array.Length;
                    dict.Add(p.Key, length);
                }
            }
        }

        private void GetMostUsedWordValue(Dictionary<string, int> dict, List<string> list)
        {
            KeyValuePair<string, int> maximum = new KeyValuePair<string, int>();
            foreach(var max in dict)
            {
                if(max.Value > maximum.Value)
                {
                    maximum = max;
                }
            }
            foreach(var word in dict)
            {
                if(word.Value == maximum.Value)
                {
                    list.Add(word.Key);
                }
            }
            MostUsedWordValue = maximum.Value;
        }

        private void GetMostUsedLetterValue(Dictionary<char, int> dict, List<char> list)
        {
            KeyValuePair<char, int> maximum = new KeyValuePair<char, int>();
            foreach (var max in dict)
            {
                if (max.Value > maximum.Value)
                {
                    maximum = max;
                }
            }
            foreach (var word in dict)
            {
                if (word.Value == maximum.Value)
                {
                    list.Add(word.Key);
                }
            }
            MostUsedLetterValue = maximum.Value;
        }

        private void FindBiggestValue(Dictionary<string, int> dict, List<string> list)
        {
            KeyValuePair<string, int> maximum = new KeyValuePair<string, int>();
            foreach (var max in dict)
            {
                if (max.Value > maximum.Value)
                {
                    maximum = max;
                }
            }
            foreach (var word in dict)
            {
                if (word.Value == maximum.Value)
                {
                    list.Add(word.Key);
                }
            }
        }

        private void FindShortestValue(Dictionary<string, int> dict, List<string> list)
        {
            KeyValuePair<string, int> minimum = new KeyValuePair<string, int>("", 2000);

            foreach (var min in wordLengths)
            {
                if (min.Value < minimum.Value)
                {
                    minimum = min;
                }
            }
            foreach (var word in dict)
            {
                if (word.Value == minimum.Value)
                {
                    list.Add(word.Key);
                }
            }
        }

        public void ClearDictsAndLists()
        {
            rawWords.Clear();
            rawLetters.Clear();
            letterTimesUsed.Clear();
            wordTimeUsed.Clear();
            wordLengths.Clear();
            mostUsedWord.Clear();
            mostUsedLetter.Clear();
            longestWord.Clear();
            shortestWord.Clear();
        }

        public void ShowDictsAndLists()
        {
            //used for debugging
            foreach(var kvp in letterTimesUsed)
            {
                Console.WriteLine($"LettersDictionary - KEY, VALUE: {kvp}");
            }
            foreach (var kvp in wordTimeUsed)
            {
                Console.WriteLine($"WordsDictionary - KEY, VALUE: {kvp}");
            }
            foreach (var kvp in wordLengths)
            {
                Console.WriteLine($"WordLengthsDictionary - KEY, VALUE: {kvp}");
            }
            foreach (var raw in rawWords)
            {
                Console.WriteLine($"Raw Words - {raw}");
            }
            foreach (var raw in rawLetters)
            {
                Console.WriteLine($"Raw Letters - {raw}");
            }
        }
    }
}