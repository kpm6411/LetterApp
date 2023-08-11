using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetterApp
{
    public class LetterService : ILetterService
    {
        public void CombineLetters(List<DirectoryInfo> dateDirList1, List<DirectoryInfo> dateDirList2, string outputPath)
        {
            List<Letter> letters1 = new List<Letter>();
            List<Letter> letters2 = new List<Letter>();
            List<Letter> combinedLetters = new List<Letter>();

            letters1 = getLetters(dateDirList1);
            letters2 = getLetters(dateDirList2);

            foreach (Letter letter in letters1) 
            {
                int matchingLetterIndex = letters2.FindIndex(p => p.studentId == letter.studentId);
                if (matchingLetterIndex >= 0)
                {
                    Letter letter2 = letters2[matchingLetterIndex];
                    string file1 = letter.directory + letter.filename;
                    string file2 = letter2.directory + letter2.filename;
                    string resultName = "combined-" + letter.studentId;
                    CombineTwoLetters(file1, file2, resultName);
                    DeleteFile(file1);
                    DeleteFile(file2);
                } else
                {
                    string source = letter.directory + letter.filename;
                    string destination = outputPath + letter.filename;
                    File.Move(source, destination);
                }
            }
            foreach (Letter letter in letters2)
            {
                string source = letter.directory + letter.filename;
                string destination = outputPath + letter.filename;
                File.Move(source, destination);
            }
        }

        private List<Letter> getLetters(List<DirectoryInfo> dirList)
        {
            List<Letter> letters = new List<Letter>();
            foreach (DirectoryInfo dir in dirList)
            {
                foreach (FileInfo file in dir.GetFiles())
                {
                    Letter letter = new Letter();
                    letter.filename = file.Name;
                    letter.directory = file.DirectoryName;
                    letter.studentId = Int32.Parse(letter.filename.Split('-', '.')[1]);
                    letter.contents = File.ReadAllText(file.FullName);
                    letters.Add(letter);
                }
            }
            return letters;
        }

        public void CombineTwoLetters(string inputFile1, string inputFile2, string resultFile)
        {
            string outputDirectory = "./CombinedLetters/Output/";
            string combinedText = File.ReadAllText(inputFile1) + "\n\n" + File.ReadAllText(inputFile2);
            string outputFilename = outputDirectory + "combined-" + inputFile1.Split('-', '.')[1] + ".txt";
            File.WriteAllText(outputFilename, combinedText);
        }

        public void DeleteFile(string filename)
        {
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
        }

        public void DeleteDateDirectories(List<DirectoryInfo> dirList)
        {
            foreach (DirectoryInfo dir in dirList)
            {
                Directory.Delete(dir.FullName);
            }
        }
    }
}
