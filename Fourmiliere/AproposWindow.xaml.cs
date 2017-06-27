using System.Windows;

namespace Fourmiliere
{
    /// <summary>
    /// Logique d'interaction pour AProposWindow.xaml
    /// </summary>
    public partial class AProposWindow : Window
    {
        public AProposWindow()
        {
            InitializeComponent();
            DataContext = new AProposViewModel();
        }
    }
}
