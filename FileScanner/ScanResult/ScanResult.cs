namespace FileScanner {
    public class ScanResult {
        public IList<World> Worlds { get; set; }
        public IList<Dimension> RogueDimensions { get; set; }

        public ScanResult() {
            Worlds = new List<World>();
            RogueDimensions = new List<Dimension>();
        }
    }
}
