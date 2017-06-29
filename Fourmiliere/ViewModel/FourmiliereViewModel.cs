using Fourmiliere.Models;
using Fourmiliere.Observer.Events;
using System;
using System.Collections.ObjectModel;
using System.Threading;

namespace Fourmiliere.ViewModel
{

    public class FourmiliereViewModel : ViewModelBase
    {
        static Random Hazard = new Random();

        private string titreApplication;


        private NewLoopEvent newLoopEvent;

        private ObservableCollection<Fourmi> fourmisList;

        public ObservableCollection<Fourmi> FourmisList
        {
            get { return fourmisList; }
            set
            {
                fourmisList = value;
                OnPropertyChanged("FourmisList");
            }
        }

        public ObservableCollection<Nourriture> nourrituresList;
        private Fourmi fourmiSelect;
        private Fourmi fourmiTmp;
        private bool testAjout = true;

        public QGFourmiliere QG { get; set; }

        public string TitreApplication {
            get { return titreApplication; }
            set
            {
                titreApplication = value;
                OnPropertyChanged("TitreApplication");
            }
        }

        public ObservableCollection<Nourriture> NourrituresList
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
            
            newLoopEvent = new NewLoopEvent();
            NourrituresList = new ObservableCollection<Nourriture>();
            FourmisList = new ObservableCollection<Fourmi>();

            QG = QGFourmiliere.Get();
        }

        public void AjouteFourmi()
        {
            fourmiTmp = new Fourmi("Fourmi");
            newLoopEvent.Attach(fourmiTmp);
            FourmisList.Add(fourmiTmp);
        }

        public void AjouteNourriture(int column, int row)
        {
            Console.WriteLine("Column {0}, Row {1}", column, row); 
            NourrituresList.Add(new Nourriture(column, row)); 
        }

        public void SupprimeFourmi()
        {
            newLoopEvent.Detach(FourmiSelect);
            FourmisList.Remove(FourmiSelect);
        }

        internal void TourSuivant()
        {
            newLoopEvent.Notify();
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
