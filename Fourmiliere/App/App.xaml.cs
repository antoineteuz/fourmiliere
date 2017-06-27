using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Fourmiliere
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static FourmiliereViewModel FourmiliereViewModel { get; set; }
        public static QuartierGénéral QuartierGénéral { get; set; }

        public App()
        {
            FourmiliereViewModel = new FourmiliereViewModel();
            QuartierGénéral = new QuartierGénéral(new Random().Next(5));
        }
    }
}
