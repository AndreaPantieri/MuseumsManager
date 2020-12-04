using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class TipoPersonale : DBEntity
    {
        public int idTipoPersonale { get; set; }
        public string Descrizione { get; set; }

        public TipoPersonale(int idTipoPersonale) : base(idTipoPersonale) { }
    }
}
