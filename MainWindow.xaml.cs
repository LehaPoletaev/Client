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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }
      

        private void Upload(object sender, RoutedEventArgs e)
        {
            Upload upload = new Upload();
            this.Close();
            upload.Show();
        }
        private void Download(object sender, RoutedEventArgs e)
        {
            Download download = new Download();
            this.Close();
            download.Show();
        }
        private void Rename(object sender, RoutedEventArgs e)
        {
            Rename rename = new Rename();
            this.Close();
            rename.Show();
        }
        private void Delete(object sender, RoutedEventArgs e)
        {
            Delete upload = new Delete();
            this.Close();
            upload.Show();
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            this.Close();
            login.Show();
        }
    }
}
