using System;

namespace HW_AIR_S19.Models.TextProcessing
{ 
        /// <summary>
        /// 
        /// </summary>
    public interface IStemming
        {
            /// <summary>
            /// Returns the stemmed form of a word
            /// </summary>
            /// <param name="word">The word to stem. It must be capitalized</param>
            /// <returns>The stemmed word</returns>
            string StemWord(string word);
    }
}
