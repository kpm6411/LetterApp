using LetterApp;
using System.Diagnostics;

LetterService letterService = new LetterService();
string projectFolder = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
string admissionInputPath = projectFolder + "\\CombinedLetters\\Input\\Admission\\";
string scholarshipInputPath = projectFolder + "\\CombinedLetters\\Input\\Scholarship\\";
string outputPath = projectFolder + $"\\CombinedLetters\\Output\\{DateTime.Now.ToString("yyyyMMdd")}\\";
string archivePath = projectFolder + $"\\CombinedLetters\\Archive\\{DateTime.Now.ToString("yyyyMMdd")}\\";
DirectoryInfo admissionInputDir = new DirectoryInfo(admissionInputPath);
DirectoryInfo scholarshipInputDir = new DirectoryInfo(scholarshipInputPath);
List<DirectoryInfo> admissionDateDirList = admissionInputDir.GetDirectories().ToList();
List<DirectoryInfo> scholarshipDateDirList = scholarshipInputDir.GetDirectories().ToList();

letterService.CombineLetters(admissionDateDirList, scholarshipDateDirList, outputPath, archivePath);
letterService.DeleteDateDirectories(admissionDateDirList);
letterService.DeleteDateDirectories(scholarshipDateDirList);