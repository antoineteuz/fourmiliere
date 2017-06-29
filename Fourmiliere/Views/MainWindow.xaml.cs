
using Fourmiliere.Models;
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

namespace Fourmiliere.Views
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer dt = new DispatcherTimer();
        Stopwatch stopwatch = new Stopwatch();

        private QGFourmiliere QG { get; }

        public MainWindow()
        {
            InitializeComponent();

            DataContext = App.FourmiliereViewModel;

            QG = QGFourmiliere.Get();

            //Rafraichissement du plateau
            dt.Tick += new EventHandler(Redessine_Tick);
            dt.Interval = new TimeSpan(0, 0, 0, 0, App.FourmiliereViewModel.VitesseExecution);

            DessinePlateau();
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
            ViderPlateau();

            InitPlateau();

            DessinePheromone(0, 0);

            DessineCellules();

            DessineFourmiliere();

            DessineFourmis();

            DessineNourriture();
        }

        private void ViderPlateau()
        {
            Plateau.ColumnDefinitions.Clear();
            Plateau.RowDefinitions.Clear();
            Plateau.Children.Clear();
        }

        private void InitPlateau()
        {
            for (int i = 0; i < App.DimensionX; i++)
            {
                Plateau.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < App.DimensionY; i++)
            {
                Plateau.RowDefinitions.Add(new RowDefinition());
            }
        }

        private void DessinePheromone(int x, int y)
        {
            Ellipse pheromone = new Ellipse();
            pheromone.Fill = new SolidColorBrush(Colors.BlueViolet);

            Grid.SetColumn(pheromone, 4);
            Grid.SetRow(pheromone, 4);
            Plateau.Children.Add(pheromone);
        }

        private void DessineCellules()
        {
            for (int i = 0; i < App.DimensionX; i++)
            {
                for (int j = 0; j < App.DimensionY; j++)
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
        }

        private void DessineFourmiliere()
        {
            Grid.SetColumn(QG.image, QG.X);
            Grid.SetRow(QG.image, QG.Y);
            Plateau.Children.Add(QG.image);
        }

        public void DessineFourmis()
        {
            // On dessine les fourmis
            foreach (var fourmi in App.FourmiliereViewModel.FourmisList)
            {
                Grid.SetColumn(fourmi.image, fourmi.X);
                Grid.SetRow(fourmi.image, fourmi.Y);

                fourmi.GridIndex = Plateau.Children.Add(fourmi.image);
                
            }
        }

        public void RedessineFourmi(Fourmi fourmi)
        {
            Plateau.Children.RemoveAt(fourmi.GridIndex);

            Grid.SetColumn(fourmi.image, fourmi.X);
            Grid.SetRow(fourmi.image, fourmi.Y);

            fourmi.GridIndex = Plateau.Children.Add(fourmi.image);
        }

        private void DessineNourriture()
        {
            foreach (var nourriture in App.FourmiliereViewModel.NourrituresList)
            {
                Uri uri = new Uri("../Assets/images/olive.png", UriKind.Relative);
                Image img = new Image();
                img.Source = new BitmapImage(uri);

                Grid.SetColumn(img, nourriture.X);
                Grid.SetRow(img, nourriture.Y);
                Plateau.Children.Add(img);
            }
        }

        private void BtnOnClick(object sender, RoutedEventArgs e)
        {
            int column = Grid.GetColumn((Button)sender);
            int row = Grid.GetRow((Button) sender);

            App.FourmiliereViewModel.AjouteNourriture(column, row);
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
            if (e.Key == Key.Delete)
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