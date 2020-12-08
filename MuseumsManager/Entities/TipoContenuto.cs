using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class TipoContenuto : DBEntity
    {
        public int idTipoContenuto { get; set; }
        public string Descrizione { get; set; }

        public TipoContenuto() : base() { }
        public TipoContenuto(int idTipoContenuto) : base(idTipoContenuto) { }
    }
}
