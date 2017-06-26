using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fourmiliere
{
    public class AproposViewModel : ViewModelBase
    {
        public string Copyright
        {
            get { return "Metagenia"; }
        }

        public string DateApplication
        {
            get { return DateTime.Now.ToString(); }
        }

        public string Auteur
        {
            get { return "Antoine PELLETIER\nJean-Christophe MELIKIAN"; }
        }
        
    }
}
