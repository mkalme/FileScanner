namespace FileScanner {
    public class FileCopier {
        public void Copy(ScanResult result, string outputFolder) {
            string worldFolder = $"{outputFolder}\\Worlds";
            CopyWorlds(result.Worlds, worldFolder);

            Console.WriteLine("Copying rogue dimensions");

            string rogueDimensionFolder = $"{outputFolder}\\Rogue Dimensions";
            CopyDimensions(result.RogueDimensions, rogueDimensionFolder, true);
        }

        private static void CopyWorlds(IList<World> worlds, string outputFolder) {
            Directory.CreateDirectory(outputFolder);

            int i = 1;
            foreach (World world in worlds) {
                string worldFolder = CreateUniquePath(Path.GetFileName(world.FolderPath), outputFolder);
                Directory.CreateDirectory(worldFolder);

                foreach (string levelFile in world.Files) {
                    string levelPath = CreateUniquePath(Path.GetFileName(levelFile), worldFolder);
                    File.Copy(levelFile, levelPath);
                }

                CopyDimensions(world.Dimensions, worldFolder);

                Console.WriteLine($"Copying world ({i++}/{worlds.Count})");
            }
        }
        private static void CopyDimensions(IList<Dimension> dimensions, string outputFolder, bool outputProgress = false) {
            Directory.CreateDirectory(outputFolder);

            int i = 1;
            foreach (Dimension dimension in dimensions) {
                string dimensionFolder = CreateUniquePath(Path.GetFileName(dimension.FolderPath), outputFolder);
                Directory.CreateDirectory(dimensionFolder);

                foreach (string region in dimension.Files) {
                    string regionPath = CreateUniquePath(Path.GetFileName(region), dimensionFolder);
                    File.Copy(region, regionPath);
                }

                if(outputProgress) Console.WriteLine($"Copying dimension ({i++}/{dimensions.Count})");
            }
        }

        private static string CreateUniquePath(string name, string folder) {
            HashSet<string> children = new HashSet<string>();
            children.UnionWith(Directory.GetDirectories(folder).Select(x => Path.GetFileName(x)));
            children.UnionWith(Directory.GetFiles(folder).Select(x => Path.GetFileName(x)));

            string output = name;
            int counter = 2;
            while (children.Contains(output)) {
                output = $"{Path.GetFileNameWithoutExtension(name)} [{counter++}]{Path.GetExtension(name)}";
            }

            return $"{folder}\\{output}";
        }
    }
}
