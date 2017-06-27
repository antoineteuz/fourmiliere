using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fourmiliere.Model
{

    public class Fourmi
    {

        static Random Hasard = new Random();

        public string Nom { get; set; }

        public int X { get; set;  }

        public int Y { get; set;  }

        public ObservableCollection<Etape> EtapesList { get; set; }

        private int décalageX;
        private int décalageY;
        private int newX;
        private int newY;
        private int previousX;
        private int previousY;

        public Fourmi(string Nom, int maxDimX, int maxDimY)
        {
            this.Nom = Nom;
            X = maxDimX;
            Y = maxDimY;

            EtapesList = new ObservableCollection<Etape>();

            for (int i = 0, max = Hasard.Next(10); i < max; i++)
            {
                EtapesList.Add(new Etape());
            }
        }

        public override string ToString()
        {
            return "(Brouillon)" + Nom;
        }

        internal void Avance1Tour(int dimensionX, int dimensionY)
        {
            AvanceAuHasard(dimensionX, dimensionY);
            EtapesList.Add(new Etape() { Lieu = X + " " + Y });
        }

        private void AvanceAuHasard(int dimensionX, int dimensionY)
        {

            do
            {
                décalageX = Hasard.Next(3) - 1;
                décalageY = Hasard.Next(3) - 1;

                newX = X + ((décalageX == 2) ? 1 : décalageX);
                newY = Y + ((décalageY == 2) ? 1 : décalageY);
            } while (newX == previousX && newY == previousY);

            previousX = X;
            previousY = Y;

            if ((newX >= 0) && (newX < dimensionX)) X = newX;
            if ((newY >= 0) && (newY < dimensionY)) Y = newY;

        }
    }
}
