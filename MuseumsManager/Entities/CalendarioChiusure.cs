using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class CalendarioChiusure : DBEntity
    {
        public int idCalendarioChiusure { get; set; }
        public DateTime Data { get; set; }
        public int idMuseo { get; set; }
        
        public CalendarioChiusure(int idCalendarioChiusure) : base(idCalendarioChiusure) { }
    }
}
