using System.Windows;
using System.Windows.Controls;
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
            var drives = _vm.Drivers;
            foreach (var drive in drives)
            {
                var item = new TreeViewItem();

                FolderView.Items.Add(item);
            }
        }
    }
}
