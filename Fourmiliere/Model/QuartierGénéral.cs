using System;

namespace Fourmiliere
{
    public class QuartierGénéral: ModelBase
    {

        private static QuartierGénéral instance;
        private int x;
        private int y;

        public int X { get; }
        public int Y { get; }

        private int quantitéNourriture = 0;
         
        public int QuantitéNourriture { get; }

        private static Random Hasard = new Random();

        protected QuartierGénéral()
        {
            X = Hasard.Next(App.DimensionX);
            Y = Hasard.Next(App.DimensionY);
        }

        public static QuartierGénéral get()
        {
            if (instance == null)
            {
                instance = new QuartierGénéral();
            }
            return instance;
        }

        private void AjoutNourriture(int quantité)
        {
            quantitéNourriture++;
        }

    }
}
