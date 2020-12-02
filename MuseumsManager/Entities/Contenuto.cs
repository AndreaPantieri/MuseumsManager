using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Contenuto
    {
        public int idContenuto;
        public string Nome;
        public string Descrizione;
        public DateTime DataRitrovamento;
        public DateTime DataArrivoMuseo;
        public int idContenutoPadre;
        public int idProvenienza;
        public int idPeriodoStorico;
        public int idSezione;
    }
}
