using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Please enter the directory path to search for SQL files:");
        string searchDirectory = Console.ReadLine();

        // Validate the input directory path
        if (string.IsNullOrWhiteSpace(searchDirectory) || !Directory.Exists(searchDirectory))
        {
            Console.WriteLine("Invalid directory path. Exiting application.");
            return;
        }

        var csvFilePath = Path.Combine(searchDirectory, "SqlFilesList.csv");

        using (var writer = new StreamWriter(csvFilePath))
        {
            // Write the header
            writer.WriteLine("FileName,Path,Size");

            // Search for SQL files and write details to the CSV
            var sqlFiles = Directory.GetFiles(searchDirectory, "*.sql", SearchOption.AllDirectories);
            foreach (var filePath in sqlFiles)
            {
                var fileInfo = new FileInfo(filePath);
                var line = $"\"{fileInfo.Name}\",\"{fileInfo.FullName}\",{fileInfo.Length}";
                writer.WriteLine(line);
            }
        }

        Console.WriteLine($"CSV file has been created: {csvFilePath}");
    }
}
