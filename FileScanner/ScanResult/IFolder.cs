namespace FileScanner {
    public interface IFolder {
        string FolderPath { get; }
        IList<string> Files { get; }
    }
}
