using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Provenienza : DBEntity
    {
        public int idProvenienza { get; set; }
        public string Nome { get; set; }
        public string Descrizione { get; set; }

        public Provenienza(int idProvenienza) : base(idProvenienza) { }
    }
}
