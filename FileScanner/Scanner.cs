namespace FileScanner {
    public class Scanner {
        public ScanResult Scan(string path) {
            List<string> levelFiles = new List<string>();
            GetFiles(levelFiles, path, "level.dat");
            Console.WriteLine("level.dat files collected");
            
            GetFiles(levelFiles, path, "level.dat_old");
            Console.WriteLine("level.dat_old files collected");

            GetFiles(levelFiles, path, "level.dat_mcr");
            Console.WriteLine("level.dat_mcr files collected");

            List<string> regionFiles = new List<string>();
            GetFiles(regionFiles, path, "*.mca");
            Console.WriteLine(".mca files collected");

            IDictionary<string, IFolder> worlds = SortFilesIntoFolders(levelFiles, parentFolder => new World() { FolderPath = parentFolder });
            IDictionary<string, IFolder> dimensions = SortFilesIntoFolders(regionFiles, parentFolder => new Dimension() { FolderPath = parentFolder });

            return SortDimensionsIntoWorlds(worlds, dimensions);
        }

        private static void GetFiles(IList<string> output, string path, string filter) {
            string[] files = Directory.GetFiles(path, filter);
            foreach (string file in files) {
                output.Add(file);
            }

            string[] folders = Directory.GetDirectories(path);
            foreach (string folder in folders) {
                try {
                    GetFiles(output, folder, filter);
                } catch { }
            }
        }

        private static IDictionary<string, IFolder> SortFilesIntoFolders(IList<string> files, Func<string, IFolder> folderCreator) {
            Dictionary<string, IFolder> folders = new Dictionary<string, IFolder>(files.Count);
            
            foreach (string file in files) {
                string parentFolder = Path.GetDirectoryName(file);

                if (!folders.TryGetValue(parentFolder, out IFolder folder)) {
                    folder = folderCreator(parentFolder);
                    folders.Add(parentFolder, folder);
                }

                folder.Files.Add(file);
            }

            return folders;
        }
        private static ScanResult SortDimensionsIntoWorlds(IDictionary<string, IFolder> worlds, IDictionary<string, IFolder> dimensions) {
            ScanResult output = new ScanResult();

            foreach (var pair in dimensions) {
                Dimension dimension = (Dimension)pair.Value;

                string folder = Path.GetDirectoryName(dimension.FolderPath);
                string parent = Path.GetDirectoryName(folder);

                if (!worlds.TryGetValue(folder, out IFolder world) && (parent == null ? true : !worlds.TryGetValue(parent, out world))) {
                    output.RogueDimensions.Add(dimension);
                } else {
                    ((World)world).Dimensions.Add(dimension);
                }
            }

            foreach (var pair in worlds) {
                output.Worlds.Add((World)pair.Value);
            }

            return output;
        }
    }
}