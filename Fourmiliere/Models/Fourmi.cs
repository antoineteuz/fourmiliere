using Fourmiliere.Observer;
using Fourmiliere.Observer.Events;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Fourmiliere.Models
{
    public class Fourmi : AbstractObserver
    {
        private static Random Hasard = new Random();

        public string Nom { get; set; }

        public int GridIndex { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        public int OldX { get; set; }
        public int OldY { get; set; }

        public Image image;

        public Uri imageUri;

        public ObservableCollection<Etape> EtapesList { get; set; }

        private int décalageX;
        private int décalageY;
        private int newX;
        private int newY;
        private int oldX;
        private int oldY;

        public Fourmi(string Nom)
        {
            this.Nom = Nom;

            image = new Image();
            imageUri = new Uri("../Assets/images/fourmi.png", UriKind.Relative);
            image.Source = new BitmapImage(imageUri);

            X = QGFourmiliere.Get().X;
            Y = QGFourmiliere.Get().Y;

            EtapesList = new ObservableCollection<Etape>();

            EtapesList.Add(new Etape());
            
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
            } while (newX == oldX && newY == oldY);

            oldX = X;
            oldY = Y;

            if ((newX >= 0) && (newX < App.DimensionX)) X = newX;
            if ((newY >= 0) && (newY < App.DimensionY)) Y = newY;
        }

        public override void Update()
        {
            Avance1Tour();
        }
    }
}