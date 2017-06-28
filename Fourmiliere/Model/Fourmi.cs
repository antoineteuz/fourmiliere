using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fourmiliere.Model
{

    public class Fourmi
    {

        static Random Hasard = new Random();

        public string Nom { get; set; }

        public Position Position { get; set; }

        public ObservableCollection<Etape> EtapesList { get; set; }

        private int décalageX;
        private int décalageY;
        private int newX;
        private int newY;

        public Fourmi(string Nom, int X, int Y)
        {
            this.Nom = Nom;

            Position = new Position(X, Y);

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
            AvanceAuHasard();
            EtapesList.Add(new Etape() { Lieu = Position.X + " " + Position.Y });
        }

        private void AvanceAuHasard()
        {
            do
            {
                décalageX = Hasard.Next(3) - 1;
                décalageY = Hasard.Next(3) - 1;

                newX = Position.X + ((décalageX == 2) ? 1 : décalageX);
                newY = Position.Y + ((décalageY == 2) ? 1 : décalageY);
            } while (newX == Position.OldX && newY == Position.OldY);

            if (Position.Update(newX, newY))
            {
                Console.WriteLine(String.Format("Moving to position {0}, {1} !", Position.X.ToString(), Position.Y.ToString()));
            }
            else
            {
                Console.WriteLine("Same position, not moving...");
            }

        }
    }
}
