using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class CalendarioApertureSpeciali
    {
        public int idCalendarioApertureSpeciali;
        public DateTime Data;
        public TimeSpan OrarioApertura;
        public TimeSpan OrarioChiusura;
        public int NumBigliettiMax;
        public int idMuseo;
    }
}
