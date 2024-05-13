using System;
using System.Collections.Generic;

namespace Azil.WebAPI.Models
{
    public partial class KucniLjubimciUdomitelj
    {
        public int IdLjubimca { get; set; }
        public int IdUdomitelja { get; set; }

        public virtual KucniLjubimci IdLjubimcaNavigation { get; set; }
    }
}
