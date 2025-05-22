namespace FileScanner {
    public class Dimension : IFolder {
        public string FolderPath { get; set; }
        public IList<string> Files { get; set; }

        public Dimension() {
            FolderPath = string.Empty;
            Files = new List<string>();
        }
    }
}
