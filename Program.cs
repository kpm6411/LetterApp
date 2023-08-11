using LetterApp;

LetterService letterService = new LetterService();
string admissionInputPath = "./CombinedLetters/Input/Admission/";
string scholarshipInputPath = "./CombinedLetters/Input/Scholarship/";
string outputPath = $"./CombinedLetters/Output/{DateTime.Now.ToString("yyyyMMdd")}/";
string archivePath = $"./CombinedLetters/Archive/{DateTime.Now.ToString("yyyyMMdd")}/";
DirectoryInfo admissionInputDir = new DirectoryInfo(admissionInputPath);
DirectoryInfo scholarshipInputDir = new DirectoryInfo(scholarshipInputPath);
List<DirectoryInfo> admissionDateDirList = admissionInputDir.GetDirectories().ToList();
List<DirectoryInfo> scholarshipDateDirList = scholarshipInputDir.GetDirectories().ToList();

letterService.CombineLetters(admissionDateDirList, scholarshipDateDirList, outputPath, archivePath);
letterService.DeleteDateDirectories(admissionDateDirList);
letterService.DeleteDateDirectories(scholarshipDateDirList);
Console.ReadLine();