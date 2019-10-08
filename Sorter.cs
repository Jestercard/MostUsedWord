using System;
using System.Collections.Generic;
using System.Linq;

namespace MostUsedWord
{
    class Sorter
    {
        public string mostUsedWord = "null";
        public char mostUsedLetter = ' ';
        public string longestWord = "null";
        public string shortestWord = "null";

        List<string> rawWords = new List<string>();
        Dictionary<char, int> letters = new Dictionary<char, int>();
        Dictionary<string, int> words = new Dictionary<string, int>();
        Dictionary<string, int> wordLengths = new Dictionary<string, int>();

        public void SortText(string inputText)
        {
            string inputTextLower = inputText.ToLower();
            char[] letterArray = inputTextLower.ToCharArray();
            List<char> tempWord = new List<char>();
            foreach (char c in letterArray)
            {
                if (c != ' ')
                {
                    if (letters.ContainsKey(c))
                    {
                        letters.TryGetValue(c, out int previousCountLetter);
                        letters.Remove(c);
                        letters.Add(c, ++previousCountLetter);
                    }
                    else
                    {
                        letters.Add(c, 1);
                    }
                    tempWord.Add(c);
                }
                else
                {
                    CheckWordsForDupes(tempWord);
                }
            }
            CheckWordsForDupes(tempWord);
            SetStats();
        }

        private void CheckWordsForDupes(List<char> tempWord)
        {
            char[] wordCharsArray = tempWord.ToArray();
            string word = new string(wordCharsArray);
            rawWords.Add(word);
            if (words.ContainsKey(word))
            {
                words.TryGetValue(word, out int previousCountWord);
                words.Remove(word);
                words.Add(word, ++previousCountWord);
            }
            else
            {
                words.Add(word, 1);
                wordLengths.Add(word, tempWord.Count);
            }
            tempWord.Clear();
        }

        private void SetStats()
        {
            SetMostUsedWord();
            SetMostUsedLetter();
            SetLongestWord();
            SetShortestWord();
        }

        private void SetMostUsedWord()
        {
            KeyValuePair<string, int> maxWord= new KeyValuePair<string, int>();

            foreach(var mw in words)
            {
                if(mw.Value > maxWord.Value)
                {
                    maxWord = mw;
                }
            }
            mostUsedWord = maxWord.Key;
        }
        private void SetMostUsedLetter()
        {
            KeyValuePair<char, int> maxLetter = new KeyValuePair<char, int>();

            foreach (var ml in letters)
            {
                if (ml.Value > maxLetter.Value)
                {
                    maxLetter = ml;
                }
            }

            mostUsedLetter = maxLetter.Key;
        }
        private void SetLongestWord()
        {
            KeyValuePair<string, int> bigWord = new KeyValuePair<string, int>();

            foreach (var bw in wordLengths)
            {
                if (bw.Value > bigWord.Value)
                {
                    bigWord = bw;
                }
            }
            longestWord = bigWord.Key;
        }
        private void SetShortestWord()
        {
            KeyValuePair<string, int> littleWord = new KeyValuePair<string, int>("null", 100);

            foreach (var lw in wordLengths)
            {
                if (lw.Value < littleWord.Value)
                {
                    littleWord = lw;
                }
            }
            shortestWord = littleWord.Key;
        }

        public void ClearDicts()
        {
            rawWords.Clear();
            words.Clear();
            letters.Clear();
            wordLengths.Clear();
        }

        public void ShowDictsFull()
        {
            foreach(var o in rawWords)
            {
                Console.WriteLine($"RawWordList: {o}");
            }
            foreach(var kvp in letters)
            {
                Console.WriteLine($"LettersDictionary - KEY, VALUE: {kvp}");
            }
            foreach (var kvp in words)
            {
                Console.WriteLine($"WordsDictionary - KEY, VALUE: {kvp}");
            }
            foreach (var kvp in wordLengths)
            {
                Console.WriteLine($"WordLengthsDictionary - KEY, VALUE: {kvp}");
            }
        }
    }
}