using FileScanner;

namespace TestConsole {
    internal class Program {
        static void Main(string[] args) {
            string path = "G:\\Users\\Test";
            string output = "D:\\OutputWorlds";

            Scanner scanner = new Scanner();
            ScanResult result = scanner.Scan(path);

            Console.WriteLine("Starting to copy");
            
            FileCopier copier = new FileCopier();
            copier.Copy(result, output);

            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}