using System;
using System.Collections.ObjectModel;

namespace Fourmiliere
{
    public class Fourmi
    {
        private static readonly Random Hazard = new Random();

        public string Nom { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public ObservableCollection<Etape> EtapesList { get; set; }

        public Fourmi (string unNom, int maxDimX, int maxDimY)
        {
            Nom = unNom;
            X = maxDimX;
            Y = maxDimY;

            EtapesList = new ObservableCollection<Etape>();
            for (int i = 0, max = Hazard.Next(10); i < max; i++)
            {
                EtapesList.Add(new Etape());
            }
        }

        public override string ToString()
        {
            return "(Brouillon) " + Nom;
        }

        internal void Avance1Tour(int dimensionX, int dimensionY)
        {
            AvanceAuHasard(dimensionX, dimensionY);
            EtapesList.Add(new Etape() { Lieu = X + " " + Y });
        }

        private void AvanceAuHasard(int dimensionX, int dimensionY)
        {
            int newX = Hazard.Next(3) - 1;
            int newY = Hazard.Next(3) - 1;
            if ((newX >= 0) && (newX < dimensionX)) X = newX;
            if ((newY >= 0) && (newY < dimensionY)) Y = newY;

        }
    }
}