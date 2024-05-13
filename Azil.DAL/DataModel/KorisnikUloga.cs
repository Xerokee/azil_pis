using System;
using System.Collections.Generic;
using System.Text;

namespace Azil.DAL.DataModel
{
    public partial class KorisnikUloga
    {
        public int id_korisnika { get; set; }
        public int id_uloge { get; set; }
        public DateTime datum_od { get; set; }
        public DateTime datum_do { get; set; }
    }
}
