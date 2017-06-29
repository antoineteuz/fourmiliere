using System;

namespace Fourmiliere.Models
{
    public class Nourriture
    {
        static Random Hazard = new Random();

        public int X { get; set; }

        public int Y { get; set; }

        public int quantite { get; set; }

        public Nourriture (int x, int y)
        {
            quantite = Hazard.Next(10);
            X = x;
            Y = y;
        }
    }
}
