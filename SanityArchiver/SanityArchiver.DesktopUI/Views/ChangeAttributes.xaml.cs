using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using SanityArchiver.DesktopUI.ViewModels;

namespace SanityArchiver.DesktopUI.Views
{
    /// <summary>
    /// Interaction logic for ChangeAttributes.xaml
    /// </summary>
    public partial class ChangeAttributes : Window
    {
        private MainWindowViewModel _vm;

        #region Getters and Setters
        public string FilePath { get; set; }

        public string CurrentDirectory { get; set; }

        public string FileName { get; set; }

        public string Extension { get; set; }

        public bool IsHidden { get; set; }
        #endregion

        public ChangeAttributes()
        {
            InitializeComponent();
            _vm = new MainWindowViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FileName = txtName.Text;
            Extension = txtExtension.Text;
            IsHidden = (bool)chkIsHidden.IsChecked;
            FilePath = CurrentDirectory + "\\" + FileName + "." + Extension;

            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _vm.ChangeAttributes(FilePath, FileName, Extension, IsHidden);
        }
    }
}
