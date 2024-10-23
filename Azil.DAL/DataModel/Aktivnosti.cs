using System;
using System.Collections.Generic;
using System.Text;

namespace Azil.DAL.DataModel
{
    public partial class Aktivnosti
    {
        public int id { get; set; }
        public int id_ljubimca { get; set; }
        public DateTime datum { get; set; }
        public string opis { get; set; }
    }
}
