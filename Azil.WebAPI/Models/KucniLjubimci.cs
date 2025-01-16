using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Azil.WebAPI.Models
{
    public partial class KucniLjubimci
    {
        public KucniLjubimci()
        {
            KucniLjubimciUdomitelj = new HashSet<KucniLjubimciUdomitelj>();
        }

        [Column("id_ljubimca")]
        public int IdLjubimca { get; set; }
        [Column("id_udomitelja")]
        public int IdUdomitelja { get; set; }
        [Column("ime_ljubimca")]
        public string ImeLjubimca { get; set; }
        [Column("tip_ljubimca")]
        public int TipLjubimca { get; set; }
        [Column("opis_ljubimca")]
        public string OpisLjubimca { get; set; }
        [Column("udomljen")]
        public bool? Udomljen { get; set; }
        [Column("zahtjev_udomljen")]
        public bool? ZahtjevUdomljen { get; set; }
        [Column("imgUrl")]
        public string ImgUrl { get; set; }
        public List<GalerijaZivotinja> GalerijaZivotinja { get; set; }
        public int Dob { get; set; }
        public string Boja { get; set; }

        public virtual DnevnikUdomljavanja IdLjubimcaNavigation { get; set; }
        public virtual ICollection<KucniLjubimciUdomitelj> KucniLjubimciUdomitelj { get; set; }
    }
}
