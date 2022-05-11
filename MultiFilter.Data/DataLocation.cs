using System;
using System.IO;

namespace MultiFilter.Data
{
    public class DataLocation
    {
        public string Directory { get; set; }

        public string Filename { get; set; }

        public DataLocation(string directory, string filename)
        {
            Directory = directory; 
            Filename = filename;
        }

        public bool NotValid()
        {
            if (Directory == null) return true;
            if (Filename == null) return true;

            if (!System.IO.Directory.Exists(Directory)) return true;

            return false;
        }

        internal string GetFilePath()
        {
            if (Directory == null) return String.Empty;
            if (Filename == null) return String.Empty;

            return Path.Combine(Directory, Filename);
        }
    }
}
