using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class RegistroManutenzioni : DBEntity
    {
        public int idManutenzione { get; set; }
        public DateTime Data { get; set; }
        public string Descrizione { get; set; }
        public float Prezzo { get; set; }
        public int idMuseo { get; set; }

        public RegistroManutenzioni(int idManutenzione) : base(idManutenzione) { }
    }
}
