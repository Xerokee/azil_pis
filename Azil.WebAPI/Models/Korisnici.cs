using System;
using System.Collections.Generic;

namespace Azil.WebAPI.Models
{
    public partial class Korisnici
    {
        public int IdKorisnika { get; set; }
        public string KorisnickoIme { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string Lozinka { get; set; }
        public bool? Admin { get; set; }

        public virtual DnevnikUdomljavanja IdKorisnikaNavigation { get; set; }
        public virtual Uloge Uloge { get; set; }
    }
}
