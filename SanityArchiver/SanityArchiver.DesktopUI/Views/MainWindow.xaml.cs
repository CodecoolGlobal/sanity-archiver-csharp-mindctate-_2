using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using SanityArchiver.DesktopUI.ViewModels;
using SanityArchiver.Application.Models;

namespace SanityArchiver.DesktopUI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields
        private readonly MainWindowViewModel _vm;
        #endregion

        #region MainWindow

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            _vm = new MainWindowViewModel();
            DataContext = _vm;
        }
        #endregion

        #region Event Handlers

        /// <summary>
        /// When the program starts, shows the computer's drives.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _vm.GetDrives();
            var drives = _vm.Drives;
            foreach (var drive in drives)
            {
                var item = new TreeViewItem()
                {
                    Header = drive,
                    Tag = drive,
                };

                item.Items.Add(null);
                item.Expanded += FolderExpanded;
                FolderView.Items.Add(item);
            }
        }

        /// <summary>
        /// When a drive or folder is expanded, shows it's subfolders.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FolderExpanded(object sender, RoutedEventArgs e)
        {
            var item = (TreeViewItem)sender;
            if (item.Items.Count != 1 || item.Items[0] != null)
            {
                return;
            }

            _vm.FullPath = (string)item.Tag;
            var directories = new List<string>();
            directories = _vm.GetDirectories(_vm.FullPath);
            if (directories != null)
            {
                directories.ForEach(directoryPath =>
                    {
                        var subItem = new TreeViewItem()
                        {
                            Header = _vm.GetFileFolderName(directoryPath),
                            Tag = directoryPath,
                        };

                        subItem.Items.Add(null);

                        subItem.Expanded += FolderExpanded;

                        item.Items.Add(subItem);
                    });
            }
        }

        /// <summary>
        /// Returns the selected TreeViewItem.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetItemTag(object sender, RoutedEventArgs e)
        {
            TreeViewItem selectedItem = (TreeViewItem)FolderView.SelectedItem;

            _vm.CurrentPath = selectedItem.Tag.ToString();
            lblFolderSize.Content = $"The folder size is: {_vm.GetDirectorySize(_vm.CurrentPath)}";
        }

        /// <summary>
        /// Returns the selected folder and fill the itemSource with it's files.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnItemMouseDoubleClick(object sender, MouseButtonEventArgs args)
        {
            if (sender is TreeViewItem)
            {
                if (!((TreeViewItem)sender).IsSelected)
                {
                    return;
                }
            }

            string[] files = _vm.GetFileNames(_vm.CurrentPath);
            List<FileProp> filesInFolder = new List<FileProp>();
            foreach (string file in files)
            {
                filesInFolder.Add(new FileProp()
                {
                    Name = _vm.ConvertPathToName(file),
                    Created = _vm.GetCreationTime(file),
                    Size = _vm.GetFileSize(file),
                });
            }

            FileList.ItemsSource = filesInFolder;
        }

        #endregion
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _vm.SelectedFiles = FileList.SelectedItems
                .Cast<FileProp>()
                .ToList();

            EnableCopyMovePaste(_vm.SelectedFiles);
        }

        private void EnableCopyMovePaste(List<FileProp> selectedFiles)
        {
            if (selectedFiles.Count > 0)
            {
                btnMove.IsEnabled = true;
                btnCopy.IsEnabled = true;
                btnCompress.IsEnabled = true;
            }
            else
            {
                btnMove.IsEnabled = false;
                btnCopy.IsEnabled = false;
                btnPaste.IsEnabled = false;
                btnCompress.IsEnabled = false;
            }
        }

        private void ClickCopy(object sender, RoutedEventArgs e)
        {
            _vm.CopyOrMove = CopyType.Copy;
            btnPaste.IsEnabled = true;
        }

        private void ClickMove(object sender, RoutedEventArgs e)
        {
            _vm.CopyOrMove = CopyType.Move;
            btnPaste.IsEnabled = true;
        }

        private void ClickPaste(object sender, RoutedEventArgs e)
        {
            _vm.PasteSelectedFiles(_vm.SelectedFiles, _vm.CurrentPath);
            OnItemMouseDoubleClick(null, null);
            btnPaste.IsEnabled = false;
        }
    }
}
