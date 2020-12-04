using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Contenuto : DBEntity
    {
        public int idContenuto { get; set; }
        public string Nome { get; set; }
        public string Descrizione { get; set; }
        public DateTime DataRitrovamento { get; set; }
        public DateTime DataArrivoMuseo { get; set; }
        public int idContenutoPadre { get; set; }
        public int idProvenienza { get; set; }
        public int idPeriodoStorico { get; set; }
        public int idSezione { get; set; }

        public Contenuto(int idContenuto) : base(idContenuto) { }
    }
}
