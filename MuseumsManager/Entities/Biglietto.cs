using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Biglietto : DBEntity
    {
        public int idBiglietto { get; set; }
        public DateTime DataValidita { get; set; }
        public int idMuseo { get; set; }
        public int idTipoBiglietto { get; set; }

        public Biglietto() : base() { }
        public Biglietto(int idBiglietto) : base(idBiglietto) { }
    }
}
