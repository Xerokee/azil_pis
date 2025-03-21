using System;
using System.Collections.Generic;
using System.Text;

namespace Azil.DAL.DataModel
{
    public partial class Korisnici
    {
        public int id_korisnika { get; set; }
        public string korisnickoIme { get; set; }
        public string ime { get; set; }
        public string prezime { get; set; }
        public string email { get; set; }
        public string lozinka { get; set; }
        public bool admin { get; set; }
        public string profileImg { get; set; }
        public string token { get; set; }
    }
}
