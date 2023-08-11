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
        public void CombineLetters(List<DirectoryInfo> dateDirList1, List<DirectoryInfo> dateDirList2, string outputPath, string archivePath)
        {
            List<Letter> letters1 = new List<Letter>();
            List<Letter> letters2 = new List<Letter>();
            List<Letter> archivedLetters = new List<Letter>();

            if (!Directory.Exists(outputPath)) Directory.CreateDirectory(outputPath);
            letters1 = getLetters(dateDirList1);
            letters2 = getLetters(dateDirList2);
            if (Directory.Exists(archivePath))
            {
                archivedLetters = getLetters(new DirectoryInfo(archivePath).GetDirectories().ToList());

                foreach (Letter letter in archivedLetters)
                {
                    if (letter.filename.Split('-')[0] == "admission")
                    {
                        letters1.Add(letter);
                    }
                    else
                    {
                        letters2.Add(letter);
                    }
                }
            }

            foreach (Letter letter in letters1)
            {
                int matchingLetterIndex = letters2.FindIndex(p => p.studentId == letter.studentId);
                if (matchingLetterIndex >= 0)
                {
                    Letter letter2 = letters2[matchingLetterIndex];
                    string file1 = letter.directory + "\\" + letter.filename;
                    string file2 = letter2.directory + "\\" + letter2.filename;
                    string resultName = outputPath + "combined-" + letter.studentId + ".txt";
                    CombineTwoLetters(file1, file2, resultName);
                    if (!Directory.Exists(archivePath)) Directory.CreateDirectory(archivePath);
                    MoveFile(file1, archivePath + "\\" + letter.filename);
                    MoveFile(file2, archivePath + "\\" + letter2.filename);
                } else
                {
                    string source = letter.directory + "\\" + letter.filename;
                    string destination = archivePath + "\\" + letter.filename;
                    if (!Directory.Exists(archivePath)) Directory.CreateDirectory(archivePath);
                    MoveFile(source, destination);
                }
            }
            foreach (Letter letter in letters2)
            {
                string source = letter.directory + "\\" + letter.filename;
                string destination = archivePath + "\\" + letter.filename;
                MoveFile(source, destination);
            }

            createReport(outputPath);
        }

        private List<Letter> getLetters(List<DirectoryInfo> dirList)
        {
            List<Letter> letters = new List<Letter>();
            foreach (DirectoryInfo dir in dirList)
            {
                foreach (FileInfo file in dir.GetFiles())
                {
                    if (file.Name != "report.txt")
                    {
                        Letter letter = new Letter();
                        letter.filename = file.Name;
                        letter.directory = file.DirectoryName;
                        letter.studentId = Int32.Parse(letter.filename.Split('-', '.')[1]);
                        letter.contents = File.ReadAllText(file.FullName);
                        letters.Add(letter);
                    }
                }
            }
            return letters;
        }

        public void CombineTwoLetters(string inputFile1, string inputFile2, string resultFile)
        {
            string combinedText = File.ReadAllText(inputFile1) + "\n\n" + File.ReadAllText(inputFile2);
            File.WriteAllText(resultFile, combinedText);
        }

        public void MoveFile(string source, string destination)
        {
            if (File.Exists(source))
            {
                File.Move(source, destination, true);
            }
        }

        public void DeleteDateDirectories(List<DirectoryInfo> dirList)
        {
            foreach (DirectoryInfo dir in dirList)
            {
                Directory.Delete(dir.FullName);
            }
        }

        public void createReport(string outputPath)
        {
            List<DirectoryInfo> dir = new List<DirectoryInfo>
            {
                new DirectoryInfo(outputPath)
            };
            List<Letter> letters = getLetters(dir);
            string reportPath = $"{outputPath}\\report.txt";
            string reportText = "";

            reportText += $"{DateTime.Now.ToString("MM/dd/yyyy")} Report\n\n--------------------------------\n\n";
            reportText += $"Number of Combined Letters: {letters.Count}\n";
            foreach (Letter letter in letters)
            {
                reportText += $"\t{letter.studentId}\n";
            }
            File.WriteAllText(reportPath, reportText);
        }
    }
}
