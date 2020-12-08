using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class TipoBiglietto : DBEntity
    {
        public int idTipoBiglietto { get; set; }
        public float Prezzo { get; set; }
        public string Descrizione { get; set; }
        public int idMuseo { get; set; }

        public TipoBiglietto() : base() { }
        public TipoBiglietto(int idTipoBiglietto) : base(idTipoBiglietto) { }
    }
}
