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
            get { return fourmisList;  }
            set
            {
                fourmisList = value;
                OnPropertyChanged("FourmisList");
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

            FourmisList.Add(new Fourmi("FOURMI TEUZO", 1, 1));
            FourmisList.Add(new Fourmi("FOURMI ELIOTTI", 2, 1));
            FourmisList.Add(new Fourmi("FOURMI DEHON", 3, 1));
            FourmisList.Add(new Fourmi("FOURMI BOBINO", 4, 1));

            DimensionX = 10;
            DimensionY = 20;
            VitesseExecution = 500;
        }

        public void AjouteFourmi()
        {
            FourmisList.Add(new Fourmi("Fourmi N°" + FourmisList.Count, Hazard.Next(10), Hazard.Next(10)));
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
