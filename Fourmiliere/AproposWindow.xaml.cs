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

namespace Fourmiliere
{
    /// <summary>
    /// Logique d'interaction pour AproposWindow.xaml
    /// </summary>
    public partial class AproposWindow : Window
    {
        public AproposWindow()
        {
            InitializeComponent();
            DataContext = new AproposViewModel();
        }
    }
}
