namespace TextStatistics
{
    /**
* Represents a word and its frequency.
*/
    public interface IWordFrequency
    {
        /**
        * The word.
        * @return the word as a string.
        */
        string Word();

        /**
         * The frequency.
         * @return a long representing the frequency of the word.
         * <param name="filePath"></param>
         */
        long Frequency(string filePath);
    }

}
