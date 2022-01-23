using System.Collections.Generic;

namespace FlameSharp
{
    public class Config
    {
        /*public string PackageName { get; set; }
        public string PackageType { get; set; }
        public Dictionary<string, string> Packages { get; set; }
        public List<string> Directories { get; set; }*/
        public string Directory { get; set; }

        public Config(string directory)
        {
            Directory = directory;
        }
    }
}
