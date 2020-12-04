using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class CalendarioApertureSpeciali : DBEntity
    {
        public int idCalendarioApertureSpeciali { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan OrarioApertura { get; set; }
        public TimeSpan OrarioChiusura { get; set; }
        public int NumBigliettiMax { get; set; }
        public int idMuseo { get; set; }

        public CalendarioApertureSpeciali(int idCalendarioApertureSpeciali) : base(idCalendarioApertureSpeciali) { }
    }
}
