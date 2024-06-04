using System;
using System.Collections.Generic;

namespace Azil.WebAPI.Models
{
    public partial class KucniLjubimci
    {
        public KucniLjubimci()
        {
            KucniLjubimciUdomitelj = new HashSet<KucniLjubimciUdomitelj>();
        }

        public int IdLjubimca { get; set; }
        public int IdUdomitelja { get; set; }
        public string ImeLjubimca { get; set; }
        public string TipLjubimca { get; set; }
        public string OpisLjubimca { get; set; }
        public bool? Udomljen { get; set; }
        public string ImgUrl { get; set; }

        public virtual DnevnikUdomljavanja IdLjubimcaNavigation { get; set; }
        public virtual ICollection<KucniLjubimciUdomitelj> KucniLjubimciUdomitelj { get; set; }
    }
}
