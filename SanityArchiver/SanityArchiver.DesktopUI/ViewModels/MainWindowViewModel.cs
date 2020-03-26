using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SanityArchiver.Application.Models;

namespace SanityArchiver.DesktopUI.ViewModels
{
#pragma warning disable SA1600 // Elements should be documented
    public class MainWindowViewModel
    {
        private static List<FileInfo> _files;
        private static List<string> _directories;
        private static List<string> _drivers;

        public List<string> Drivers
        {
            get => _drivers;
            set
            {
                _drivers = value;
            }
        }

#pragma warning disable SA1201 // Elements should appear in the correct order
        public MainWindowViewModel()
#pragma warning restore SA1201 // Elements should appear in the correct order
        {
        }

        public void GetDrives()
        {
            foreach (var drive in Directory.GetLogicalDrives())
            {
                Drivers.Add(drive);
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

        public string GetFileFolderName(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }

            var normalizedPath = path.Replace('/', '\\');

            var lastIndex = normalizedPath.LastIndexOf('\\');

            if (lastIndex <= 0)
            {
                return path;
            }

            return path.Substring(lastIndex + 1);
        }
    }
#pragma warning restore SA1600 // Elements should be documented
}
