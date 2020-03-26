using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.IO;

using SanityArchiver.Application.Models;
using SanityArchiver.DesktopUI.ViewModels;

namespace SanityArchiver.DesktopUI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        private readonly MainWindowViewModel _vm;

        public MainWindow()
        {
            InitializeComponent();

            _vm = new MainWindowViewModel();
            DataContext = _vm;
        }

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
                item.Expanded += Folder_Expanded;
                FolderView.Items.Add(item);
            }
        }

        private void Folder_Expanded(object sender, RoutedEventArgs e)
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

                        subItem.Expanded += Folder_Expanded;

                        item.Items.Add(subItem);
                    });
            }
        }
    }
}
