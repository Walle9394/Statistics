using System.IO;
using System.Text.RegularExpressions;

namespace TextStatistics
{
    /// <summary>
    /// Class for handling the selected Word
    /// </summary>
    /// <seealso cref="TextStatistics.IWordFrequency" />
    public class WordLogic : IWordFrequency
    {
        private readonly string _word;

        /// <summary>
        /// Initializes a new instance of the <see cref="WordLogic"/> class.
        /// </summary>
        /// <param name="word">The word.</param>
        public WordLogic(string word)
        {
            _word = word;
        }

        /// <summary>
        /// Gets the Word.
        /// </summary>
        /// <returns>the word</returns>
        /// * The word.
        /// * @return the word as a string.
        /// */
        public string Word()
        {
            return _word;
        }

        /// <summary>
        /// Gets the amount of occurrences of the selected Word
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>amount of occurrences of the word</returns>
        /// * The frequency.
        /// * @return a long representing the frequency of the word.
        /// *
        /// */
        public long Frequency(string filePath)
        {
            int occurrences = 0;
            string[] texts = File.ReadAllLines(filePath);
            foreach (string text in texts)
            {
                string[] textWithoutSpace = text.Split(' ');
                if (textWithoutSpace.Length > 1)
                {
                    foreach (string strippedText in textWithoutSpace)
                    {
                        occurrences += RemoveSpecialCharactersAndCompareWord(strippedText, _word);
                    }
                }
                else
                {
                    occurrences += RemoveSpecialCharactersAndCompareWord(textWithoutSpace[0], _word);
                }
            }

            return occurrences;
        }

        /// <summary>
        /// Removes the special characters and compares the selected word.
        /// </summary>
        /// <param name="wordToTrim">The word to trim.</param>
        /// <param name="wordToCompare">The word to compare.</param>
        /// <returns>Increments the amount of occurrences if it found a match</returns>
        private int RemoveSpecialCharactersAndCompareWord(string wordToTrim, string wordToCompare)
        {
            string textWithoutSpecialCharacters = Regex.Replace(wordToTrim, @"[^0-9a-zA-Z]+", "");
            if (textWithoutSpecialCharacters == wordToCompare)
            {
                return 1;
            }

            return 0;
        }
    }
}
