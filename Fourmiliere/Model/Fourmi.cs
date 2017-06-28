using Fourmiliere.Observer;
using Fourmiliere.Observer.Events;
using System;
using System.Collections.ObjectModel;

namespace Fourmiliere.Model
{
    public class Fourmi : AbstractObserver
    {
        private static Random Hasard = new Random();

        public string Nom { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public ObservableCollection<Etape> EtapesList { get; set; }

        private int décalageX;
        private int décalageY;
        private int newX;
        private int newY;
        private int previousX;
        private int previousY;

        public Fourmi(string Nom)
        {
            this.Nom = Nom;
            X = QuartierGénéral.get().X;
            Y = QuartierGénéral.get().Y;

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

        public void Avance1Tour()
        {
            AvanceAuHasard();
            EtapesList.Add(new Etape() {Lieu = X + " " + Y});
        }

        private void AvanceAuHasard()
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

            if ((newX >= 0) && (newX < App.DimensionX)) X = newX;
            if ((newY >= 0) && (newY < App.DimensionY)) Y = newY;
        }

        public override void Update()
        {
            Avance1Tour();
        }
    }
}