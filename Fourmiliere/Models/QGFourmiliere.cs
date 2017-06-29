using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Fourmiliere.Models
{
    public class QGFourmiliere: ModelBase
    {

        private static QGFourmiliere instance;

        public Uri imageUri;

        public Image image;

        private int x;
        private int y;

        public int X { get; }
        public int Y { get; }

        private int quantitéNourriture = 0;

        public int QuantitéNourriture {
            get { return quantitéNourriture;  }
        }

        private static Random Hasard = new Random();

        protected QGFourmiliere()
        {

            imageUri = new Uri("../Assets/images/qg.png", UriKind.Relative);
            image = new Image();
            image.Source = new BitmapImage(imageUri);

            X = Hasard.Next(App.DimensionX);
            Y = Hasard.Next(App.DimensionY);
        }

        public static QGFourmiliere Get()
        {
            if (instance == null)
            {
                instance = new QGFourmiliere();
            }
            return instance;
        }

        public void AjoutNourriture(Nourriture nourriture)
        {
            nourriture = null;
            quantitéNourriture++;
            OnPropertyChanged("QuantitéNourriture");
        }

    }
}
