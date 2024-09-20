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
        public string TipLjubimca { get; set; }
        [Column("opis_ljubimca")]
        public string OpisLjubimca { get; set; }
        [Column("udomljen")]
        public bool? Udomljen { get; set; }
        [Column("imgUrl")]
        public string ImgUrl { get; set; }
        public List<GalerijaZivotinja> GalerijaZivotinja { get; set; }

        public virtual DnevnikUdomljavanja IdLjubimcaNavigation { get; set; }
        public virtual ICollection<KucniLjubimciUdomitelj> KucniLjubimciUdomitelj { get; set; }
    }
}
