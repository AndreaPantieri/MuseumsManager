using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Creatore : DBEntity
    {
        public int idCreatore { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Descrizione { get; set; }
        public int AnnoNascita { get; set; }

        public Creatore() : base() { }
        public Creatore(int idCretore) : base(idCretore) { }

        public override string ToString()
        {
            return this.Nome + " " + this.Cognome;
        }
    }
}
