using Fourmiliere.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Fourmiliere
{
    public class QuartierGénéral: ModelBase
    {

        private int x;
        private int y;

        public int X { get; }
        public int Y { get; }

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

        private int quantitéNourriture = 0;
         
        public int QuantitéNourriture { get; }

        private static Random Hasard = new Random();

        internal QuartierGénéral()
        {
            X = Hasard.Next(App.DimensionX);
            Y = Hasard.Next(App.DimensionY);

            FourmisList = new ObservableCollection<Fourmi>();
        }

        public void ProduireFourmi(String Nom)
        {
            fourmisList.Add(new Fourmi(Nom, X, Y));
        }

        private void AjoutNourriture(int quantité)
        {
            quantitéNourriture++;
        }

    }
}
