using System;

namespace Fourmiliere.ViewModel
{
    public class AProposViewModel: ViewModelBase
    {
        public string Copyright {
            get { return "Metagenia"; }
        }

        public string DateApplication
        {
            get { return DateTime.Now.ToString(); }
        }

        public string Auteurs
        { get { return "Antoine PELLETIER\nJean-Christophe MELIKIAN"; } }
    }
}
