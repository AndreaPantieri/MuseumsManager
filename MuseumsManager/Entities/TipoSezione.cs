using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class TipoSezione : DBEntity
    {
        public int idTipoSezione { get; set; }
        public string Descrizione { get; set; }

        public TipoSezione() : base() { }
        public TipoSezione(int idTipoSezione) : base(idTipoSezione) { }
    }
}
