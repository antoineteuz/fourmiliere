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

       

        private int quantitéNourriture = 0;
         
        public int QuantitéNourriture { get; }

        private static Random Hasard = new Random();

        internal QuartierGénéral(int maxDimensionX, int maxDimensionY)
        {
            X = Hasard.Next(maxDimensionX);
            Y = Hasard.Next(maxDimensionY);


        }

        private void AjoutNourriture(int quantité)
        {
            quantitéNourriture++;
        }

    }
}
