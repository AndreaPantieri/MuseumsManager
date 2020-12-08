using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Personale : DBEntity
    {
        public int idPersonale { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Cellulare { get; set; }
        public string Email { get; set; }
        public float StipendioOra { get; set; }
        public int idMuseo { get; set; }

        public Personale() : base() { }
        public Personale(int idPersonale) : base(idPersonale) { }
    }
}
