using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace TextStatistics
{
    /// <summary>
    /// Class for traversing text-files
    /// </summary>
    /// <seealso cref="TextStatistics.ITextStatistics" />
    public class TextLogic : ITextStatistics
    {
        public string FileName { get; set; }

        /// <summary>
        /// Gets all the words in the text file and then gets the most frequent words in the file
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        /// * Returns a list of the most frequented words of the text.
        /// * @param n how many items of the list
        /// * @return a list representing the top n frequent words of the text.
        /// */
        public List<IWordFrequency> TopWords(int n)
        {
            List<string> allWordsInFile = AllWordsInFile(true);
            List<IWordFrequency> mostFrequentWordsInFile = MostFrequentWords(allWordsInFile, n);

            return mostFrequentWordsInFile;
        }

        /// <summary>
        /// Gets the most frequent words in the text-file.
        /// </summary>
        /// <param name="allWordsInFile">All words in file.</param>
        /// <param name="n">The n.</param>
        /// <returns>A list of the most frequent words</returns>
        public List<IWordFrequency> MostFrequentWords(List<string> allWordsInFile, int n)
        {
            List<IGrouping<string, string>> frequentWordsInFile = allWordsInFile.GroupBy(x => x).OrderByDescending(x => x.Count()).ToList();

            List<IWordFrequency> mostFrequentWords = new List<IWordFrequency>();

            for (int i = 0; i < n; i++)
            {
                IWordFrequency wordFrequency = new WordLogic(frequentWordsInFile[i].Key);
                mostFrequentWords.Add(wordFrequency);
            }

            return mostFrequentWords;
        }

        /// <summary>
        /// All words in the text-file.
        /// </summary>
        /// <param name="withoutSpecialCharacters">if set to <c>true</c> [without special characters].</param>
        /// <returns>Returns all the words in the files with or without special characters</returns>
        public List<string> AllWordsInFile(bool withoutSpecialCharacters)
        {
            if (string.IsNullOrEmpty(FileName))
            {
                MessageBox.Show("A file-path needs to be provided.");
                return null;
            }

            List<string> allWordsInFile = new List<string>();

            string[] texts = File.ReadAllLines(FileName, Encoding.Default);
            if (texts.Length == 0)
            {
                MessageBox.Show("Could not read the file. Check if the file-path is correct.");
                return null;
            }
            foreach (string text in texts)
            {
                string[] textWithoutSpace = text.Split(' ');
                if (textWithoutSpace.Length > 1)
                {
                    foreach (string strippedText in textWithoutSpace)
                    {
                        if (withoutSpecialCharacters)
                        {
                            RemoveSpecialCharactersAndAddToList(strippedText, allWordsInFile);
                        }
                        else
                        {
                            RemoveSpecialCharactersAndAddOriginalWordToList(strippedText, allWordsInFile);
                        }
                    }
                }
                else
                {
                    if (withoutSpecialCharacters)
                    {
                        RemoveSpecialCharactersAndAddToList(textWithoutSpace[0], allWordsInFile);
                    }
                    else
                    {
                        RemoveSpecialCharactersAndAddOriginalWordToList(textWithoutSpace[0], allWordsInFile);
                    }
                }
            }

            return allWordsInFile;
        }

        /// <summary>
        /// Gets all the words in the text-file and then gets the longest words from all the words in the text-file
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        /// * Returns a list of the longest words of the text.
        /// * @param n how many items to return.
        /// * @return a list with the n longest words of the text.
        /// */
        public List<string> LongestWords(int n)
        {
            List<string> allWordsInFile = AllWordsInFile(false);
            List<string> topMostLongestWords = LongestWordsInList(allWordsInFile, n);

            return topMostLongestWords;
        }

        /// <summary>
        /// Gets the longest words in the text-file.
        /// </summary>
        /// <param name="allWordsInFile">All words in file.</param>
        /// <param name="n">The n.</param>
        /// <returns>A list of top-most longest words</returns>
        public List<string> LongestWordsInList(List<string> allWordsInFile, int n)
        {
            List<string> longestWordsInWholeFile = allWordsInFile.Distinct().OrderByDescending(x => x.Length).ToList();
            List<string> topMostLongestWords = new List<string>();

            for (int i = 0; i < n; i++)
            {
                topMostLongestWords.Add(longestWordsInWholeFile[i]);
            }

            return topMostLongestWords;
        }

        /// <summary>
        /// Gets the amount fo words in the text-file
        /// </summary>
        /// <returns>amount of words in the text-file</returns>
        /// * @return total number of words in the text.
        /// */
        public long NumberOfWords()
        {
            int numberOfWords = 0;
            string[] texts = File.ReadAllLines(FileName);
            foreach (string text in texts)
            {
                string[] textWithoutSpace = text.Split(' ');
                if (textWithoutSpace.Length > 1)
                {
                    foreach (string strippedText in textWithoutSpace)
                    {
                        numberOfWords += RemoveSpecialCharacters(strippedText);
                    }
                }
                else
                {
                    numberOfWords += RemoveSpecialCharacters(textWithoutSpace[0]);
                }
            }

            return numberOfWords;
        }

        /// <summary>
        /// Gets the total number of lines in the text-file
        /// </summary>
        /// <returns></returns>
        /// * @return total number of line of the text.
        /// */
        public long NumberOfLines()
        {
            string[] texts = File.ReadAllLines(FileName);
            return texts.Length;
        }

        /// <summary>
        /// Removes the special characters.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns>Increments the amount of occurrences</returns>
        private int RemoveSpecialCharacters(string word)
        {
            string textWithoutSpecialCharacters = Regex.Replace(word, @"[^0-9a-zA-Z]+", "");
            if (textWithoutSpecialCharacters != string.Empty)
            {
                return 1;
            }

            return 0;
        }

        /// <summary>
        /// Removes the special characters and adds to words-list.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <param name="wordsList">The words list.</param>
        private void RemoveSpecialCharactersAndAddToList(string word, List<string> wordsList)
        {
            string textWithoutSpecialCharacters = Regex.Replace(word, @"[^0-9a-zA-Z]+", "");
            if (textWithoutSpecialCharacters != string.Empty)
            {
                wordsList.Add(textWithoutSpecialCharacters);
            }
        }

        /// <summary>
        /// Removes the special characters and adds the original word to words-list.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <param name="wordsList">The words list.</param>
        private void RemoveSpecialCharactersAndAddOriginalWordToList(string word, List<string> wordsList)
        {
            string textWithoutSpecialCharacters = Regex.Replace(word, @"[^0-9a-zA-Z]+", "");
            if (textWithoutSpecialCharacters != string.Empty)
            {
                wordsList.Add(word);
            }
        }
    }
}
