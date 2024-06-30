using System;
using System.Collections.Generic;

namespace Azil.WebAPI.Models
{
    public partial class DnevnikUdomljavanja
    {
        public int IdLjubimca { get; set; }
        public int IdKorisnika { get; set; }
        public string ImeLjubimca { get; set; }
        public string TipLjubimca { get; set; }
        public bool Udomljen { get; set; }
        public DateTime? Datum { get; set; }
        public TimeSpan? Vrijeme { get; set; }
        public string ImgUrl { get; set; }
        public bool StanjeZivotinje { get; set; }

        public virtual Korisnici Korisnici { get; set; }
        public virtual KucniLjubimci KucniLjubimci { get; set; }
    }
}
