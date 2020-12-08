using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class PeriodoStorico : DBEntity
    {
        public int idPeriodoStorico { get; set; }
        public string Nome { get; set; }
        public string Descrizione { get; set; }
        public int AnnoInizio { get; set; }
        public int AnnoFine { get; set; }

        public PeriodoStorico() : base() { }
        public PeriodoStorico(int idPeriodoStorico) : base(idPeriodoStorico) { }
    }
}
