using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuseumsManager
{
    public class ContenutoForList
    {
        public Contenuto Contenuto { get; set; } = new Contenuto();

        public List<Contenuto> Figli { get; set; } = new List<Contenuto>();


    }
}
