# LetterApp

## Description

Console app to process admission and scholarship letters.

## Installation

Clone the repo and/or download and extract the contents of the repo as a .ZIP.

## Usage

Run LetterApp.exe

- The application will process any letters stored in the folder `CombinedLetters/Input/Admission` and `CombinedLetters/Input/Scholarship` as .TXT files.
- Letter .TXT files are stored in subfolders named with the current date in 'yyyyMMdd' format.
- Letter .TXT files are named with either the 'admission-########.txt' or 'scholarship-########.txt' format, where the eight digit number is a student ID.
- All letters in the Input folder will be archived to a subfolder in `CombinedLetters/Archive` named with the current date in 'yyyyMMdd' format.
- Any admission and scholarship letters processed on the same day with matching student IDs will be combined into a single 'combined-########.txt' file.
- Combined letters will be placed in a subfolder in `CombinedLetters/Output` named with the current date in 'yyyyMMdd' format.
- A 'report.txt' file will be generated and placed in the corresponding day's subfolder in `CombinedLetters/Output` listing the count and student IDs of all combined letters processed that day.

## Testing

The `CombinedLetters` folder is pre-populated with several test letters. Simply run LetterApp.exe to process these letters.

The `TestLetters` folder contains a backup of these default test letters that can be copied to the `CombinedLetters/Input` folder as needed.

Any additional test letters can be manually created and placed in the corresponding folders in `CombinedLetters/Input` as outlined above.

After running LetterApp.exe, please view the `CombinedLetters/Output` folder. You should see a subfolder for the processing date. 
Inside this folder will be the combined letter files and the 'report.txt' file.

LetterApp.exe can be run multiple times as needed, and the contents of the current day's output folder will be updated appropriately.
Each new day that LetterApp.exe is run, a new subfolder will be created in `CombinedLetters/Output` corresponding to the new processing date.