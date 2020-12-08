using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class RegistroPresenze : DBEntity
    {
        public int idRegistro { get; set; }
        public DateTime DataEntrata { get; set; }
        public DateTime DataUscita { get; set; }
        public int idPersonale { get; set; }

        public RegistroPresenze() : base() { }
        public RegistroPresenze(int idRegistro) : base(idRegistro) { }
    }
}
