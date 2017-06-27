using System;
using System.Collections.Generic;

namespace Fourmiliere
{
    public class QuartierGénéral
    {

        private int x;
        private int y;

        public int X { get; }
        public int Y { get; }

        private int nombreDeFourmisMaximum = 0;


        private int quantitéNourriture;
        public int QuantitéNourriture { get; }

        private static Random Hasard = new Random();

        internal QuartierGénéral(int nombreDeFourmisMaximum)
        {
            X = Hasard.Next(App.FourmiliereViewModel.DimensionX);
            Y = Hasard.Next(App.FourmiliereViewModel.DimensionY);

            quantitéNourriture = 0;
        }

        public void AjoutNourriture(int quantité)
        {
            quantitéNourriture++;
        }


    }
}
