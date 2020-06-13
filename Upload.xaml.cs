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

namespace DesktopClient
{
    /// <summary>
    /// Логика взаимодействия для Upload.xaml
    /// </summary>
    public partial class Upload : Window
    {
        public Upload()
        {
            InitializeComponent();
        }

       

        private void Window_closed(object sender, EventArgs e)
        { 
            MainWindow main = new MainWindow();
            this.Hide();
            main.Show();

        }
    }
}
