using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Sezione : DBEntity
    {
        public int idSezione { get; set; }
        public string Nome { get; set; }
        public string Descrizione { get; set; }
        public int idSezionePadre { get; set; }
        public int idMuseo { get; set; }

        public Sezione(int idSezione) : base(idSezione) { }
    }
}
