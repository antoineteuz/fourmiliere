using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace Fourmiliere
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer dt = new DispatcherTimer();
        Stopwatch stopwatch = new Stopwatch();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = App.FourmiliereViewModel;

            //Rafraichissement du plateau
            dt.Tick += new EventHandler(Redessine_Tick);
            dt.Interval = new TimeSpan(0, 0, 0, 0, App.FourmiliereViewModel.VitesseExecution);

            Redessine();
        }

        private void Redessine_Tick(object sender, EventArgs e)
        {
            if (stopwatch.IsRunning)
            {
                Redessine();
            }
        }

        private void Redessine()
        {
            DessinePlateau();
        }

        private void AddUIToGrid(Grid uneGrille, UIElement unUiElement)
        {
            
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            App.FourmiliereViewModel.AjouteFourmi();
            Redessine();
        }

        private void BtnSupp_Click(object sender, RoutedEventArgs e)
        {
            App.FourmiliereViewModel.SupprimerFourmi();
        }

        private void BtnAproposWindow_Click(object sender, RoutedEventArgs e)
        {
            AproposWindow aproposWindow = new AproposWindow();
            aproposWindow.Show();
            this.Close();
        }

        private void DessinePlateau()
        {
            
            Plateau.ColumnDefinitions.Clear();
            Plateau.RowDefinitions.Clear();
            Plateau.Children.Clear();

            for (int i = 0; i < App.FourmiliereViewModel.DimensionX; i ++)
            {
                Plateau.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < App.FourmiliereViewModel.DimensionY; i++)
            {
                Plateau.RowDefinitions.Add(new RowDefinition());
            }

            foreach (var fourmi in App.FourmiliereViewModel.FourmisList)
            {
                Image img = new Image();
                img.Source = new BitmapImage(new Uri("fourmi.png", UriKind.Relative));
                Plateau.Children.Add(img);
                Grid.SetColumn(img, fourmi.X);
                Grid.SetRow(img, fourmi.Y);
            }
        }

        private void DessinePlateau(int x, int y)
        {
            throw new NotImplementedException();
        }

        private void BtnTourSuivant_Click(object sender, RoutedEventArgs e)
        {
            App.FourmiliereViewModel.TourSuivant();
            int x = App.FourmiliereViewModel.DimensionX;
            int y = App.FourmiliereViewModel.DimensionY;
            Redessine();
        }

        private void BtnAvance_Click(object sender, RoutedEventArgs e)
        {
            Thread tt = new Thread(App.FourmiliereViewModel.Avance);
            tt.Start();
            stopwatch.Start();
            dt.Start();
        }

        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            App.FourmiliereViewModel.Stop();
            if (stopwatch.IsRunning)
            {
                stopwatch.Stop();
            }
        }
    }
}
