using System.Windows;

namespace Fourmiliere
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static FourmiliereViewModel FourmiliereViewModel { get; set; }

        public App()
        {
            FourmiliereViewModel = new FourmiliereViewModel();
        }
    }
}
