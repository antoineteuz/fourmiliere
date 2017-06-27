﻿using Fourmiliere.Model;
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


            NourrituresList = new ObservableCollection<Nourriture>();

            DimensionX = 10;
            DimensionY = 20;
            VitesseExecution = 500;
            
            QG = new QuartierGénéral(DimensionX, DimensionY);
        }

        public void AjouteFourmi()
        {
            QG.ProduireFourmi("Fourmi N°" + QG.FourmisList.Count);
        }

        public void AjouteNourriture(int x, int y)
        {
            foreach (var fourmi in QG.FourmisList)
            {
                if (fourmi.X == y && fourmi.Y == x)
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
            foreach (var uneFourmi in QG.FourmisList)
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
