using System;
using System.Collections.Generic;

namespace Azil.WebAPI.Models
{
    public partial class DnevnikUdomljavanja
    {
        public int IdLjubimca { get; set; }
        public int IdKorisnika { get; set; }
        public DateTime? Datum { get; set; }
        public string Opis { get; set; }

        public virtual Korisnici Korisnici { get; set; }
        public virtual KucniLjubimci KucniLjubimci { get; set; }
    }
}
