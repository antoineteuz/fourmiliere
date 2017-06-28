using System.Windows;

namespace Fourmiliere
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static FourmiliereViewModel FourmiliereViewModel { get; set; }
        public static int DimensionX = 10;
        public static int DimensionY = 20;

        public App()
        {
            FourmiliereViewModel = new FourmiliereViewModel();
        }
    }
}
