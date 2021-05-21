using System.Collections.Generic;
using Microsoft.VisualBasic;

namespace TextStatistics
{
    /// <summary>
    /// Main class for the application
    /// </summary>
    class TextStatisticsMain
    {
        /// <summary>
        /// Method that starts the application
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //Gets the file-path from the Pop-up and creates TextLogic objects for each file-path
            string firstFileName = Interaction.InputBox("Path to first text-file to traverse: ");
            string secondFileName = Interaction.InputBox("Path to second text-file to traverse: ");

            TextLogic firstText = new TextLogic
            {
                FileName = firstFileName
            };

            TextLogic secondText = new TextLogic
            {
                FileName = secondFileName
            };

            //Gets the 20 most frequent words for each text-file and outputs the data to the Output-window
            List<IWordFrequency> mostFrequentWordsInFirstText = firstText.TopWords(20);
            List<IWordFrequency> mostFrequentWordsInSecondText = secondText.TopWords(20);

            System.Diagnostics.Debug.WriteLine("------------------The 20 most frequent words in the first file: ------------------");
            foreach (IWordFrequency word in mostFrequentWordsInFirstText)
            {
                System.Diagnostics.Debug.WriteLine(word.Word());
            }

            System.Diagnostics.Debug.WriteLine("------------------The 20 most frequent words in the second file: ------------------");
            foreach (IWordFrequency word in mostFrequentWordsInSecondText)
            {
                System.Diagnostics.Debug.WriteLine(word.Word());
            }

            //Gets the 10 longest words for each text-file and outputs the data to the Output-window
            List<string> longestWordsInFirstText = firstText.LongestWords(10);
            List<string> longestWordsInSecondText = secondText.LongestWords(10);

            System.Diagnostics.Debug.WriteLine("------------------The 10 longest words in the first file: ------------------");
            foreach (string word in longestWordsInFirstText)
            {
                System.Diagnostics.Debug.WriteLine(word);
            }

            System.Diagnostics.Debug.WriteLine("------------------The 10 longest words in the second file: ------------------");
            foreach (string word in longestWordsInSecondText)
            {
                System.Diagnostics.Debug.WriteLine(word);
            }

            //Gets all the words for each text-file without the special characters in the word
            //and then joins the words in both list to one common list
            List<string> allWordsInFirstTextFileWithoutSpecialCharacters = firstText.AllWordsInFile(true);
            List<string> allWordsInSecondTextFileWithoutSpecialCharacters = secondText.AllWordsInFile(true);

            List<string> allWordsInAllFilesWithoutSpecialCharacters = new List<string>();
            allWordsInAllFilesWithoutSpecialCharacters.AddRange(allWordsInFirstTextFileWithoutSpecialCharacters);
            allWordsInAllFilesWithoutSpecialCharacters.AddRange(allWordsInSecondTextFileWithoutSpecialCharacters);

            //Gets all the words for each text-file with the special characters in the word
            //and then joins the words in both list to one common list
            List<string> allWordsInFirstTextFileWithSpecialCharacters = firstText.AllWordsInFile(false);
            List<string> allWordsInSecondTextFileWithSpecialCharacters = secondText.AllWordsInFile(false);

            List<string> allWordsInAllFilesWithSpecialCharacters = new List<string>();
            allWordsInAllFilesWithSpecialCharacters.AddRange(allWordsInFirstTextFileWithSpecialCharacters);
            allWordsInAllFilesWithSpecialCharacters.AddRange(allWordsInSecondTextFileWithSpecialCharacters);

            TextLogic allTexts = new TextLogic();

            //Saves the 20 most frequent words in the combines list to a another list
            //and the same goes for the longest words and then outputs the data to the Output-window
            List<IWordFrequency> mostFrequentWordsInAllFiles = allTexts.MostFrequentWords(allWordsInAllFilesWithoutSpecialCharacters, 20);
            List<string> longestWordsInAllFiles = allTexts.LongestWordsInList(allWordsInAllFilesWithSpecialCharacters, 10);

            System.Diagnostics.Debug.WriteLine("------------------The 20 most frequent words in all files: ------------------");
            foreach (IWordFrequency word in mostFrequentWordsInAllFiles)
            {
                System.Diagnostics.Debug.WriteLine(word.Word());
            }

            System.Diagnostics.Debug.WriteLine("------------------The 10 longest words in the all files: ------------------");
            foreach (string word in longestWordsInAllFiles)
            {
                System.Diagnostics.Debug.WriteLine(word);
            }

        }
    }
}
