using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class TipoMuseo : DBEntity
    {
        public int idTipoMuseo { get; set; }
        public string Descrizione { get; set; }
        public TipoMuseo() : base() { }
        public TipoMuseo(int idTipoMuseo) : base(idTipoMuseo) { }
    }
}
