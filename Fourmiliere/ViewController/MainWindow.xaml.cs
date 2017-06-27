using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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

        // TODO: Optimiser le redessinage
        private void Redessine()
        {
            DessinePlateau();
        }

        private void DessinePlateau()
        {
            Plateau.ColumnDefinitions.Clear();
            Plateau.RowDefinitions.Clear();
            Plateau.Children.Clear();

            for (int i = 0; i < App.FourmiliereViewModel.DimensionX; i++)
            {
                Plateau.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < App.FourmiliereViewModel.DimensionY; i++)
            {
                Plateau.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < 7; i++)
            {
                Ellipse pheromone = new Ellipse();
                pheromone.Fill = new SolidColorBrush(Colors.BlueViolet);

                Grid.SetColumn(pheromone, 4);
                Grid.SetRow(pheromone, 4);
                Plateau.Children.Add(pheromone);
            }

            for(int i = 0; i < App.FourmiliereViewModel.DimensionX; i++)
            {
                for (int j = 0; j < App.FourmiliereViewModel.DimensionY; j++)
                {
                    Button btn = new Button();
                    btn.Background = new SolidColorBrush(Colors.Aqua);
                    Plateau.Children.Add(btn);
                    Grid.SetColumn(btn, i);
                    Grid.SetRow(btn, j);
                    btn.Click += BtnOnClick;
                    btn.Tag = i + " " + j;
                    btn.Margin = new Thickness(4);
                }
            }

            // On dessine la fourmilière
            Uri uri = new Uri("../Assets/images/qg.png", UriKind.Relative);
            Image img = new Image();
            img.Source = new BitmapImage(uri);

            Grid.SetColumn(img, App.FourmiliereViewModel.QG.X);
            Grid.SetRow(img, App.FourmiliereViewModel.QG.Y);
            Plateau.Children.Add(img);

            // On dessine les fourmis
            foreach (var fourmi in App.FourmiliereViewModel.QG.FourmisList)
            {
                uri = new Uri("../Assets/images/fourmi.png", UriKind.Relative);
                img = new Image();
                img.Source = new BitmapImage(uri);

                Grid.SetColumn(img, fourmi.X);
                Grid.SetRow(img, fourmi.Y);
                Plateau.Children.Add(img);
            }

            foreach (var nourriture in App.FourmiliereViewModel.NourrituresList)
            {
                uri = new Uri("../Assets/images/olive.png", UriKind.Relative);
                img = new Image();
                img.Source = new BitmapImage(uri);

                Grid.SetColumn(img, nourriture.X);
                Grid.SetRow(img, nourriture.Y);
                Plateau.Children.Add(img);
            }
            

        }

        private void BtnOnClick(object sender, RoutedEventArgs e)
        {
            int row = Grid.GetRow((Button) sender);
            int column = Grid.GetColumn((Button) sender);

            App.FourmiliereViewModel.AjouteNourriture(row, column);
            Redessine();
        }

        private void AjouteFourmi_Click(object sender, RoutedEventArgs e)
        {
            App.FourmiliereViewModel.AjouteFourmi();
            Redessine();
        }

        private void SupprimerFourmi_Click(object sender, RoutedEventArgs e)
        {
            App.FourmiliereViewModel.SupprimeFourmi();
        }

        private void ListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Delete)
            {
                App.FourmiliereViewModel.SupprimeFourmi();
            }
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            Window APropos = new AProposWindow();
            APropos.ShowDialog();
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
