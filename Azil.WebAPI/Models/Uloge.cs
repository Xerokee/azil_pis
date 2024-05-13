using System;
using System.Collections.Generic;

namespace Azil.WebAPI.Models
{
    public partial class Uloge
    {
        public Uloge()
        {
            KorisnikUloga = new HashSet<KorisnikUloga>();
        }

        public int IdUloge { get; set; }
        public string NazivUloge { get; set; }

        public virtual Korisnici IdUlogeNavigation { get; set; }
        public virtual ICollection<KorisnikUloga> KorisnikUloga { get; set; }
    }
}
