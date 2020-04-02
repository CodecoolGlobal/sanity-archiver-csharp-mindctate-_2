using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using SanityArchiver.Application.Models;

namespace SanityArchiver.DesktopUI.ViewModels
{
    /// <summary>
    /// Logic about getting and setting information about the files, directories and drives.
    /// </summary>
    public class MainWindowViewModel
    {
        #region Fields
        private static string[] _fileNames;
        private static List<string> _directories = new List<string>();
        private static List<string> _drives = new List<string>();
        private string[] _splittedPath;
        #endregion

        #region Getters and Setters

        public List<FileProp> SelectedFiles { get; set; }

        public CopyType CopyOrMove { get; set; }

        public string FullPath { get; set; }

        public string CurrentPath { get; set; }

        public List<string> Drives
        {
            get => _drives;
            set
            {
                _drives = value;
            }
        }
        #endregion

        #region Constructor
        public MainWindowViewModel()
        {
        }
        #endregion

        #region Drive Methods

        /// <summary>
        /// Get all drives the computer has.
        /// </summary>
        public void GetDrives()
        {
            foreach (var drive in Directory.GetLogicalDrives())
            {
                Drives.Add(drive);
            }
        }
        #endregion

        #region Dictionary Methods

        /// <summary>
        /// Get all subdirectories a drive or directory has.
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns>subdirectories</returns>
        public List<string> GetDirectories(string fullPath)
        {
            try
            {
                try
                {
                    var dirs = Directory.GetDirectories(fullPath);

                    if (dirs.Length > 0)
                    {
                        _directories.AddRange(dirs);
                    }

                    return dirs.ToList();
                }
                catch (UnauthorizedAccessException e)
                {
                }

                return null;
            }
            catch (IOException e)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the path of the selected folder.
        /// </summary>
        /// <param name="path"></param>
        /// <returns>path of the folder</returns>
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
        #endregion

        #region File Methods

        /// <summary>
        /// Gets all files in a directory.
        /// </summary>
        /// <param name="path"></param>
        /// <returns>list of files</returns>
        public string[] GetFileNames(string path)
        {
            string[] files = Directory.GetFiles(path);
            return files;
        }

        /// <summary>
        /// Get the creation time of a file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>creation time of the file</returns>
        public string GetCreationTime(string fileName)
            {
            DateTime creation = File.GetCreationTime(fileName);

            return creation.ToString();
            }

        /// <summary>
        /// Get the size of a file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>size of the file</returns>
        public string GetFileSize(string fileName)
        {
            FileInfo fi = new FileInfo(fileName);

            return ConvertSize(fi.Length);
        }

        /// <summary>
        /// Remove the path from the file's name and give back only the last part of it.
        /// </summary>
        /// <param name="path"></param>
        /// <returns>the file's name</returns>
        public string ConvertPathToName(string path)
        {
            _splittedPath = path.Split('\\');
            return _splittedPath[_splittedPath.Length - 1];
        }

        public void PasteSelectedFiles(List<FileProp> selectedFiles, string destinationPath)
        {
            try
            {
                if (CopyOrMove == CopyType.Copy)
                {
                    foreach (var file in selectedFiles)
                    {
                        File.Copy(file.FilePath, destinationPath + "\\" + file.Name, true);
                    }
                }
                else if (CopyOrMove == CopyType.Move)
                {
                    foreach (var file in selectedFiles)
                    {
                        File.Move(file.FilePath, destinationPath + "\\" + file.Name);
                    }
                }
            }
            catch (IOException iox)
            {
                Console.WriteLine(iox.Message);
            }

            SelectedFiles.Clear();
        }

        public void ZipMultiFiles(string dir, List<FileProp> files)
        {
        }

        public string GetDirectorySize(string path)
        {
            try
            {
                string[] filesInFolder = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
                long size = 0;
                foreach (string file in filesInFolder)
                {
                    FileInfo info = new FileInfo(file);
                    size += info.Length;
                }

                return ConvertSize(size);
            }
            catch (UnauthorizedAccessException uae)
            {
                Console.WriteLine(uae.Message);
                return null;
            }
        }

        public string ConvertSize(long size)
        {
            if (size > 0 && size < 1024)
            {
                return (size + " B").ToString();
            }
            else if (size >= 1024 && size < (1024 * 1024))
            {
                return ((size / 1024) + " KB").ToString();
            }
            else if (size >= (1024 * 1024) && size < (1024 * 1024 * 1024))
            {
                return ((size / (1024 * 1024)) + " MB").ToString();
            }
            else if (size >= (1024 * 1024 * 1024))
            {
                return ((size / (1024 * 1024 * 1024)) + " GB").ToString();
            }

            return size.ToString();
        }
        #endregion
    }
}
