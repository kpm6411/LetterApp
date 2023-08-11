using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetterApp
{
    public interface ILetterService
    {
        /// <summary>
        /// Combine two letter files into one file.
        /// </summary>
        /// <param name="inputFile1">File path for the first letter.</param>
        /// <param name="inputFile2">File path for the second letter.</param>
        /// <param name="resultFile">File path for the combined letter.</param>
        void CombineTwoLetters(string inputFile1, string inputFile2, string resultFile);
    }
}
