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

namespace Fourmiliere.Views
{
    /// <summary>
    /// Logique d'interaction pour VueCellulePlateau.xaml
    /// </summary>
    public partial class VueCellulePlateau : UserControl
    {
        public VueCellulePlateau()
        {
            InitializeComponent();
            
        }

        private void Cellule_OnMouseDown(object sender, RoutedEventArgs e)
        {
            int row = Grid.GetRow((Button)sender);
            int column = Grid.GetColumn((Button)sender);

            App.FourmiliereViewModel.AjouteNourriture(row, column);
        }
    }
}
