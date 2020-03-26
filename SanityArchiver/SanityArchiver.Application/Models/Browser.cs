using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SanityArchiver.Application.Models
{
#pragma warning disable SA1600 // Elements should be documented
    public class Browser
    {
        private static List<FileInfo> _files;
        private static List<DirectoryInfo> _directories;
        private static List<string> _drivers;

        public void GetDrives()
        {
            foreach (var drive in Directory.GetLogicalDrives())
            {
                _drivers.Add(drive);
            }
        }

        public void GetDirectories(string fullPath)
        {
            var dirs = Directory.GetDirectories(fullPath);

            if (dirs.Length > 0)
            {
                _directories.AddRange(dirs);
            }
        }
    }
}
#pragma warning restore SA1600 // Elements should be documented
