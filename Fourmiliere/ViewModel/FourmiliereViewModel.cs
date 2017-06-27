using Fourmiliere.Model;
using System;
using System.Collections.ObjectModel;
using System.Threading;

namespace Fourmiliere
{

    public class FourmiliereViewModel : ViewModelBase
    {
        static Random Hazard = new Random();

        public int DimensionX { get; set; }
        public int DimensionY { get; set; }
        private string titreApplication;
        public ObservableCollection<Fourmi> fourmisList;
        public ObservableCollection<Nourriture> nourrituresList;
        private Fourmi fourmiSelect;

        public string TitreApplication {
            get { return titreApplication; }
            set
            {
                titreApplication = value;
                OnPropertyChanged("TitreApplication");
            }
        }

        public ObservableCollection<Fourmi> FourmisList
        {
            get { return fourmisList; }
            set
            {
                fourmisList = value;
                OnPropertyChanged("FourmisList");
            }
        }

        public ObservableCollection<Model.Nourriture> NourrituresList
        {
            get { return nourrituresList; }
            set
            {
                nourrituresList = value;
                OnPropertyChanged("NourrituresList");
            }
        }

        public Fourmi FourmiSelect {
            get { return fourmiSelect; }
            set
            {
                fourmiSelect = value;
                OnPropertyChanged("FourmiSelect");
            }
        }

        public FourmiliereViewModel ()
        {
            TitreApplication = "Fourmilière";

            FourmisList = new ObservableCollection<Fourmi>();
            NourrituresList = new ObservableCollection<Nourriture>();

            DimensionX = 10;
            DimensionY = 20;
            VitesseExecution = 500;
        }

        public void AjouteFourmi()
        {
            FourmisList.Add(new Fourmi("Fourmi N°" + FourmisList.Count, Hazard.Next(10), Hazard.Next(10)));
        }

        public void AjouteNourriture(int x, int y)
        {
            NourrituresList.Add(new Nourriture(x, y));
        }

        public void SupprimeFourmi()
        {
            FourmisList.Remove(FourmiSelect);
        }

        internal void TourSuivant()
        {
            foreach (var uneFourmi in FourmisList)
            {
                uneFourmi.Avance1Tour(DimensionX, DimensionY);
            }
        }

        public bool EnCours { get; set; }
        public int VitesseExecution { get; set; }

        public void Avance()
        {
            EnCours = true;
            while (EnCours)
            {
                Thread.Sleep(VitesseExecution);
                TourSuivant();
            }
        }

        public void Stop()
        {
            EnCours = false;
        }
    }
}
