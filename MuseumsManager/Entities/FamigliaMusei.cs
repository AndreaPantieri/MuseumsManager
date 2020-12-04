using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class FamigliaMusei : DBEntity
    {
        public int idFamiglia { get; set; }
        public string Nome { get; set; }

        public FamigliaMusei(int idFamiglia) : base(idFamiglia) { }
    }
}
