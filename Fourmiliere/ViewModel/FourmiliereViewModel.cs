using Fourmiliere.Model;
using System;
using System.Collections.ObjectModel;
using System.Threading;

namespace Fourmiliere
{

    public class FourmiliereViewModel : ViewModelBase
    {
        static Random Hazard = new Random();

        private string titreApplication;
        
        public ObservableCollection<Nourriture> nourrituresList;
        private Fourmi fourmiSelect;
        private bool testAjout = true;

        public QuartierGénéral QG { get; set; }

        public string TitreApplication {
            get { return titreApplication; }
            set
            {
                titreApplication = value;
                OnPropertyChanged("TitreApplication");
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

            VitesseExecution = 500;

            NourrituresList = new ObservableCollection<Nourriture>();
            
            QG = new QuartierGénéral();
        }

        public void AjouteFourmi()
        {
            QG.ProduireFourmi("Fourmi N°" + QG.FourmisList.Count);
        }

        public void AjouteNourriture(int x, int y)
        {
            foreach (var fourmi in QG.FourmisList)
            {
                if (fourmi.Position.X == y && fourmi.Position.Y == x)
                {
                    testAjout = false;
                } else
                {
                    testAjout = testAjout && true;
                }
            }

            if (testAjout)
                NourrituresList.Add(new Nourriture(x, y)); 
        }

        public void SupprimeFourmi()
        {
            QG.FourmisList.Remove(FourmiSelect);
        }

        internal void TourSuivant()
        {
            Console.WriteLine("Avance");
            foreach (var uneFourmi in QG.FourmisList)
            {
                uneFourmi.Avance1Tour(App.DimensionX, App.DimensionY);
            }
        }

        public bool EnCours { get; set; }
        public int VitesseExecution { get; set; }

        public void Avance()
        {
            EnCours = true;
            while (EnCours)
            {
                Thread.Sleep(App.FourmiliereViewModel.VitesseExecution);
                TourSuivant();
            }
        }

        public void Stop()
        {
            EnCours = false;
        }
    }
}
