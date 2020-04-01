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

namespace SanityArchiver.DesktopUI.Views
{
    /// <summary>
    /// Interaction logic for ChangeAttributes.xaml
    /// </summary>
    public partial class ChangeAttributes : Window
    {
        #region Getters and Setters
        public string FileName { get; set; }

        public string Extension { get; set; }

        public bool IsHidden { get; set; }
        #endregion

        public ChangeAttributes()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
