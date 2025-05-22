namespace FileScanner {
    public class World : IFolder {
        public string FolderPath { get; set; }
        public IList<string> Files { get; set; }
        public IList<Dimension> Dimensions { get; set; }

        public World() {
            FolderPath = string.Empty;
            Files = new List<string>();
            Dimensions = new List<Dimension>();
        }
    }
}
