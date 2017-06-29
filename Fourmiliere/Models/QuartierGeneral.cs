using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Fourmiliere.Models
{
    public class QuartierGeneral: ModelBase
    {

        private static QuartierGeneral instance;

        public Uri imageUri;

        public Image image;

        private int x;
        private int y;

        public int X { get; }
        public int Y { get; }

        private int quantitéNourriture = 0;

        public int QuantitéNourriture { get; }

        private static Random Hasard = new Random();

        protected QuartierGeneral()
        {

            imageUri = new Uri("../Assets/images/qg.png", UriKind.Relative);
            image = new Image();
            image.Source = new BitmapImage(imageUri);

            X = Hasard.Next(App.DimensionX);
            Y = Hasard.Next(App.DimensionY);
        }

        public static QuartierGeneral Get()
        {
            if (instance == null)
            {
                instance = new QuartierGeneral();
            }
            return instance;
        }

        private void AjoutNourriture(int quantité)
        {
            quantitéNourriture++;
        }

    }
}
