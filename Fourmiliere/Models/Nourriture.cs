using System;

namespace Fourmiliere.Models
{
    public class Nourriture
    {
        static Random Hazard = new Random();

        public int X { get; set; }

        public int Y { get; set; }

        public int quantite { get; set; }

        public Nourriture (int mX, int mY)
        {
            quantite = Hazard.Next(10);
            X = mX;
            Y = mY;
        }
    }
}
