using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Statistiche : DBEntity
    {
        public int idStatistiche { get; set; }
        public DateTime MeseAnno { get; set; }
        public float SpeseTotali { get; set; }
        public float Fatturato { get; set; }
        public int NumBigliettiVenduti { get; set; }
        public int NumPresenzeTotali { get; set; }
        public int NumManutenzioni { get; set; }
        public int NumContenutiNuovi { get; set; }
        public int NumChiusure { get; set; }

        public Statistiche() : base() { }
        public Statistiche(int idStatistiche) : base(idStatistiche) { }
    }
}
