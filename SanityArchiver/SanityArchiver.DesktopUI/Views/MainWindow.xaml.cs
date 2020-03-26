using System.Windows;
using SanityArchiver.Application.Models;

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
        public MainWindow()
        {
            InitializeComponent();

           // _vm = new MainWindowViewModel();
           // DataContext = _vm;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           // _vm.GetDrives();
        }
    }
}
