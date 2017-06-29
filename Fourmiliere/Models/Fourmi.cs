using Fourmiliere.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Fourmiliere.Models
{
    public class Fourmi : AbstractObserver
    {
        private static Random Hasard = new Random();
        private int nourritureDistanceMax = 3;

        public string Nom { get; set; }

        public int GridIndex { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        public int OldX { get; set; }
        public int OldY { get; set; }

        public Image image;

        public Uri imageUri;

        public ObservableCollection<Etape> EtapesList { get; set; }

        private int oldX;
        private int oldY;

        private Nourriture nourritureCible;

        public Boolean PorteNourriture { get; set; }
        public Nourriture NourriturePortee { get; set; }

        public int ChargeMaximale { get; set; }

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

            PorteNourriture = false;

            ChargeMaximale = 1;
        }

        public override string ToString()
        {
            return "(Brouillon)" + Nom;
        }

        public void Avance1Tour()
        {
            if (PorteNourriture)
            {
                RetourneAuQG();
            }
            else
            {
                ChercheNourriture();
            }

            EtapesList.Add(new Etape() {Lieu = X + " " + Y});
        }

        private void Avance(int decalageX, int decalageY)
        {    
            if (decalageX >= -1 && decalageY <= 1)
            {
                oldX = X;
                oldY = Y;

                int newX = X + decalageX;
                int newY = Y + decalageY;

                if ((newX >= 0) && (newX < App.DimensionX)) X = newX;
                if ((newY >= 0) && (newY < App.DimensionY)) Y = newY;

                ControleCase();
            }
        }

        private void ControleCase()
        {

            // Si la fourmi a de la nourriture en cible
            if (nourritureCible != null)
            {
                // La fourmi ramasse la nourriture si elle est sur la même case qu'elle
                if (X == nourritureCible.X && Y == nourritureCible.Y)
                {
                    PorteNourriture = true;
                    NourriturePortee = nourritureCible;
                    App.FourmiliereViewModel.NourrituresList.Remove(NourriturePortee);
                }
            }

            if (PorteNourriture && X == QGFourmiliere.Get().X && Y == QGFourmiliere.Get().Y)
            {
                DeposerNourriture();
            }
        }

        private void DeposerNourriture()
        {
            QGFourmiliere.Get().AjoutNourriture(NourriturePortee);
            nourritureCible = null;
            NourriturePortee = null;
            PorteNourriture = false;
        }

        private Double CalculDistance(int cibleX, int cibleY)
        {
            return Math.Sqrt(Math.Pow(cibleX - X, 2) + Math.Pow(cibleY - Y, 2));
        }

        private void ChercheNourriture()
        {
            Double distance;
            Double distanceCible = 100;
            foreach (var nourriture in App.FourmiliereViewModel.NourrituresList)
            {
                // Calcul de distance entre 2 points X1Y1 et X2Y2
                distance = CalculDistance(nourriture.X, nourriture.Y);
                Console.WriteLine("Fourmi: {0},{1} ; Nourriture: {2},{3} ; distance = {4}", X, Y, nourriture.X,
                    nourriture.Y, distance);
                if (distance <= nourritureDistanceMax)
                {
                    if (nourritureCible != null || distance < distanceCible)
                    {
                        nourritureCible = nourriture;
                        distanceCible = distance;
                    }
                }
            }

            // Si la fourmi a de la nourriture à proximité, elle se déplace vers elle
            if (nourritureCible != null && distanceCible <= nourritureDistanceMax)
            {
                DeplacerVersCible(nourritureCible.X, nourritureCible.Y);
            }
            // Sinon, elle avance au hasard
            else
            {
                AvanceAuHasard();
            }
        }

        // Retourne à la fourmiliere si porte de la nourriture
        private void RetourneAuQG()
        {
            DeplacerVersCible(QGFourmiliere.Get().X, QGFourmiliere.Get().Y);
        }

        private void DeplacerVersCible(int cibleX, int cibleY)
        {
            if (X > cibleX)
                Avance(-1, 0);
            else if (X < cibleX)
                Avance(1, 0);
            else
            {
                if (Y > cibleY)
                    Avance(0, -1);
                else if (Y < cibleY)
                    Avance(0, 1);
            }
        }

        private void AvanceAuHasard()
        {
            int testX, testY;
            int decalageX, decalageY;

            do
            {
                decalageX = Hasard.Next(3) - 1;
                decalageY = Hasard.Next(3) - 1;

                testX = X + ((decalageX == 2) ? 1 : decalageX);
                testY = Y + ((decalageY == 2) ? 1 : decalageY);
            } while (testX == oldX && testY == oldY);
            Avance(decalageX, decalageY);
        }

        public override void Update()
        {
            Avance1Tour();
        }
    }
}